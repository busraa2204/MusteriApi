CREATE DATABASE MusteriDB;
GO

USE MusteriDB;
GO

CREATE TABLE Musteriler (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Ad NVARCHAR(50) NOT NULL,
    Soyad NVARCHAR(50) NOT NULL,
    Eposta NVARCHAR(100) NOT NULL,
    TelefonNumarasi NVARCHAR(20),
    Adres NVARCHAR(200),
    OlusturmaTarihi DATETIME NOT NULL DEFAULT GETDATE(),
    GuncellemeTarihi DATETIME NULL,
    MusteriVerisi NVARCHAR(MAX)
);
GO

-- Stored Procedures
CREATE PROCEDURE sp_TumMusterileriGetir
AS
BEGIN
    SELECT * FROM Musteriler;
END
GO

CREATE PROCEDURE sp_MusteriGetirById
    @Id INT
AS
BEGIN
    SELECT * FROM Musteriler WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_MusteriEkle
    @Ad NVARCHAR(50),
    @Soyad NVARCHAR(50),
    @Eposta NVARCHAR(100),
    @TelefonNumarasi NVARCHAR(20),
    @Adres NVARCHAR(200),
    @MusteriVerisi NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Musteriler (Ad, Soyad, Eposta, TelefonNumarasi, Adres, MusteriVerisi)
    VALUES (@Ad, @Soyad, @Eposta, @TelefonNumarasi, @Adres, @MusteriVerisi);
    
    SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE sp_MusteriGuncelle
    @Id INT,
    @Ad NVARCHAR(50),
    @Soyad NVARCHAR(50),
    @Eposta NVARCHAR(100),
    @TelefonNumarasi NVARCHAR(20),
    @Adres NVARCHAR(200),
    @MusteriVerisi NVARCHAR(MAX)
AS
BEGIN
    UPDATE Musteriler
    SET Ad = @Ad,
        Soyad = @Soyad,
        Eposta = @Eposta,
        TelefonNumarasi = @TelefonNumarasi,
        Adres = @Adres,
        MusteriVerisi = @MusteriVerisi,
        GuncellemeTarihi = GETDATE()
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_MusteriSil
    @Id INT
AS
BEGIN
    DELETE FROM Musteriler WHERE Id = @Id;
END
GO 