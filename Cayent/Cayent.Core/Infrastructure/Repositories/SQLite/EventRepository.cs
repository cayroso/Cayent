using Cayent.CQRS.Events;
using Cayent.CQRS.Repositories;
using Cayent.Infrastructure.Data;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cayent.Core.Infrastructure.Repositories.SQLite
{
    public sealed class EventRepository : IEventRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public EventRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            _dbConnection = unitOfWork.GetDbConnection();
            _dbTransaction = unitOfWork.GetDbTransaction();
        }


        void IEventRepository.Add(IEvent @event)
        {
            const string sql = @"
insert into core_Event(Id, correlationId, TenantId, Type, Event, RetryCount, DateSent, DateFailed, DateSuccess)
    values  (@Id, @correlationId, @TenantId, @Type, @Event, @RetryCount, @DateSent, @DateFailed, @DateSuccess)
;
";
            var eventData = @event.ToEventData();
            eventData.DateSent = DateTime.UtcNow;

            _dbConnection.Execute(sql, eventData, _dbTransaction);
        }

        void IEventRepository.IncrementRetryCount(string eventId)
        {
            const string sql = @"
update  core_Event
set     RetryCount = RetryCount + 1
        , DateFailed = current_timestamp
where   Id = @Id
;
";
            _dbConnection.Execute(sql, new { Id = eventId }, _dbTransaction);
        }

        void IEventRepository.UpdateDateSuccess(string eventId)
        {
            const string sql = @"
update  core_Event
set     DateSuccess = @DateSuccess
where   Id = @Id
;
";
            _dbConnection.Execute(sql, new { Id = eventId, DateSuccess = DateTime.UtcNow }, _dbTransaction);
        }

        void IEventRepository.UpdateDateFailure(string eventId)
        {
            const string sql = @"
update  core_Event
set     DateFailed = @DateFailure
where   Id = @Id
;
";
            _dbConnection.Execute(sql, new { Id = eventId, DateFailure = DateTime.UtcNow }, _dbTransaction);
        }

        void IEventRepository.UpdateTransactionDateFailure(string correlationId)
        {
            const string sql = @"
update  core_Event
set     DateFailed = @DateFailure
where   correlationId = @correlationId
;
";
            _dbConnection.Execute(sql, new { correlationId = correlationId, DateFailure = DateTime.UtcNow }, _dbTransaction);
        }


        IEnumerable<IEvent> IEventRepository.GetEvents(string correlationId)
        {
            const string sql = @"
select  evt.*
from    core_Event as evt
where   evt.correlationId = @correlationId
;
";
            var items = _dbConnection.Query<EventData>(sql, new { correlationId = correlationId }, _dbTransaction).AsList();

            var events = items.Select(p => p.DeserializeEvent()).AsList();

            return events;
        }

        IEnumerable<IEvent> IEventRepository.GetUnproccesedEvents()
        {
            const string sql = @"
select  evt.*
from    core_Event as evt
where   evt.DateSuccess = @DateSuccess
and     evt.DateFailed = @DateFailed
order by evt.DateSent asc
;
";
            var items = _dbConnection.Query<EventData>(sql, new
            {
                DateSuccess = DateTime.MaxValue,
                DateFailed = DateTime.MaxValue
            }, _dbTransaction).AsList();

            var events = items.Select(p => p.DeserializeEvent()).ToList();

            return events;
        }

        IEnumerable<IEvent> IEventRepository.GetFailedEvents()
        {
            throw new NotImplementedException();
        }

        TEvent IEventRepository.GetEventOfType<TEvent>()
        {
            const string sql = @"
select  evt.*
from    core_Event as evt
where   evt.Type = @Type
;
";
            var item = _dbConnection.Query<EventData>(sql, new
            {
                Type = typeof(TEvent).AssemblyQualifiedName
            }, _dbTransaction).SingleOrDefault();

            if (item != null)
            {
                return item.DeserializeEvent() as TEvent;
            }

            return null;
        }

        TEvent IEventRepository.GetEventOfType<TEvent>(string correlationId)
        {
            const string sql = @"
select  evt.*
from    core_Event as evt
where   evt.correlationId = @correlationId
and     evt.Type = @Type
;
";
            var item = _dbConnection.Query<EventData>(sql, new
            {
                correlationId = correlationId,
                Type = typeof(TEvent).AssemblyQualifiedName
            }, _dbTransaction).SingleOrDefault();

            if (item != null)
            {
                return item.DeserializeEvent() as TEvent;
            }

            return null;
        }

        //        TEvent IEventRepository.GetEventOfTypeByTenant<TEvent>(string tenantId)
        //        {
        //            const string sql = @"
        //select  evt.*
        //from    core_Event as evt
        //where   evt.TenantId = @TenantId
        //and     evt.Type = @Type
        //;
        //";
        //            var item = _dbConnection.Query<EventData>(sql, new
        //            {
        //                TenantId = tenantId,
        //                Type = typeof(TEvent).AssemblyQualifiedName
        //            }, _dbTransaction).SingleOrDefault();

        //            if (item != null)
        //            {
        //                return item.DeserializeEvent() as TEvent;
        //            }

        //            return null;
        //        }
    }

    internal static class EventDataExtensions
    {
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };

        public static EventData ToEventData(this IEvent @event)
        {
            var payload = JsonConvert.SerializeObject(@event, serializerSettings);

            var eventData = new EventData
            {
                Id = @event.Id,
                CorrelationId = @event.CorrelationId,
                //TenantId = @event.TenantId,
                Type = @event.GetType().AssemblyQualifiedName,
                Event = payload
            };

            return eventData;
        }

        public static IEvent DeserializeEvent(this EventData eventData)
        {
            var obj = (IEvent)JsonConvert.DeserializeObject(eventData.Event, Type.GetType(eventData.Type));

            return obj;
        }
    }
}
