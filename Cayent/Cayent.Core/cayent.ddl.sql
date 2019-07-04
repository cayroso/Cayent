
--	///////////////////////////////
--	Core Tables
--	///////////////////////////////
create table core_App
(
	AppId varchar(32) not null
	, Title varchar(128) not null
	, Description varchar(1024) not null
	, IconClass varchar(32) not null
	, Url varchar(128) not null
	, Sequence integer not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_app_pk primary key(AppId)
	, constraint core_app_uk_title unique(Title)
)
;

create table core_Module
(
	AppId varchar(32) not null
	, ModuleId varchar(32) not null	
	, Title varchar(128) not null
	, Description varchar(1024) not null
	, IconClass varchar(32) not null
	, Url varchar(128) not null
	, Sequence integer not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_module_pk primary key(ModuleId)
	, constraint core_module_fk_app foreign key(AppId) references core_App(AppId)
	, constraint core_module_uk_name unique(AppId, Title)
)
;

create table core_Permission
(
	AppId varchar(32) not null
	, PermissionId varchar(32) not null
	, [Name] varchar(128) not null
	, [Description] varchar(1024) not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_perm_pk primary key(PermissionId)
	, constraint core_perm_uk_name unique(AppId, [Name])
)
;

create table core_Event(
	EventId varchar(32) not null
	, CorrelationId varchar(32) not null
	, [Type] varchar(512) null
	, [Event] varchar(8000) null
	
	, RetryCount int not null
	, DateSent datetime not null
	, DateFailed datetime not null
	, DateSuccess datetime not null
	
	, constraint core_event_pk primary key(EventId)
)
;
create index core_event_idx on core_Event(CorrelationId, [Type])
;

create table core_User
(
	UserId varchar(32) not null
	, FirstName varchar(128) not null
	, MiddleName varchar(128) not null
	, LastName varchar(128) not null
	, Email varchar(128) not null
	, Phone varchar(128) not null
	, Mobile varchar(128) not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_user_pk primary key(UserId)
)
;

create table core_Login
(
	LoginId varchar(32) not null
	, UserName varchar(128) not null
	, HashedPassword varchar(128) not null
	, Salt varchar(128) not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_login_pk primary key(LoginId)
	, constraint core_login_fk_user foreign key(LoginId) references core_User(UserId)
	, constraint core_login_uk_username unique(UserName)
)
;

create table core_Membership
(
	MembershipId varchar(32) not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_membership_pk primary key(MembershipId)
)
;


create table core_Role
(
	RoleId varchar(32) null
	, [Name] varchar(128) not null
	, [Description] varchar(1024) not null
	
	, DateCreated datetime not null
	, DateUpdated datetime not null
	, DateEnabled datetime not null
	, DateDeleted datetime not null
	
	, constraint core_role_pk primary key(RoleId)
	, constraint core_role_uk_name unique([Name])
)
;

create table core_RolePermission
(
	RoleId varchar(32) not null
	, PermissionId varchar(32) not null
	
	, DateCreated datetime not null
	, DateEnabled datetime not null
	
	, constraint core_rolePerm_pk primary key(RoleId, PermissionId)
	, constraint core_rolePerm_fk_role foreign key(RoleId) references core_Role(RoleId)
	, constraint core_rolePerm_fk_perm foreign key(PermissionId) references core_Permission(PermissionId)
)
;

create table core_MembershipRole
(
	MembershipId varchar(32) not null
	, RoleId varchar(32) not null
	
	, DateCreated datetime not null
	, DateEnabled datetime not null
	
	, constraint core_memRole_pk primary key(MembershipId, RoleId)
	, constraint core_memRole_fk_membership foreign key(MembershipId) references core_Membership(MembershipId)
	, constraint core_memRole_fk_role foreign key(RoleId) references core_Role(RoleId)
)
;

create table core_MembershipApp
(
	MembershipId varchar(32) not null
	, AppId varchar(32) not null

	, DateCreated datetime not null
	, DateEnabled datetime not null
	
	, constraint coreMemApp_pk primary key(MembershipId, AppId)
	, constraint coreMemApp_fk_mem foreign key(MembershipId) references core_Membership(MembershipId)
	, constraint coreMemApp_fk_app foreign key(AppId) references core_App(AppId)
)
;

create table core_MembershipModule
(
	MembershipId varchar(32) not null
	, ModuleId varchar(32) not null

	, DateCreated datetime not null
	, DateEnabled datetime not null
	
	, constraint coreMemMod_pk primary key(MembershipId, ModuleId)
	, constraint coreMemMod_fk_mem foreign key(MembershipId) references core_Membership(MembershipId)
	, constraint coreMemMod_fk_app foreign key(ModuleId) references core_Module(ModuleId)
)
;


create table core_Edge
(
	EdgeId integer not null 
	, EntryEdgeId int null
	, DirectEdgeId int null
	, ExitEdgeId int null

	, StartVertex varchar(128) not null
	, EndVertex varchar(128) not null
	, Hops int not null
	, [Source] varchar(128) not null
    
	, constraint edge_pk primary key(EdgeId)
	, constraint core_edge_fk_entry foreign key(EntryEdgeId) references core_Edge(EdgeId)
	, constraint core_edge_fk_direct foreign key(DirectEdgeId) references core_Edge(EdgeId)
	, constraint core_edge_fk_exit foreign key(ExitEdgeId) references core_Edge(EdgeId)
	, constraint core_edge_uk unique(StartVertex, EndVertex, Hops, [Source])
)
;


--	///////////////////////////////
--	Core Views
--	///////////////////////////////

create view core_vwModule
as
	select	m.AppId
			, m.ModuleId			
			, m.Title
			, m.Description
			, m.IconClass
			, m.Url
			, m.Sequence
			
			, m.DateCreated
			, m.DateUpdated
			, cast((case when m.DateEnabled < a.DateEnabled then m.DateEnabled else a.DateEnabled end) as datetime) as 'DateEnabled'
			, m.DateDeleted
	from	core_Module m
	join	core_App a on (m.AppId = a.AppId)
;

create view core_vwPermission
as
	select	p.PermissionId
			, p.AppId
			, p.Name
			, p.Description
			
			, p.DateCreated
			, p.DateUpdated
			, cast((case when p.DateEnabled < a.DateEnabled then p.DateEnabled else a.DateEnabled end) as datetime) as 'DateEnabled'
			, p.DateDeleted
	from	core_Permission p
	join	core_App a on (p.AppId = a.AppId)
;

create view core_vwMembership
as
	select	m.MembershipId
			, m.DateCreated
			, m.DateUpdated
			, m.DateEnabled
			, m.DateDeleted
	from	core_Membership m
	join	core_User u on (m.MembershipId = u.UserId)
;

create view core_vwUser
as
	select	u.UserId
			, u.FirstName
			, u.MiddleName
			, u.LastName
			, u.Email
			, u.Phone
			, u.Mobile

			, u.DateCreated
			, u.DateUpdated
			, cast((case when u.DateEnabled < m.DateEnabled then u.DateEnabled else m.DateEnabled end) as datetime) as 'DateEnabled'
			, u.DateDeleted
	from	core_User u
	join	core_vwMembership m on (m.MembershipId = u.UserId)
;

create view core_vwLogin
as
	select	l.LoginId
			, l.UserName
			, l.HashedPassword
			, l.Salt
			
			, l.DateCreated
			, l.DateUpdated
			, cast((case when l.DateEnabled < m.DateEnabled then l.DateEnabled else m.DateEnabled end) as datetime) as 'DateEnabled'
			, l.DateDeleted
	from	core_Login l
	join	core_vwMembership m on (m.MembershipId = l.LoginId)
;

create view core_vwRole
as
	select	r.RoleId
			, r.[Name]
			, r.[Description]
			
			, r.DateCreated
			, r.DateUpdated
			, r.DateEnabled
			, r.DateDeleted
	from	core_Role r
;

create view core_vwRolePermission
as
	select	rp.RoleId
			, p.PermissionId
			, p.[Name]
			, p.[Description]
			
			, rp.DateCreated
			, p.DateUpdated
			, cast((
				case when p.DateEnabled < rp.DateEnabled 
					then case when p.DateEnabled < r.DateEnabled then p.DateEnabled else r.DateEnabled end 
					else case when rp.DateEnabled < r.DateEnabled then rp.DateEnabled else r.DateEnabled end
				end
			) as datetime) as 'DateEnabled'
			, p.DateDeleted
	from	core_vwPermission p
	join	core_RolePermission rp on (rp.PermissionId = p.PermissionId)
	join	core_vwRole r on (rp.RoleId = r.RoleId)
;

create view core_vwMembershipRole
as
	select	mr.MembershipId
			, r.RoleId			
			, r.Name
			, r.Description

			, r.DateCreated
			, r.DateUpdated
			, cast((
				case when r.DateEnabled < mr.DateEnabled
					then case when r.DateEnabled < m.DateEnabled then r.DateEnabled else m.DateEnabled end
					else case when mr.DateEnabled < m.DateEnabled then mr.DateEnabled else m.DateEnabled end
				end
			) as datetime) as 'DateEnabled'
			, r.DateDeleted
	from	core_vwRole r
	join	core_MembershipRole mr on (mr.RoleId = r.RoleId)
	join	core_vwMembership m on (mr.MembershipId = m.MembershipId)
;

create view core_vwMembershipPermission
as
	select	mr.MembershipId
			, p.AppId
			, p.PermissionId			
			, p.Name
			, p.Description
			
			, p.DateCreated
			, p.DateUpdated
			, cast((
				case when mr.DateEnabled < rp.DateEnabled
					then case when mr.DateEnabled < p.DateEnabled then mr.DateEnabled else p.DateEnabled end
					else case when rp.DateEnabled < p.DateEnabled then rp.DateEnabled else p.DateEnabled end
				end
			) as datetime) as 'DateEnabled'
			, p.DateDeleted
	from	core_vwMembershipRole mr
	join	core_vwRolePermission rp on (rp.RoleId = mr.RoleId)
	join	core_vwPermission p on (rp.PermissionId = p.PermissionId)
;


--	///////////////////////////////
--	Default Values
--	///////////////////////////////
insert into core_App(AppId, Title, Description, IconClass, Url, Sequence, DateCreated, DateUpdated, DateEnabled, DateDeleted)
	values	('system.security', 'System Security', 'System Security Desc', 'fas fa-fw fa-home', 'www.cayent.come', -999, '2000-01-01', '2000-01-01', '9999-12-31', '9999-12-31')
			, ('app.security', 'Application Security', 'Application Security Desc', 'fas fa-fw fa-home', 'www.cayent.come', -999, '2000-01-01', '2000-01-01', '9999-12-31', '9999-12-31')

