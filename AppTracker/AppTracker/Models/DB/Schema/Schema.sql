--AppTracker DB Schema

create table Company (
	id int identity(1,1) primary key,
	[name] nvarchar(64),
	address1 nvarchar(64),
	address2 nvarchar(64),
	address3 nvarchar(64),
	notes nvarchar(max)
);

create table [Application] (
	id int identity(1,1) primary key,
	companyId int foreign key references Company(id),
	applicationDate date,
	[role] nvarchar(64),
);

create table ApplicationStatus (
	id int identity(1,1) primary key,
	applicationId int foreign key references [Application](id),
	[timestamp] datetime default current_timestamp,
	active bit,
	[status] nvarchar(32),
	notes nvarchar(max)
);

create table Contact (
	id int identity(1,1) primary key,
	companyId int foreign key references Company(id),
	firstName nvarchar(32),
	lastName nvarchar(32),
	phone nvarchar(32),
	email nvarchar(256),
	[role] nvarchar(64),
	notes nvarchar(max)
);

create table ApplicationContactXref (
	id int identity(1,1) primary key,
	applicationId int foreign key references [Application](id),
	contactId int foreign key references Contact(id)
);
