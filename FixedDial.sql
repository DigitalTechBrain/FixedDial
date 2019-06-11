Use indiantr_fixeddial

create database indiantr_fixeddial

--Create a Sample Sequence object:
CREATE SEQUENCE dbo.seq_TestVarcharSequenceNumber AS 
INT START WITH 1
INCREMENT BY 1;
GO
-------Create Sequence constraint using FORMAT function:
ALTER TABLE dbo.MediaMaster
ADD CONSTRAINT seq_tbl_TestVarcharSequence_VarcharID DEFAULT 
FORMAT((NEXT VALUE FOR dbo.seq_TestVarcharSequenceNumber),'ABC00#') 
FOR mediafileName;
GO



create table CategoryMaster
(
categoryId int identity(1,1) not null primary key,
categoryName varchar(64),
IsActive bit,
IsSelected bit,
mediaId int,
pageTitle varchar(128),
metaKeyword varchar(512),
metaDescription varchar(256),
createDate datetime,
createdBy int,
updatedDate datetime,
updatedBy int
)

create Procedure categoryMasterPro
(
@categoryName varchar(64),
@IsActive bit,
@IsSelected bit,
@mediaId int,
@pageTitle varchar(128),
@metaKeyword varchar(512),
@metaDescription varchar(256),
@createDate datetime,
@createdBy int,
@updatedDate datetime,
@updatedBy int
)
As
Begin
insert into CategoryMaster (categoryName,IsActive,IsSelected,mediaId,pageTitle,metaKeyword,metaDescription,createDate,createdBy,updatedDate,updatedBy)
values(@categoryName,@IsActive,@IsSelected,@mediaId,@pageTitle,@metaKeyword,@metaDescription,@createDate,@createdBy,@updatedDate,@updatedBy)
End

Exec categoryMasterPro
@categoryName = 'Law',
@IsActive = 1,
@IsSelected = 1,
@mediaId = 1,
@pageTitle = 'Testing',
@metaKeyword ='Advocate',
@metaDescription = 'advocate Added',
@createDate = N'20120720',
@createdBy = 1,
@updatedDate = N'20120720',
@updatedBy = 1


create Procedure updateCategoryPro
(
@Id INT,
@categoryName varchar(64),
@IsActive bit,
@IsSelected bit,
@mediaId int,
@pageTitle varchar(128),
@metaKeyword varchar(512),
@metaDescription varchar(256),
@createDate datetime,
@createdBy int,
@updatedDate datetime,
@updatedBy int

)
As
Begin
UPDATE CategoryMaster SET categoryName=@categoryName,@IsActive = IsActive,@IsSelected = IsSelected,@mediaId = mediaId, pageTitle=@pageTitle,metaKeyword=@metaKeyword,metaDescription=@metaDescription ,@createDate = createDate,@createdBy = createdBy,@updatedDate = updatedDate,@updatedBy = updatedBy
WHERE categoryId = @id
End


select * from CategoryMaster
select * from MediaMaster

delete from CategoryMaster
delete from MediaMaster

delete from MediaMaster where mediaID = 91
select mediaID from CategoryMaster where categoryID = 90
SELECT SCOPE_IDENTITY()

create Table SubCategoryManager
(
SubCat_ID int identity(1,1)not null primary key,
Cat_ID int references CategoryMaster(Cate_Id),
SubCatName varchar(64),
PageTitle varchar(128),
MetaKeywork varchar(512),
MedaDescription varchar(256),
CreateDate datetime,
CreateBy int,
UpdatedDate datetime,
UpdatedBy int
)

create Procedure updateSubCategoryPro
(
@Id INT,
@SubCatName varchar(64),
@PageTitle varchar(128),
@MetaKeywork varchar(512),
@MedaDescription varchar(256),
@UpdatedDate datetime,
@UpdatedBy int

)
As
Begin
UPDATE SubCategoryManager SET SubCatName=@SubCatName,PageTitle = @PageTitle,MetaKeywork = @MetaKeywork,MedaDescription = @MedaDescription, UpdatedDate = UpdatedDate,UpdatedBy=@UpdatedBy 
WHERE SubCat_ID = @id
End

create Procedure subCategoryPro
(
@Cat_ID int,
@SubCatName varchar(64),
@PageTitle varchar(128),
@MetaKeywork varchar(512),
@MedaDescription varchar(256),
@CreateDate datetime,
@CreateBy int,
@UpdatedDate datetime,
@UpdatedBy int
)
As
Begin
insert into SubCategoryManager (Cat_ID,SubCatName,PageTitle,MetaKeywork,MedaDescription,CreateDate,CreateBy,UpdatedDate,UpdatedBy)
values(@Cat_ID,@SubCatName,@PageTitle,@MetaKeywork,@MedaDescription,@CreateDate,@CreateBy,@UpdatedDate,@UpdatedBy)
End

Create Table MicroCategory
(
MicroCat_ID int identity(1,1) not null primary key,
SubCat_ID int references SubCategoryManager(SubCat_ID),
MicroCatName varchar(64),
PageTitle varchar(128),
MetaKeyword varchar(512),
MetaDescription varchar(256),
CreateDate datetime,
CreateBy int,
UpdatedDate datetime,
UpdatedBy int
)

create Procedure microCategoryInsertPro
(

@SubCat_ID int ,
@MicroCatName varchar(64),
@PageTitle varchar(128),
@MetaKeyword varchar(512),
@MetaDescription varchar(256),
@CreateDate datetime,
@CreateBy int,
@UpdatedDate datetime,
@UpdatedBy int
)
As
Begin
insert into MicroCategory (SubCat_ID,MicroCatName,PageTitle,MetaKeyword,MetaDescription,CreateDate,CreateBy,UpdatedDate,UpdatedBy)
values(@SubCat_ID,@MicroCatName,@PageTitle,@MetaKeyword,@MetaDescription,@CreateDate,@CreateBy,@UpdatedDate,@UpdatedBy)
End

create Procedure microCategoryUpdationPro
(
@id int,
@MicroCatName varchar(64),
@PageTitle varchar(128),
@MetaKeyword varchar(512),
@MetaDescription varchar(256),
@UpdatedDate datetime,
@UpdatedBy int
)
As
Begin
UPDATE MicroCategory SET MicroCatName = @MicroCatName,PageTitle = @PageTitle,MetaKeyword = @MetaKeyword,MetaDescription = @MetaDescription, UpdatedDate = @UpdatedDate,UpdatedBy=@UpdatedBy 
WHERE MicroCat_ID = @id
End



select * from CategoryMaster
select * from SubCategoryManager
select * from MicroCategory

delete from MicroCategory where SubCat_ID = 16

// For Images
drop table MediaMaster

Create Table MediaMaster
(
mediaID int identity (1,1) not null primary key,
mediafileName varchar(64),
mediaOriginalName varchar(256),
mediaExtn varchar(8),
mediaSize int,
IsDeleated bit default 0,
CreatedDate datetime,
CreatedBy int,
UpdatedDate datetime,
UpdatedBy int
)


create Procedure mediaDetails
(
@mediafileName varchar(64),
@mediaOriginalName varchar(256),
@mediaExtn varchar(8),
@mediaSize int,
@CreatedDate datetime,
@CreatedBy int,
@UpdatedDate datetime,
@UpdatedBy int
)
As
Begin
insert into MediaMaster (mediafileName,mediaOriginalName,mediaExtn,mediaSize,CreatedBy,CreatedDate,UpdatedDate,UpdatedBy) 
values(@mediafileName,@mediaOriginalName,@mediaExtn,@mediaSize,@CreatedBy,@CreatedDate,@UpdatedDate,@UpdatedBy)
SELECT @@IDENTITY
End

create Procedure updateMediaDetails
(
@ID int,
@mediafileName varchar(64),
@mediaOriginalName varchar(256),
@mediaExtn varchar(8),
@mediaSize int,
@CreatedDate datetime,
@CreatedBy int,
@UpdatedDate datetime,
@UpdatedBy int
)
As
Begin
update MediaMaster set mediafileName = @mediafileName,
mediaOriginalName = @mediaOriginalName,
mediaExtn = @mediaExtn,
mediaSize = @mediaSize,
CreatedBy = @CreatedBy,
CreatedDate = @CreatedDate,
UpdatedBy = @UpdatedBy,
UpdatedDate = @UpdatedDate
where
mediaID = @ID
End

Exec mediaDetails
@mediafileName = 'Jack.jpg',
@mediaOriginalName = 'Aokk' ,
@mediaExtn = '.jpg',
@mediaSize =  563,
@CreatedDate = N'2016-01-27T08:00:00',
@CreatedBy = 1,
@UpdatedDate = N'2016-01-27T08:00:00',
@UpdatedBy = 1

select * from MediaMaster
delete from MediaMaster



Create Table VisiterMaster
(
Visiter_ID int identity(1,1) not null primary key,
VisiterName varchar(64),
VisitorEmail varchar(64),
VisitorPhone varchar(16),
RegisterDate datetime,
UpdateDate datetime
)

create Table ProductServiceMaster
(
PS_ID int identity(1,1) not null primary key,
Ps_Name varchar(128),
ImageID int,
MemberID int,
IsActive bit,             
IsDeleted bit,
CreatedDate date,
CreatedBy int,
UpdateDate date,
UpdatedBy int,
PsDescription varchar(256),
AboutPS varchar(256)
)


create Table EnquiryMaster
(
Lead_ID int identity(1,1) not null primary key,
LeadDate datetime,
SendBy int,
SendTo int,
EnquiryType varchar(256)NOT NULL CHECK (EnquiryType IN('CallBackRequest', 'MicroSiteEnquiry', 'ContactUsPage')),
LeadDescription varchar(256),
SenderName varchar(128),
SenderPhone varchar(16),
SenderEmail varchar(64),
SenderDescription varchar(256)
)

create Table MemberRegistration
(
memberRegistration_ID int identity(1,1) not null primary key,
MemberName varchar(128),
MemeberEmail varchar(64),
MemberPassword varchar(64)
)

create procedure MemberAuthenticationPro
(
@MemeberEmail varchar(64),
@MemberPassword varchar(64)
)
As
Begin
select memberRegistration_ID from MemberRegistration where MemeberEmail = @MemeberEmail  and MemberPassword = @MemberPassword
End



insert into MemberRegistration
values ('Jack','Jack@jack.com','1234')

delete from MemberRegistration

select * from MemberRegistration

Create Table MembersMaster
(
Member_ID int identity(1,1) not null primary key,
memberRegID int references MemberRegistration(memberRegistration_ID),
CompanyName varchar(128),
MemberDesignation varchar(128),
DisplayName varchar(128),
POCName varchar(128),
POCPhoneOne varchar(16),
POCPhoneTwo varchar(16),
TollFreeNumber varchar(20),
FaxNo varchar(20),
Location varchar(20),
Building varchar(20),
Street varchar(20),
Landmark varchar(20),
City varchar(20),
PinCode varchar(16),
MemberState  varchar(20),
MemberCountry varchar(20),
Website varchar(20),
GSTIN varchar(50),
PAN varchar(20),
MemberTAN varchar(30),
Profile varchar(max),
EnquiryEmail varchar(20),
CreatedDate datetime,
CreatedBy int,
IsApproved bit,
IsActive bit,
IsDeleted bit,
UpdatedBy int,
UpdatedDate datetime,
ActivePlanType varchar(20),
MicroCategory int,
PageTitle varchar(128),
MetaKeyword varchar(512),
MetaDescription varchar(256),
--ServiceProviderType varchar(256),
ServiceProviderType varchar(256)NOT NULL CHECK (ServiceProviderType IN('Manugacture', 'Suplier', 'Service Provider')),
TurnOver varchar(256),
--BusinessStatus varchar(256),
BusinessStatus varchar(256)NOT NULL CHECK (BusinessStatus IN('Propriter', 'Pvt. Limited', 'Partnership','Limited','PPL')),
Employee varchar(128),
LogoMediaId int,
BannerImageID int
)
-------------------------------------------
insert into MembersMaster(MemberName,MemeberEmail,MemberPassword)
values('Rack','rack@gmail.com','ddfd')

create Procedure ClientRegistration
(
@MemberName varchar(128),
@MemeberEmail varchar(64),
@MemberPassword varchar(64)
)
As
Begin
Insert into MembersMaster (MemberName,MemeberEmail,MemberPassword)
values(@MemberName,@MemeberEmail,@MemberPassword)
End

Exec ClientRegistration
@MemberName = 'Jack',
@MemeberEmail = 'jack@jack.com',
@MemberPassword = 'jack'


select * from MembersMaster

drop table AdminAssistant

Create Table AdminAssistant
(
aID int identity(1,1) not null primary key,
aName varchar(30) not null,
aPhone varchar(20)not null unique ,
aMail varchar(50)not null unique ,
aAddress varchar(250)not null,
aCity varchar(30),
aGender varchar(20)NOT NULL CHECK (aGender IN('Male', 'Female')),
aPassword varchar(30) not null,
aRoll varchar(30)NOT NULL CHECK (aRoll IN('SuperAdmin', 'Admin','Subscriber')),
mediaID int not null,
registerDate datetime not null,
aStatus bit,
isDeleted bit not null default 0
)
---------Procedures------------------------
create procedure insertAdminData
(
@name varchar(30),
@phone varchar(20),
@mail varchar(50),
@address varchar(250),
@city varchar(30),
@gender varchar(20),
@password varchar(30),
@aRoll varchar(30),
@mediaID int,
@registerDate datetime,
@aStatus bit 
)
As
Begin
insert into AdminAssistant (aName,aPhone,aMail,aAddress,aCity,aGender,aPassword,aRoll,mediaID,registerDate,aStatus) 
values (@name,@phone,@mail,@address,@city,@gender,@password,@aRoll,@mediaID,@registerDate,@aStatus)
SELECT @@IDENTITY 
End
-------------------------------------------
--running store procedure testing
Exec insertAdminData
@name = 'Jack',
@phone = '7503219460', 
@mail = 'Jitender.city@gmail.com', 
@address = 'Bhajan Pura',
@city = 'Delhi',
@gender = 'Male',
@password = '12345',
@aRoll = 'SuperAdmin',
@mediaID = 2,
@registerDate = N'20120720',
@aStatus = 0
SELECT @@IDENTITY   
SELECT SCOPE_IDENTITY()

--------------------------------------
select * from AdminAssistant
select * from MediaMaster

delete from MediaMaster
delete from AdminAssistant

drop table AdminAssistant

insert into AdminAssistant values('Jack','7503219460','jitender.city@gmail.com','Bihari pur','Delhi','Male','1234','Admin','abc','c:\abc',GETDATE(),'Active')

create Table PaymentSubscription
(
p_ID int identity(1,1) not null primary key,
member_ID int references MembersMaster(Member_ID),
subscriptionType varchar(20)NOT NULL CHECK (SubscriptionType IN('Free', 'Premium','Gold','Silver')),
subscriptionDate datetime,
expiryDate datetime,
modeOfPayment varchar(32)NOT NULL CHECK (modeOfPayment IN('CASH', 'ECS','CHEQUE','ONLINE')),
nextDueDate datetime,
nextCollectableAmount int,

)


--Admin Login Check Store Procedure
CREATE PROCEDURE adminLogin
(
@aMail NVARCHAR(50),@aPassword varchar(50)
)
AS
BEGIN
SET NOCOUNT ON;
SELECT  aMail,aPassword
FROM dbo.AdminAssistant
WHERE aMail = @aMail and aPassword = @aPassword and aStatus = 'True' and isDeleted = 'False'
end

----
Exec adminLogin
@aMail = 'jitender.city@gmail.com',
@aPassword = '111'


Exec adminLogin
@aMail = 'suny.city@gmail.com',
@aPassword = '1234'


create procedure getAdminID
(
@aMail varchar(50)
)
AS
BEGIN
SELECT  aID
FROM dbo.AdminAssistant
WHERE aMail = @aMail
SELECT @@IDENTITY  
end

exec getAdminID
@aMail = 'jitender.city@gmail.com'

