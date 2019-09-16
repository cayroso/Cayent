using Cayent.Core.Domains.Models.Applications.Events;
using Cayent.Core.Domains.Models.Applications.Modules;
using Cayent.Core.Domains.Models.Permissions;
using Cayent.Core.Infrastructure.Data.Applications;
using Cayent.Domain.Models.Entities;
using Cayent.Domain.Models.Events;
using Cayent.Domain.Models.Identities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cayent.Core.Domains.Models.Applications
{
    public sealed class AppId : Identity
    {
        public AppId(string id) : base(id)
        {
        }
    }

    public class App : AggregateRoot
    {
        public AppId AppId { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string IconClass { get; private set; }
        public string Url { get; private set; }
        public int Sequence { get; private set; }

        private List<PermissionId> _permissionIds = new List<PermissionId>();
        public IReadOnlyCollection<PermissionId> PermissionIds => new ReadOnlyCollection<PermissionId>(_permissionIds);

        private List<ModuleId> _moduleIds = new List<ModuleId>();
        public IReadOnlyCollection<ModuleId> ModuleIds => new ReadOnlyCollection<ModuleId>(_moduleIds);

        public App(AppData data)
            : base(data.DateCreated, data.DateUpdated, data.DateEnabled, data.DateDeleted)
        {
            AppId = new AppId(data.Id);
            Title = data.Title;
            Description = data.Description;
            IconClass = data.IconClass;
            Url = data.Url;
            _permissionIds = data.PermissionIds.Select(p => new PermissionId(p)).ToList();
            _moduleIds = data.ModuleIds.Select(p => new ModuleId(p)).ToList();
        }

        public App(AppId appId, string title, string description, string iconClass, string url, int sequence)
            : this(appId, title, description, iconClass, url, sequence,
                  DateTime.UtcNow, DateTime.UtcNow, DateTime.MaxValue, DateTime.MaxValue)
        {

        }
        public App(AppId appId, string title, string description, string iconClass, string url, int sequence,
            DateTime dateCreated, DateTime dateUpdated, DateTime dateEnabled, DateTime dateDeleted)
            : base(dateCreated, dateUpdated, dateEnabled, dateDeleted)
        {
            Apply(new AppCreated(appId, title, description, iconClass, url, sequence,
                dateCreated, dateUpdated, dateEnabled, dateDeleted));
        }

        public void Enable()
        {
            if (DateEnabled < DateTime.MaxValue)
            {
                Apply(new AppEnabled(AppId));
            }
        }

        public void Disable()
        {
            if (DateEnabled == DateTime.MaxValue)
            {
                Apply(new AppDisabled(AppId));
            }
        }

        public void AddPermission(string permissionId)
        {
            var found = _permissionIds.SingleOrDefault(p => p.Id == permissionId);

            if (found == null)
            {
                Apply(new PermissionAdded(AppId, new PermissionId(permissionId)));
            }
        }

        public void EnablePermission(string permissionId)
        {
            var found = _permissionIds.SingleOrDefault(p => p.Id == permissionId);

            if (found != null)
            {
                Apply(new PermissionEnabled(AppId, found));
            }
        }

        public void DisablePermission(string permissionId)
        {
            var found = _permissionIds.SingleOrDefault(p => p.Id == permissionId);

            if (found != null)
            {
                Apply(new PermissionDisabled(AppId, new PermissionId(permissionId)));
            }
        }

        public void RemovePermission(string permissionId)
        {
            var found = _permissionIds.SingleOrDefault(p => p.Id == permissionId);

            if (found != null)
            {
                Apply(new PermissionRemoved(AppId, new PermissionId(permissionId)));
            }
        }

        public void EnableModule(string moduleId)
        {
            var found = _moduleIds.SingleOrDefault(p => p.Id == moduleId);

            if (found != null)
            {
                Apply(new ModuleEnabled(AppId, found));
            }
        }

        public void DisableModule(string moduleId)
        {
            var found = _moduleIds.SingleOrDefault(p => p.Id == moduleId);

            if (found != null)
            {
                Apply(new ModuleDisabled(AppId, found));
            }
        }

        void When(AppCreated e)
        {
            AppId = e.AppId;
            Title = e.Title;
            Description = e.Description;
            IconClass = e.IconClass;
            Url = e.Url;
        }

        void When(PermissionAdded e)
        {
            _permissionIds.Add(e.PermissionId);
        }

        void When(PermissionEnabled e)
        {
            //  no action required
        }

        void When(PermissionDisabled e)
        {
            //  no action required
        }

        void When(PermissionRemoved e)
        {
            _permissionIds.Remove(e.PermissionId);
        }

        void When(AppEnabled e)
        {
            DateEnabled = DateTime.MaxValue;
        }

        void When(AppDisabled e)
        {
            DateEnabled = e.OccurredAt;
        }

        void When(ModuleEnabled e)
        {
            //  no action required
        }

        void When(ModuleDisabled e)
        {
            //  no action required
        }

        protected override IEnumerable<object> GetIdentityComponents()
        {
            yield return AppId;
        }

        protected override void When<T>(T e)
        {
            When(e as dynamic);
        }
        //protected override void When<T>(T e) where T: class, IDomainEvent
        //{
        //    When(e as dynamic);
        //}
    }
}
