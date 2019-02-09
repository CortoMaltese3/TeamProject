create table Users(
user_id int identity(1,1) not null primary key,
firstname varchar(20) not null,
lastname varchar(20) not null,
email varchar(50) not null,
password varchar(100) not null,
);

create table UserRoles(
userrole_id int identity(1,1) not null primary key,
user_id int not null,
roledesc varchar(5) not null,
check (roledesc in ('Admin','Owner','User')),
constraint fk_userroles1 foreign key (user_id) references Users(user_id)
);

create table Review(
review_id int identity(1,1) not null primary key,
rating int not null,
comment nvarchar(250),
date datetime not null,
check (rating <=5 and rating >0)
);

create table Company(
company_id int identity(1,1) not null primary key,
company_name nvarchar(50) not null
);

create table Court(
court_id int identity (1,1) not null primary key,
court_name nvarchar(50) not null,
max_player int not null,
price decimal (5,2) not null,
check (max_player in (10, 14, 22))
);

create table Timetable(
timetable_id int identity (1,1) not null primary key,
court_id int not null,
day int not null,
hour time not null,
duration int not null,
constraint fk_timetable1 foreign key (court_id) references Court(court_id)
);

create table Facilities(
facility_id int identity(1,1) not null primary key,
facilitydesc nvarchar(20) not null
);

create table Location(
location_id int identity (1,1) not null primary key,
longtitude decimal (6,3) not null,
latitude decimal (6,3) not null,
point decimal (6,3) not null,
city nvarchar(20) not null,
);

create table Branch(
branch_id int identity (1,1) not null primary key,
company_id int not null,
branch_name nvarchar(50) not null,
location_id int not null,
constraint fk_branch1 foreign key (company_id) references Company(company_id),
constraint fk_branch2 foreign key (location_id) references Location(location_id)
);

create table Scedule(
scedule_id int identity(1,1) not null primary key,
court_id int not null,
constraint fk_scedule1 foreign key (court_id) references Court(court_id)
);

create table CompanyFacilities(
company_id int not null,
facility_id int not null,
constraint fk_companyfacilities1 foreign key (company_id) references Company(company_id),
constraint fk_companyfacilities2 foreign key (facility_id) references Facilities(facility_id)
);

create table CourtFacilities(
court_id int not null,
facility_id int not null,
constraint fk_courtfacilities1 foreign key (court_id) references Court(court_id),
constraint fk_courtfacilities2 foreign key (facility_id) references Facilities(facility_id)
);

create table Booking(
booking_id int identity (1,1) not null primary key,
court_id int not null,
DateTime datetime not null,
user_id int not null,
constraint fk_booking1 foreign key (court_id) references Court(court_id),
constraint fk_booking2 foreign key (user_id) references Users(user_id)
); 

