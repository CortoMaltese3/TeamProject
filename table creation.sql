create database ProjectDB collate greek_ci_ai
go
use ProjectDB
go

create table [User](
	ID int identity(1,1) not null primary key,
	Firstname varchar(20) not null,
	Lastname varchar(20) not null,
	Email varchar(50) not null,
	Password varchar(100) not null
);

create table UserRoles(
	ID int identity(1,1) not null primary key,
	UserID int not null,
	Role varchar(5) not null,
	check (Role in ('Admin','Owner','User')),
	constraint fk_UserRolesToUsers foreign key (UserID) references [User](ID)
);

create table Company(
	ID int identity(1,1) not null primary key,
	UserID int not null,
	Name nvarchar(50) not null,
	constraint fk_CompanyToUsers foreign key (UserID) references [User](ID)
);

create table Branch(
	ID int identity (1,1) not null primary key,
	CompanyID int not null,
	Name nvarchar(50) not null,
	Longtitude decimal (6,3) not null,
	Latitude decimal (6,3) not null,
	Point decimal (6,3) not null,
	City nvarchar(20) not null,
	Address nvarchar(200) not null,
	ZipCode nvarchar(200) not null,

	constraint fk_BranchToCompany foreign key (CompanyID) references Company(ID)
);

create table Court(
	ID int identity (1,1) not null primary key,
	BranchID int not null,
	Name nvarchar(50) not null,
	MaxPlayers int not null,
	Price decimal (5,2) not null,
	check (MaxPlayers in (10, 14, 22)),
	constraint fk_CourtToBranch foreign key (BranchID) references Branch(ID)
);

create table Review(
	ID int identity(1,1) not null primary key,
	BranchID int not null,
	UserID int not null,
	Rating int not null,
	Comment nvarchar(250),
	CommentAt datetime not null, check (rating <=5 and rating >0),
	constraint fk_ReviewToBranch foreign key (BranchID) references Branch(ID),
	constraint fk_ReviewToUsers foreign key (UserID) references [User](ID)
);

create table Facility(
	ID int identity(1,1) not null primary key,
	Description nvarchar(20) not null	
);

create unique index Ix_FacilityDescription on Facility (Description)


create table BranchFacilities(
	BrancID int not null,
	FacilityID int not null,
	constraint fk_BranchFacilitiesToBranch foreign key (BrancID) references Branch(ID),
	constraint fk_BranchFacilitiesToFacility foreign key (FacilityID) references Facility(ID),
	constraint pk_BranchFacilities primary key (BrancID, FacilityID)
);

create table TimeSlot(
	ID int identity (1,1) not null primary key,
	CourtID int not null,
	Day int not null,
	Hour time not null,
	Duration int not null,
	constraint fk_TimeSlotToCourt foreign key (CourtID) references Court(ID)
);

create table Booking(
	ID int identity(1,1) not null primary key,
	CourtID int not null,
	UserID int not null,
	BookedAt datetime not null,
	Duration int not null,
	constraint fk_BookingToCourt foreign key (CourtID) references Court(ID),
	constraint fk_BookingToUser foreign key (UserID) references [User](ID)
);
	
create unique index Ix_BookingCourtBookedAt on Booking (CourtID, BookedAt)
