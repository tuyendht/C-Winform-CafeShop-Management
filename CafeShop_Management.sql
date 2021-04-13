USE MASTER
IF EXISTS (SELECT * FROM sys.databases WHERE name='CafeShop_Managerment')
	DROP DATABASE CafeShop_Managerment
GO
CREATE DATABASE CafeShop_Managerment
GO
USE CafeShop_Managerment
GO

CREATE TABLE tblBill
(
	billID INT IDENTITY(1,1) NOT NULL,
	dateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	username VARCHAR(30) NOT NULL,
	billStatus BIT NOT NULL DEFAULT 0, -- 1 Đã thanh toán || 0 Chưa thanh toán 
	constraint pk_bill primary key(billID)
)
CREATE TABLE tblBillInfo
(
	idInfo INT IDENTITY(1,1) NOT NULL,
	billID INT NOT NULL,
	proID VARCHAR(30) NOT NULL,
	quantity INT NOT NULL,
	unitPrice FLOAT NOT NULL,
	constraint pk_BillInf primary key(idInfo)
)

CREATE TABLE tblUsers
(
	username VARCHAR(30) NOT NULL,
	fullname NVARCHAR(250) NOT NULL,
	password VARCHAR(20) NOT NULL,
	isAdmin BIT NOT NULL DEFAULT 0,
	constraint pk_User primary key(username)
)
CREATE TABLE tblCategory
(
	cateID VARCHAR(30) NOT NULL,
	cateName NVARCHAR(250) NOT NULL,
	constraint pk_Cate primary key(cateID)
)
CREATE TABLE tblProduct
(
	proID VARCHAR(30) NOT NULL,
	cateID VARCHAR(30) NOT NULL,
	proName NVARCHAR(250) NOT NULL,
	unitPrice FLOAT NOT NULL,
	constraint pk_Prodct primary key(proID)
)
ALTER TABLE tblProduct ADD CONSTRAINT fk01_PRO FOREIGN KEY(cateID) REFERENCES tblCategory(cateID)
ALTER TABLE tblBill ADD CONSTRAINT fk01_BILL FOREIGN KEY(username) REFERENCES tblUsers(username)
ALTER TABLE tblBillInfo ADD CONSTRAINT fk01_BILLInfo FOREIGN KEY(billID) REFERENCES tblBill(billID)
ALTER TABLE tblBillInfo ADD CONSTRAINT fk02_BillInfo FOREIGN KEY(proID) REFERENCES tblProduct(proID)


INSERT INTO tblUsers VALUES('admin',N'Đặng Hà Trung Tuyển','123',1)
INSERT INTO tblUsers VALUES('user1',N'Trần Công Minh Hiếu','123','')
INSERT INTO tblUsers VALUES('user2',N'Lê Đỗ Minh Hiển','123','')
INSERT INTO tblUsers VALUES('user3',N'Nguyễn Tuấn Hoàng','123','')

INSERT INTO tblCategory VALUES('SP01',N'Coffe')
INSERT INTO tblCategory VALUES('SP02',N'Tea')
INSERT INTO tblCategory VALUES('SP03',N'Ice Blender')
INSERT INTO tblCategory VALUES('SP04',N'Cake')
INSERT INTO tblCategory VALUES('SP05',N'Sandwitch')

INSERT INTO tblProduct VALUES('CF01','SP01',N'Americano',55000)
INSERT INTO tblProduct VALUES('CF02','SP01',N'Cappuccino',55000)
INSERT INTO tblProduct VALUES('CF03','SP01',N'Macchiato',53000)
INSERT INTO tblProduct VALUES('CF04','SP01',N'Espresso',53000)
INSERT INTO tblProduct VALUES('CF05','SP01',N'Mocha',50000)

INSERT INTO tblProduct VALUES('TE01','SP02',N'Black Tea',45000)
INSERT INTO tblProduct VALUES('TE02','SP02',N'Grean Tea',45000)
INSERT INTO tblProduct VALUES('TE03','SP02',N'Lemon & Ginger Tea',45000)
INSERT INTO tblProduct VALUES('TE04','SP02',N'Honey Lime Tea',49000)
INSERT INTO tblProduct VALUES('TE05','SP02',N'Bluebery Rose Mint Tea',49000)

INSERT INTO tblProduct VALUES('IB01','SP03',N'Oreo Milkshakes',49000)
INSERT INTO tblProduct VALUES('IB02','SP03',N'Frozen Blueberry',49000)
INSERT INTO tblProduct VALUES('IB03','SP03',N'Java Chip',45000)
INSERT INTO tblProduct VALUES('IB04','SP03',N'White Chocolate Ice Blended',45000)
INSERT INTO tblProduct VALUES('IB05','SP03',N'Matcha Smoothie',42000)

INSERT INTO tblProduct VALUES('CK01','SP04',N'Chocolate Cake',39000)
INSERT INTO tblProduct VALUES('CK02','SP04',N'Tiramisu',35000)
INSERT INTO tblProduct VALUES('CK03','SP04',N'Black Forest',35000)
INSERT INTO tblProduct VALUES('CK04','SP04',N'Red Velvet',35000)
INSERT INTO tblProduct VALUES('CK05','SP04',N'Caramel',32000)

INSERT INTO tblProduct VALUES('SW01','SP05',N'Chiken Mayo & Crispy Bacon',45000)
INSERT INTO tblProduct VALUES('SW02','SP05',N'Crispy Bacon & Egg Mayo',45000)
INSERT INTO tblProduct VALUES('SW03','SP05',N'Crispy Bacon & Avocado',43000)
INSERT INTO tblProduct VALUES('SW04','SP05',N'Smoked Salmon & Cream Cheese',45000)
INSERT INTO tblProduct VALUES('SW05','SP05',N'Ham & Cheese',43000)

INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('01/17/2021','user3',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('02/20/2021','user2',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('02/12/2021','user2',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('01/10/2021','user1',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('03/09/2021','user1',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('01/05/2021','admin',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('02/22/2021','user1',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('03/20/2021','user3',1)
INSERT INTO tblBill(dateCheckIn, username, billStatus) VALUES ('01/29/2021','user2',1)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(1,'IB01',2,49000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(1,'CF02',1,55000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(1,'SW01',1,45000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(2,'CK01',1,39000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(2,'TE04',3,49000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(2,'SW05',2,43000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(3,'TE03',1,45000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(3,'CK05',2,32000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(4,'SW04',3,45000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(5,'IB04',1,45000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(5,'TE04',1,49000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(5,'CF01',1,55000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(5,'CK02',3,35000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(6,'CK03',1,35000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(6,'SW03',1,43000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(6,'CF05',2,50000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(7,'SW01',2,45000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(7,'IB02',2,49000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(8,'CF04',2,53000)
INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(8,'CF02',3,55000)

INSERT INTO tblBillInfo(billID, proID, quantity, unitPrice) VALUES(9,'SW04',3,45000)

--------------------------------------------------------
