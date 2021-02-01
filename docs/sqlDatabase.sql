CREATE TABLE Roles (
    Id int IDENTITY(1,1) PRIMARY KEY,
    TenRole nvarchar(255) NOT NULL,
    MoTa nvarchar(255)
)
CREATE TABLE Users (
    Id int IDENTITY(1,1) PRIMARY KEY,
    HoTen nvarchar(255) NOT NULL,
    DiaChi nvarchar(500) NOT NULL,
	SoDienThoai nvarchar(50) NOT NULL,
	TenDangNhap nvarchar(50) NOT NULL,
	MatKhau nvarchar(50) NOT NULL,
	Quyen int NOT NULL,
	IsActive int NOT NULL,
	CONSTRAINT FK_User_Roles FOREIGN KEY (Quyen)
    REFERENCES Roles(Id)
)
CREATE TABLE LoaiSanPham (
    MaLoai int IDENTITY(1,1) PRIMARY KEY ,
    TenLoai nvarchar(255) NOT NULL,
    MoTa nvarchar(255),
    LinkAnh nvarchar(250)
)
CREATE TABLE SanPham (
    MaSanPham int IDENTITY(1,1) PRIMARY KEY,
    TenSanPham nvarchar(255) NOT NULL,
    Gia int NOT NULL ,
	SoLuong int NOT NULL,
	MoTa nvarchar(1000) ,
	LinkAnh nvarchar(500) NOT NULL,
	KhuyenMai int ,
	IsActive int NOT NULL ,
	MaLoai int NOT NULL,
	CONSTRAINT FK_SanPham_LoaiSP FOREIGN KEY (MaLoai)
    REFERENCES LoaiSanPham(MaLoai)
)
CREATE TABLE DonHang (
    MaDonHang int IDENTITY(1,1) PRIMARY KEY,
    NgayTao  date DEFAULT GETDATE(),
    SoLuongSanPham int NOT NULL,
	TongTien int NOT NULL,
	GhiChu nvarchar(500),
	IsAccept int NOT NULL,
	IdUser int NOT NULL,
	CONSTRAINT FK_DonHang_User FOREIGN KEY (IdUser)
    REFERENCES Users(Id)
)
CREATE TABLE ChiTietDonHang (
    Id int IDENTITY(1,1) PRIMARY KEY,
    MaSanPham int NOT NULL ,
    SoLuong int NOT NULL,
	MaDonHang int NOT NULL,
	CONSTRAINT FK_CTDonHang_DonHang FOREIGN KEY (MaDonHang)
    REFERENCES DonHang(MaDonHang),
	CONSTRAINT FK_CTDonHang_SanPham FOREIGN KEY (MaSanPham)
    REFERENCES SanPham(MaSanPham)
)
CREATE TABLE HoaDon (
    MaHoaDon int IDENTITY(1,1) PRIMARY KEY,
    NgayLap date DEFAULT GETDATE(),
    MaDonHang int NOT NULL,
	IdUser int NOT NULL,
	GhiChu nvarchar(500),
	TongTien int NOT NULL,
	CONSTRAINT FK_HoaDon_Users FOREIGN KEY (IdUser)
    REFERENCES Users(Id),
	CONSTRAINT FK_HoaDon_DonHang FOREIGN KEY (MaDonHang)
    REFERENCES DonHang(MaDonHang)	
)