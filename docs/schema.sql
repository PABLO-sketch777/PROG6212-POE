IF DB_ID(N'RaceDayDb') IS NULL CREATE DATABASE RaceDayDb;
GO
USE RaceDayDb;
GO
CREATE TABLE Users (Id INT IDENTITY PRIMARY KEY, FullName NVARCHAR(120) NOT NULL, Email NVARCHAR(256) NOT NULL UNIQUE, PasswordHash NVARCHAR(500) NOT NULL, Role NVARCHAR(20) NOT NULL CHECK(Role IN ('Organiser','Participant')), Phone NVARCHAR(40) NULL, ProfileImageUrl NVARCHAR(1000) NULL);
CREATE TABLE Events (Id INT IDENTITY PRIMARY KEY, Name NVARCHAR(160) NOT NULL, Description NVARCHAR(MAX) NOT NULL, EventDate DATETIME2 NOT NULL, Location NVARCHAR(200) NOT NULL, DistanceKm DECIMAL(8,2) NOT NULL, EventType NVARCHAR(20) NOT NULL CHECK(EventType IN ('Run','Walk','Cycle')), BannerImageUrl NVARCHAR(1000) NULL, OrganiserId INT NOT NULL REFERENCES Users(Id));
CREATE TABLE Categories (Id INT IDENTITY PRIMARY KEY, EventId INT NOT NULL REFERENCES Events(Id) ON DELETE CASCADE, Name NVARCHAR(100) NOT NULL, MinimumAge INT NULL, MaximumAge INT NULL, DistanceKm DECIMAL(8,2) NULL);
CREATE TABLE Enrollments (Id INT IDENTITY PRIMARY KEY, EventId INT NOT NULL REFERENCES Events(Id) ON DELETE CASCADE, ParticipantId INT NOT NULL REFERENCES Users(Id), CategoryId INT NOT NULL REFERENCES Categories(Id), EnrolledAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(), CONSTRAINT UQ_Enrollment UNIQUE(EventId,ParticipantId));
CREATE TABLE Results (Id INT IDENTITY PRIMARY KEY, EnrollmentId INT NOT NULL UNIQUE REFERENCES Enrollments(Id) ON DELETE CASCADE, FinishTime TIME NOT NULL, FinishingPosition INT NOT NULL, RecordedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME());
INSERT Users(FullName,Email,PasswordHash,Role,Phone) VALUES
('Ava Organiser','organiser1@raceday.test','v1:ABEiM0RVZneImaq7zN3u/w==:omGC4JOzTp9jBk0k4VcIMiCU6we8p+Lxli1rJ4u+EM8=','Organiser','010-555-0101'),('Noah Organiser','organiser2@raceday.test','v1:ECEyQ1RldoeYqbrL3O3+Dw==:p1ar4Fe0+AIOPzPrqPDVVJjibs2AVCCfXIziIqD0zrE=','Organiser','010-555-0102'),('Liam Participant','participant1@raceday.test','v1:IDFCU2R1hpeoucrb7P0OHw==:QsZls/WGJy8wJGOOSNiibK0D/rxCv9B2KRPdd2cIaVE=','Participant','010-555-0201'),('Mia Participant','participant2@raceday.test','v1:MEFSY3SFlqe4ydzt/g8QIQ==:HrJUNTOTDvRB6JgdMkDJZ8M70eVoEwccmLxDX5NRCi4=','Participant','010-555-0202');
INSERT Events(Name,Description,EventDate,Location,DistanceKm,EventType,OrganiserId) VALUES
('Rosebank Sunrise Run','A friendly city road run.','2027-02-13T07:00:00','Rosebank, Johannesburg',10,'Run',1),('Lakeside Family Walk','An accessible community walk.','2027-03-06T08:00:00','Zoo Lake, Johannesburg',5,'Walk',2),('Gauteng Cycle Challenge','A timed road cycling event.','2027-04-10T06:00:00','Pretoria East',42,'Cycle',1);
INSERT Categories(EventId,Name,MinimumAge,MaximumAge,DistanceKm) VALUES
(1,'Senior 10km',18,NULL,10),(1,'Under 20 10km',12,19,10),(2,'Family 5km',8,NULL,5),(2,'Senior 5km',60,NULL,5),(3,'Open 42km',18,NULL,42),(3,'Junior 20km',14,17,20);
INSERT Enrollments(EventId,ParticipantId,CategoryId) VALUES(1,3,1),(2,4,3),(3,3,5);
