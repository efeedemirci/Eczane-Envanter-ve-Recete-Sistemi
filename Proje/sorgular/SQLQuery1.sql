CREATE DATABASE Eczane;

use Eczane;

CREATE TABLE Hastalar (
    HastaID INT PRIMARY KEY IDENTITY,
    Ad VARCHAR(50),
    Soyad VARCHAR(50),
    DogumTarihi DATE,
	ReceteID INT,
	DoktorID INT,
	EczaciID INT,
	FOREIGN KEY (ReceteID) REFERENCES Receteler(ReceteID),
	FOREIGN KEY (DoktorID) REFERENCES Doktorlar(DoktorID),
	FOREIGN KEY (EczaciID) REFERENCES Eczacilar(EczaciID)
);


CREATE TABLE Doktorlar (
    DoktorID INT PRIMARY KEY IDENTITY,
    Ad VARCHAR(50),
    Soyad VARCHAR(50),
    Uzmanlik VARCHAR(100)
);

CREATE TABLE Eczacilar (
    EczaciID INT PRIMARY KEY IDENTITY,
    Ad VARCHAR(50),
    Soyad VARCHAR(50),
    LisansNo CHAR(8) CHECK (DATALENGTH(LisansNo) = 8)
);

CREATE TABLE Firmalar (
    FirmaID INT PRIMARY KEY IDENTITY,
    FirmaAdi VARCHAR(100),
    Sehir VARCHAR(100)
);

CREATE TABLE Ilaclar (
    IlacID INT PRIMARY KEY IDENTITY,
    IlacAdi VARCHAR(100),
    Adet INT,
    Ucret DECIMAL(10, 2),
    FirmaID INT,
    FOREIGN KEY (FirmaID) REFERENCES Firmalar(FirmaID)
);

CREATE TABLE Receteler (
    ReceteID INT PRIMARY KEY IDENTITY,
    IlacID INT,
    Tarih DATE,
    Recete VARCHAR(200),
    FOREIGN KEY (IlacID) REFERENCES Ilaclar(IlacID),
);

CREATE TRIGGER SilinenHastaReçetesi
ON Hastalar
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ReceteID INT;
    SELECT @ReceteID = ReceteID FROM deleted;

    DELETE FROM Receteler WHERE ReceteID = @ReceteID;
END;

ALTER TABLE Hastalar
ALTER COLUMN ReceteID INT NULL;

CREATE TRIGGER SilinenReceteHastasi
ON Receteler
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DeletedReceteID INT;
    SELECT @DeletedReceteID = ReceteID FROM deleted;


    UPDATE Hastalar
    SET ReceteID = NULL
    WHERE ReceteID = @DeletedReceteID;

    
    DELETE FROM Receteler
    WHERE ReceteID = @DeletedReceteID;
END;

SELECT IlacAdi, Adet, Ucret, FirmaAdi
FROM Ilaclar
JOIN Firmalar ON Ilaclar.FirmaID = Firmalar.FirmaID;

CREATE PROCEDURE GetAllReceteler
AS
BEGIN
    SELECT * FROM Receteler;
END;
GO

CREATE PROCEDURE AddRecete
    @Tarih DATE,
    @Recete VARCHAR(200),
    @IlacID INT
AS
BEGIN
    INSERT INTO Receteler (Tarih, Recete, IlacID)
    VALUES (@Tarih, @Recete, @IlacID);
END;
GO

CREATE PROCEDURE DeleteRecete
    @ReceteID INT
AS
BEGIN
    DELETE FROM Receteler
    WHERE ReceteID = @ReceteID;
END;
GO

CREATE PROCEDURE UpdateRecete
    @ReceteID INT,
    @Tarih DATE,
    @Recete VARCHAR(200),
    @IlacID INT
AS
BEGIN
    UPDATE Receteler
    SET Tarih = @Tarih, Recete = @Recete, IlacID = @IlacID
    WHERE ReceteID = @ReceteID;
END;
GO


SELECT DISTINCT I.IlacAdi, F.FirmaAdi
FROM Ilaclar I
INNER JOIN Firmalar F ON I.FirmaID = F.FirmaID
INNER JOIN (
    SELECT IlacAdi
    FROM Ilaclar
    GROUP BY IlacAdi
    HAVING COUNT(*) > 1
) AS Duplicates ON I.IlacAdi = Duplicates.IlacAdi
ORDER BY I.IlacAdi, F.FirmaAdi;
--ayný ilaçlarýn firmalarýný gösteren sorgu

CREATE VIEW V_HastaRecete AS
SELECT H.HastaID, H.Ad, H.Soyad, H.DogumTarihi, R.ReceteID, R.Tarih, R.Recete
FROM Hastalar H
INNER JOIN Receteler R ON H.ReceteID = R.ReceteID;

SELECT * FROM V_HastaRecete ORDER BY Tarih DESC;

SELECT * FROM Hastalar h where year(h.DogumTarihi)<1999;

CREATE VIEW EczacilarGizliView AS
SELECT EczaciID, Ad, Soyad
FROM Eczacilar;

SELECT * FROM EczacilarGizliView;
--eczacilarin lisansnosunu gizleyip listeler

SELECT 
    h.Ad AS HastaAdi, 
    h.Soyad AS HastaSoyadi, 
    r.Recete AS Recete, 
    i.IlacAdi AS IlacAdi
FROM 
    Hastalar h
JOIN 
    Receteler r ON h.ReceteID = r.ReceteID
JOIN 
    Ilaclar i ON r.IlacID = i.IlacID
GROUP BY 
    h.Ad, h.Soyad, r.Recete, i.IlacAdi
ORDER BY 
    h.Ad, h.Soyad;

--hasta adý soyadý reçete ve ilaç adý listeleyen sorgu
