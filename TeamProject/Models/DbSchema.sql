use master
go
--drop database ProjectDB
--go 

create database ProjectDB collate greek_ci_ai
go

use ProjectDB
go

create table [User](
	  Id int identity(1,1) not null
	, Firstname varchar(50) not null
	, Lastname varchar(50) not null
	, Email varchar(50) not null
	, Password BINARY(64) not null
	, Salt uniqueidentifier not null

	, constraint pk_user primary key (Id)
)
go

create unique index ix_User_email on [User] (Email)
go

create procedure ValidateUser
	  @Email varchar(30)
	, @Password varchar(30)
as
begin
		select count(*)
		from [user] 
		where  password = HASHBYTES('SHA2_512', @password+CAST(salt AS NVARCHAR(36)))
		   and email = @email
end
go

create procedure InsertUser 
	  @Email varchar(50)
	, @FirstName varchar(50)
	, @LastName varchar(50) 
	, @Password varchar(30) 
aS
begin

	declare @salt uniqueidentifier = newid()
	
	insert into [User] (email, password, salt , firstName, lastName) 
	values (@email, HASHBYTES('SHA2_512', @password+CAST(@salt AS NVARCHAR(36))), @salt , @firstName, @lastName)
	select SCOPE_IDENTITY();
end
go

create procedure UpdateUserPassword 
	  @Id int
	, @Password varchar(30) 
as 
begin
	update [User] set 
		password = HASHBYTES('SHA2_512', @password+CAST(salt AS NVARCHAR(36)))
	where Id = @id 
end
go

create table UserRoles(
	  Id int identity(1,1) not null
	, UserId int not null
	, Role varchar(5) not null

	, constraint chk_Role check (role in ('Admin','Owner','User'))
	, constraint fk_UserRolesToUsers foreign key (UserId) references [User](Id)
	, constraint pk_UserRoles primary key (Id)
);
go

create unique index ix_UserRoles on UserRoles (UserId, Role)
go

create table Branch(
	  Id int identity (1,1) not null
	, UserId int not null
	, Name nvarchar(50) not null
	, Longitude float not null
	, Latitude float not null
	, City nvarchar(20) not null
	, Address nvarchar(200) not null
	, ZipCode nvarchar(200) not null

	, constraint fk_CompanyToUsers foreign key (UserID)  references [User](Id) on delete cascade
	, constraint pk_Branch primary key (Id)
);
go

create unique index ix_Branch on Branch (Name)
go

create table Court(
	  Id int identity (1,1) not null
	, BranchId int not null
	, Name nvarchar(50) not null
	, ImageCourt nvarchar(500) not null
	, MaxPlayers int not null
	, Price decimal (5,2) not null
	, constraint fk_CourtToBranch foreign key (BranchId) references Branch(Id) on delete cascade
	, constraint pk_Court primary key (Id)
);
go

create table Facility(
	  Id int identity(1,1) not null
	, Description nvarchar(20) not null	
	, constraint pk_Facility primary key (Id)
);
go

create unique index Ix_FacilityDescription on Facility (Description)
go

create table BranchFacilities(
	  BranchId int not null
	, FacilityId int not null
	, constraint fk_BranchFacilitiesToBranch foreign key (BranchId) references Branch(Id)
	, constraint fk_BranchFacilitiesToFacility foreign key (FacilityId) references Facility(Id)
	, constraint pk_BranchFacilities primary key (BranchId, FacilityId)
);
go

create table TimeSlot(
	  Id int identity (1,1) not null
	, CourtId int not null
	, Day int not null
	, Hour time not null
	, Duration int not null
	, constraint fk_TimeSlotToCourt foreign key (CourtId) references Court(Id)
	, constraint pk_TimeSlot primary key (Id)
);
go

create table Booking(
	  Id int identity(1,1) not null
	, CourtId int not null
	, UserId int not null
	, BookedAt datetime not null
	, Duration int not null
	, constraint fk_BookingToCourt foreign key (CourtId) references court(Id)
	, constraint fk_BookingToUser foreign key (UserId) references [User](Id)
	, constraint pk_Booking primary key (Id)
);
go

create unique index Ix_BookingCourtBookedAt on Booking (CourtId, BookedAt)
go

create table Review(
	  Id int identity(1,1) not null 
	, CourtId int not null
	, UserId int not null
	, Rating int not null
	, Comment nvarchar(250)
	, CommentAt datetime not null, check (rating <=5 and rating >0)
	, constraint fk_ReviewToCourt foreign key (CourtId) references Court(Id) on delete cascade
	, constraint fk_ReviewToUsers foreign key (UserId) references [User](Id) 
	, constraint pk_Review primary key (Id)
);
go

Create Procedure [dbo].[GetBranchesDistance] 
	@Latitude float,
	@Longitude float,
	@Distance float
AS
BEGIN
	DECLARE @g geography;  
	SET @g = geography::STGeomFromText('POINT('+ cast(@latitude as varchar) + ' ' + cast(@longitude as varchar) +')', 4326);  

	SELECT branch.*,  geography::STGeomFromText('POINT('+ cast(latitude as varchar) + ' ' + cast(longitude as varchar) +')', 4326).STDistance(@g) as Distance
	from branch
	where geography::STGeomFromText('POINT('+ cast(latitude as varchar) + ' ' + cast(longitude as varchar) +')', 4326).STDistance(@g)  <@distance
END
go 

execute InsertUser 'geo.xiros@gmail.com', 'george', 'xiros', '1234'
go

INSERT INTO Branch ([userId], [name], [latitude], [longitude], [city], [address], [zipCode]) VALUES 
(1, 'Athens Pitch', 37.9838096,	23.7275388, 'Αθήνα', 'Κεντρο','16232'),
(1, 'Δημοτικό Στάδιο Π.Α.Ο. Ρουφ', 37.9739482,	23.7056573, 'Αθήνα', 'Κεντρο','16232')
go

INSERT INTO [dbo].[Court] ([branchId],[name],[ImageCourt],[maxPlayers],[price]) VALUES 
(1,'Γηπεδο 1','https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Football_iu_1996.jpg/300px-Football_iu_1996.jpg',14,20.00),
(1,'Γηπεδο 2','https://avante.biz/wp-content/uploads/Football-Wallpapers/Football-Wallpapers-016.jpg',14,13.00),
(1,'Γηπεδο 3','https://upload.wikimedia.org/wikipedia/commons/7/72/Mia1997.JPG',14,10.00),
(2,'Γηπεδο 1','https://i.pinimg.com/736x/c4/42/c3/c442c3ac647fb2f31461a73c6e9f3afa--team-pictures-football-pictures.jpg',12,4.00),
(2,'Γηπεδο 2','https://www.telegraph.co.uk/content/dam/football/2018/08/31/TELEMMGLPICT000173026221_trans_NvBQzQNjv4BqYG-7GzYVtFQSFAHuTMXOjE_576i42-XSqgQd283hHmQ.jpeg?imwidth=450',5,24.00),
(2,'Γηπεδο 3','https://static.standard.co.uk/s3fs-public/thumbnails/image/2019/01/13/15/samirnasri1301.jpg',21,24.00)
GO


