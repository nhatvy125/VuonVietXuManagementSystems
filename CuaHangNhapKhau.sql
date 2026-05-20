USE VuonVietXuStore;
GO

-- 1. Tạo bảng users
CREATE TABLE users (
    username VARCHAR(50) PRIMARY KEY,
    password VARCHAR(50) NOT NULL
);
GO

-- 2. Thêm thử một tài khoản mặc định để bạn test đăng nhập
INSERT INTO users (username, password) 
VALUES ('tracy', '123');
GO
-- =========================================
-- 1. TẠO DANH SÁCH TÊN THỰC TẾ (BẢNG TẠM)
-- =========================================
IF OBJECT_ID('tempdb..#RealNames') IS NOT NULL DROP TABLE #RealNames;
CREATE TABLE #RealNames (ID INT IDENTITY(1,1), FullName NVARCHAR(200), Origin NVARCHAR(100), CategoryName NVARCHAR(100));

INSERT INTO #RealNames (FullName, Origin, CategoryName)
SELECT DISTINCT TOP 200
    P.Name + ' ' + G.Grade + ' (' + Q.Pack + ')', P.Origin, P.DM
FROM 
    (VALUES 
        (N'Cherry đỏ chủng Bing', N'Mỹ', N'Trái cây'), 
        (N'Lê Hàn Quốc Evergood', N'Hàn Quốc', N'Trái cây'),
        (N'Thăn lưng bò Black Angus', N'Mỹ', N'Thịt bò'), 
        (N'Bắp hoa bò Úc Kilcoy', N'Úc', N'Thịt bò'),
        (N'Sò điệp Hokkaido L-size', N'Nhật Bản', N'Hải sản'), 
        (N'Cá hồi Atlantic Fillet', N'Na Uy', N'Hải sản'),
        (N'Sữa hạnh nhân Blue Diamond', N'Mỹ', N'Sữa tươi'),
        (N'Rượu vang Cabernet Sauvignon', N'Chile', N'Rượu vang'),
        (N'Chocolate Lindt Excellence', N'Thụy Sĩ', N'Bánh kẹo'),
        (N'Mì Ý Barilla Linguine', N'Ý', N'Đồ khô'),
        (N'Phô mai Brie President', N'Pháp', N'Đồ khô'),
        (N'Hạt dẻ cười Wonderful', N'Mỹ', N'Hạt')
    ) AS P(Name, Origin, DM)
CROSS JOIN (VALUES (N'Premium'), (N'Organic'), (N'Selected')) AS G(Grade)
CROSS JOIN (VALUES (N'Hộp 1kg'), (N'Chai 750ml'), (N'Túi 500g'), (N'Khay 300g')) AS Q(Pack);

-- =========================================
-- 2. CẬP NHẬT TRỰC TIẾP VÀO BẢNG SANPHAM
-- =========================================
;WITH CTE_Target AS (
    SELECT MaSP, TenSP, XuatXu, GiaBan, GiaNhap, MaDanhMuc,
           ROW_NUMBER() OVER (ORDER BY MaSP) as Rn
    FROM SanPham
)
UPDATE T
SET 
    T.TenSP = R.FullName,
    T.XuatXu = R.Origin,
    T.GiaBan = T.GiaNhap * 1.35,
    -- Cập nhật lại MaDanhMuc dựa trên tên Danh Mục thực tế
    T.MaDanhMuc = (SELECT TOP 1 MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = R.CategoryName)
FROM CTE_Target T
INNER JOIN #RealNames R ON T.Rn = R.ID;

-- =========================================
-- 3. CẬP NHẬT NHÀ CUNG CẤP QUỐC TẾ
-- =========================================
;WITH CTE_NCC AS (
    SELECT MaNCC, ROW_NUMBER() OVER (ORDER BY MaNCC) as Rn FROM NhaCungCap
)
UPDATE N
SET 
    N.TenNCC = T.Name + ' ' + B.Suf,
    N.QuocGia = T.Country,
    N.DiaChiNCC = CAST(C.Rn * 21 AS NVARCHAR) + ' ' + T.Addr,
    N.SDT = '0' + CAST(111222333 + C.Rn AS VARCHAR) -- Đảm bảo Unique 10 số
FROM NhaCungCap N
JOIN CTE_NCC C ON N.MaNCC = C.MaNCC
CROSS JOIN (
    VALUES (N'Global Agri Corp', N'USA', N'California St, San Francisco'),
           (N'Hokkaido Marine Ltd', N'Japan', N'Sapporo Center'),
           (N'Euro Gourmet Group', N'France', N'Rue de Rivoli, Paris'),
           (N'Oceania Meat Co', N'Australia', N'George St, Sydney')
) AS T(Name, Country, Addr)
CROSS JOIN (VALUES (N'Global'), (N'International'), (N'Logistics')) AS B(Suf)
WHERE C.Rn = C.Rn;

-- =========================================
-- 4. CẬP NHẬT KHÁCH HÀNG (SĐT UNIQUE 10 SỐ)
-- =========================================
;WITH CTE_KH AS (
    SELECT MaKH, ROW_NUMBER() OVER (ORDER BY MaKH) as Rn FROM KhachHang
)
UPDATE K
SET K.SDT = '0' + CAST(910000000 + C.Rn AS VARCHAR)
FROM KhachHang K JOIN CTE_KH C ON K.MaKH = C.MaKH;

-- =========================================
-- 5. KIỂM TRA KẾT QUẢ (DÙNG JOIN ĐỂ HIỆN TÊN DANH MỤC)
-- =========================================
SELECT TOP 20 
    S.MaSP, 
    S.TenSP, 
    D.TenDanhMuc, -- Lấy từ bảng DanhMuc qua JOIN
    S.XuatXu, 
    N.TenNCC as NhaCungCap
FROM SanPham S
JOIN DanhMuc D ON S.MaDanhMuc = D.MaDanhMuc
JOIN NhaCungCap N ON S.MaNCC = N.MaNCC
ORDER BY S.MaSP;

DROP TABLE #RealNames;

USE VuonVietXuStore;
GO

-- 1. XÓA BẢNG CŨ VÀ TẠO LẠI (Đảm bảo cấu trúc sạch 100%)
IF OBJECT_ID('ChiTietPhieuNhap', 'U') IS NOT NULL DROP TABLE ChiTietPhieuNhap;
IF OBJECT_ID('ChiTietDH', 'U') IS NOT NULL DROP TABLE ChiTietDH;
IF OBJECT_ID('PhieuNhap', 'U') IS NOT NULL DROP TABLE PhieuNhap;
IF OBJECT_ID('DonHang', 'U') IS NOT NULL DROP TABLE DonHang;
IF OBJECT_ID('SanPham', 'U') IS NOT NULL DROP TABLE SanPham;
IF OBJECT_ID('KhachHang', 'U') IS NOT NULL DROP TABLE KhachHang;
IF OBJECT_ID('NhaCungCap', 'U') IS NOT NULL DROP TABLE NhaCungCap;
IF OBJECT_ID('DanhMuc', 'U') IS NOT NULL DROP TABLE DanhMuc;
GO

CREATE TABLE DanhMuc (MaDanhMuc INT IDENTITY(1,1) PRIMARY KEY, TenDanhMuc NVARCHAR(100) NOT NULL);
CREATE TABLE NhaCungCap (MaNCC INT IDENTITY(1,1) PRIMARY KEY, TenNCC NVARCHAR(100) NOT NULL, SDT VARCHAR(15), DiaChiNCC NVARCHAR(255), QuocGia NVARCHAR(100));
CREATE TABLE SanPham (MaSP INT IDENTITY(1,1) PRIMARY KEY, TenSP NVARCHAR(100) NOT NULL, MaDanhMuc INT REFERENCES DanhMuc(MaDanhMuc), MaNCC INT REFERENCES NhaCungCap(MaNCC), SoLuongTon INT DEFAULT 0, GiaNhap DECIMAL(18,2), GiaBan DECIMAL(18,2), XuatXu NVARCHAR(100), HSD DATE);
CREATE TABLE KhachHang (MaKH INT IDENTITY(1,1) PRIMARY KEY, TenKH NVARCHAR(100) NOT NULL, SDT VARCHAR(15), DiaChiKH NVARCHAR(255));
CREATE TABLE DonHang (MaDH INT IDENTITY(1,1) PRIMARY KEY, MaKH INT REFERENCES KhachHang(MaKH), NgayDatHang DATE, TongTien DECIMAL(18,2));
CREATE TABLE ChiTietDH (MaDH INT REFERENCES DonHang(MaDH), MaSP INT REFERENCES SanPham(MaSP), SoLuong INT, Gia DECIMAL(18,2), PRIMARY KEY (MaDH, MaSP));
CREATE TABLE PhieuNhap (MaPhieuNhap INT IDENTITY(1,1) PRIMARY KEY, MaNCC INT REFERENCES NhaCungCap(MaNCC), NgayNhap DATE, TongTien DECIMAL(18,2));
CREATE TABLE ChiTietPhieuNhap (MaPhieuNhap INT REFERENCES PhieuNhap(MaPhieuNhap), MaSP INT REFERENCES SanPham(MaSP), SoLuong INT, GiaNhap DECIMAL(18,2), PRIMARY KEY (MaPhieuNhap, MaSP));
GO

-- 2. INSERT DANH MỤC & NCC
INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Trái cây'), (N'Thịt bò'), (N'Hải sản'), (N'Sữa tươi'), (N'Gia vị'), (N'Rượu vang'), (N'Bánh kẹo'), (N'Đồ khô'), (N'Hạt'), (N'Đồ hộp');
INSERT INTO NhaCungCap (TenNCC, QuocGia) VALUES (N'NCC Mỹ', 'USA'), (N'NCC Nhật', 'Japan'), (N'NCC Hàn', 'Korea'), (N'NCC Úc', 'Australia'), (N'NCC Pháp', 'France');
GO

-- 3. SINH 50 SẢN PHẨM (Lấy ID thực tế từ bảng DanhMuc và NCC)
DECLARE @i INT = 1;
DECLARE @MaDM_Min INT = (SELECT MIN(MaDanhMuc) FROM DanhMuc), @MaDM_Max INT = (SELECT MAX(MaDanhMuc) FROM DanhMuc);
DECLARE @MaNCC_Min INT = (SELECT MIN(MaNCC) FROM NhaCungCap), @MaNCC_Max INT = (SELECT MAX(MaNCC) FROM NhaCungCap);

WHILE @i <= 50
BEGIN
    INSERT INTO SanPham (TenSP, MaDanhMuc, MaNCC, SoLuongTon, GiaNhap, GiaBan, XuatXu, HSD)
    VALUES (
        N'Sản phẩm ' + CAST(@i AS NVARCHAR(5)), 
        (ABS(CHECKSUM(NEWID())) % (@MaDM_Max - @MaDM_Min + 1)) + @MaDM_Min,
        (ABS(CHECKSUM(NEWID())) % (@MaNCC_Max - @MaNCC_Min + 1)) + @MaNCC_Min,
        ABS(CHECKSUM(NEWID())) % 200 + 50,
        (ABS(CHECKSUM(NEWID())) % 500 + 100) * 1000, 
        0, N'Nhập khẩu', DATEADD(DAY, 300, GETDATE()));
    SET @i += 1;
END
UPDATE SanPham SET GiaBan = GiaNhap * 1.3;
GO

-- 4. SINH 50 KHÁCH HÀNG
DECLARE @j INT = 1;
WHILE @j <= 50
BEGIN
    INSERT INTO KhachHang (TenKH, SDT) VALUES (N'Khách hàng ' + CAST(@j AS NVARCHAR(5)), '090' + CAST(1000000 + @j AS VARCHAR));
    SET @j += 1;
END
GO

-- 5. SINH 200 ĐƠN HÀNG (Doanh thu rải đều trong 6 tháng để Dashboard đẹp)
DECLARE @k INT = 1;
DECLARE @MaKH_Min INT = (SELECT MIN(MaKH) FROM KhachHang), @MaKH_Max INT = (SELECT MAX(MaKH) FROM KhachHang);

WHILE @k <= 200
BEGIN
    -- Ngày ngẫu nhiên từ 180 ngày trước tới nay
    DECLARE @Ngay DATE = DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 180), GETDATE());
    INSERT INTO DonHang (MaKH, NgayDatHang, TongTien) 
    VALUES ((ABS(CHECKSUM(NEWID())) % (@MaKH_Max - @MaKH_Min + 1)) + @MaKH_Min, @Ngay, 0);
    
    DECLARE @currDH INT = SCOPE_IDENTITY();
    -- Mỗi đơn 1-3 món
    INSERT INTO ChiTietDH (MaDH, MaSP, SoLuong, Gia)
    SELECT TOP (ABS(CHECKSUM(NEWID())) % 3 + 1) @currDH, MaSP, (ABS(CHECKSUM(NEWID())) % 5 + 1), GiaBan 
    FROM SanPham ORDER BY NEWID();
    
    UPDATE DonHang SET TongTien = (SELECT SUM(SoLuong*Gia) FROM ChiTietDH WHERE MaDH = @currDH) WHERE MaDH = @currDH;
    SET @k += 1;
END
GO

-- 6. KIỂM TRA KẾT QUẢ
SELECT N'Đã sinh dữ liệu thành công!' AS ThongBao;
SELECT (SELECT COUNT(*) FROM SanPham) AS TongSP, 
       (SELECT COUNT(*) FROM DonHang) AS TongDonHang,
       (SELECT SUM(TongTien) FROM DonHang) AS TongDoanhThu;


USE VuonVietXuStore;
GO

-- SINH 100 PHIẾU NHẬP (Để Dashboard chi phí và nhập kho xịn sò)
DECLARE @n INT = 1;
DECLARE @MaNCC_Min INT = (SELECT MIN(MaNCC) FROM NhaCungCap), @MaNCC_Max INT = (SELECT MAX(MaNCC) FROM NhaCungCap);
DECLARE @MaSP_Min INT = (SELECT MIN(MaSP) FROM SanPham), @MaSP_Max INT = (SELECT MAX(MaSP) FROM SanPham);

WHILE @n <= 100
BEGIN
    -- Ngày nhập ngẫu nhiên trong 6 tháng qua
    DECLARE @NgayNhap DATE = DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 180), GETDATE());
    
    INSERT INTO PhieuNhap (MaNCC, NgayNhap, TongTien) 
    VALUES (
        (ABS(CHECKSUM(NEWID())) % (@MaNCC_Max - @MaNCC_Min + 1)) + @MaNCC_Min, 
        @NgayNhap, 
        0
    );
    
    DECLARE @currPN INT = SCOPE_IDENTITY();
    
    -- Mỗi phiếu nhập 3-7 loại sản phẩm khác nhau
    INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaSP, SoLuong, GiaNhap)
    SELECT TOP (ABS(CHECKSUM(NEWID())) % 5 + 3) 
           @currPN, 
           MaSP, 
           (ABS(CHECKSUM(NEWID())) % 50 + 20), -- Nhập mỗi loại từ 20-70 cái
           GiaNhap
    FROM SanPham 
    ORDER BY NEWID();
    
    -- Cập nhật tổng tiền cho Phiếu Nhập
    UPDATE PhieuNhap 
    SET TongTien = (SELECT SUM(SoLuong * GiaNhap) FROM ChiTietPhieuNhap WHERE MaPhieuNhap = @currPN) 
    WHERE MaPhieuNhap = @currPN;

    SET @n += 1;
END
GO

-- KIỂM TRA DỮ LIỆU NHẬP HÀNG
SELECT 
    (SELECT COUNT(*) FROM PhieuNhap) AS TongPhieuNhap,
    (SELECT SUM(TongTien) FROM PhieuNhap) AS TongChiPhiNhapHang,
    (SELECT COUNT(*) FROM ChiTietPhieuNhap) AS SoDongChiTietNhap;

SELECT TOP 10 p.MaPhieuNhap, n.TenNCC, p.NgayNhap, p.TongTien
FROM PhieuNhap p
JOIN NhaCungCap n ON p.MaNCC = n.MaNCC
ORDER BY p.NgayNhap DESC;


-- =========================================
-- 2. CẬP NHẬT KHÁCH HÀNG (SĐT 10 số, bắt đầu bằng 0, Unique)
-- =========================================
;WITH CTE_KH AS (
    SELECT MaKH, 
           ROW_NUMBER() OVER (ORDER BY MaKH) as Rn
    FROM KhachHang
)
UPDATE k
SET 
    k.TenKH = H.Ho + ' ' + L.Lot + ' ' + T.Ten,
    k.SDT = '0' + CAST(900000000 + c.Rn AS VARCHAR), -- Đảm bảo 10 số, bắt đầu bằng 0
    k.DiaChiKH = N'Số ' + CAST(c.Rn * 2 AS NVARCHAR) + ' ' + D.Duong + N', TP.HCM'
FROM KhachHang k
JOIN CTE_KH c ON k.MaKH = c.MaKH
CROSS JOIN (VALUES (N'Nguyễn'), (N'Trần'), (N'Lê'), (N'Phạm')) AS H(Ho)
CROSS JOIN (VALUES (N'Minh'), (N'Hoàng'), (N'Thị'), (N'Văn')) AS L(Lot)
CROSS JOIN (VALUES (N'An'), (N'Bình'), (N'Chi'), (N'Dũng'), (N'Em')) AS T(Ten)
CROSS JOIN (VALUES (N'Nguyễn Trãi'), (N'Cách Mạng Tháng 8'), (N'Điện Biên Phủ')) AS D(Duong);

-- =========================================
-- CẬP NHẬT NHÀ CUNG CẤP TÊN RIÊNG BIỆT (QUỐC TẾ)
-- =========================================
;WITH CTE_NCC_Data AS (
    SELECT * FROM (
        VALUES 
            (1, N'Global Agri-Foods Corp', N'USA', N'101 California St, San Francisco', '0912345678'),
            (2, N'Hokkaido Marine Products', N'Japan', N'5-2 Chuo-ku, Sapporo', '0922345679'),
            (3, N'Aussie Prime Beef Ltd', N'Australia', N'25 George St, Sydney', '0933456780'),
            (4, N'European Gourmet Group', N'France', N'12 Rue de Rivoli, Paris', '0944567891'),
            (5, N'K-Food Worldwide Export', N'Korea', N'88 Sejong-daero, Seoul', '0955678902')
    ) AS Source(ID, NewName, NewCountry, NewAddr, NewPhone)
)
UPDATE N
SET 
    N.TenNCC = D.NewName,
    N.QuocGia = D.NewCountry,
    N.DiaChiNCC = D.NewAddr,
    N.SDT = D.NewPhone
FROM NhaCungCap N
JOIN (
    -- Khớp dòng dựa trên số thứ tự hiện có trong bảng của bạn
    SELECT MaNCC, ROW_NUMBER() OVER (ORDER BY MaNCC) as Rn
    FROM NhaCungCap
) AS Target ON N.MaNCC = Target.MaNCC
JOIN CTE_NCC_Data D ON Target.Rn = D.ID;





USE VuonVietXuStore;
GO

-- =========================================
-- CẬP NHẬT KHÁCH HÀNG: TÊN THẬT - SĐT CHUẨN - ĐỊA CHỈ THỰC
-- =========================================
;WITH CTE_Source AS (
    -- Tạo danh sách tổ hợp tên thực tế
    SELECT 
        H.Ho + ' ' + L.Lot + ' ' + T.Ten AS HoTen,
        D.Duong,
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Rn
    FROM (VALUES (N'Nguyễn'), (N'Trần'), (N'Lê'), (N'Phạm'), (N'Hoàng'), (N'Võ'), (N'Phan'), (N'Đặng')) AS H(Ho)
    CROSS JOIN (VALUES (N'Minh'), (N'Thanh'), (N'Ngọc'), (N'Văn'), (N'Thị'), (N'Đức'), (N'Quốc')) AS L(Lot)
    CROSS JOIN (VALUES (N'Anh'), (N'Tuấn'), (N'Lan'), (N'Mai'), (N'Dũng'), (N'Hương'), (N'Vy'), (N'Nam')) AS T(Ten)
    CROSS JOIN (VALUES (N'Lê Văn Sỹ'), (N'Cách Mạng Tháng 8'), (N'Nguyễn Trãi'), (N'Điện Biên Phủ'), (N'Phan Xích Long')) AS D(Duong)
),
CTE_Dest AS (
    -- Lấy danh sách khách hàng hiện có trong bảng
    SELECT 
        MaKH, 
        ROW_NUMBER() OVER (ORDER BY MaKH) AS Rn
    FROM KhachHang
)
UPDATE K
SET 
    K.TenKH = S.HoTen,
    -- SĐT: Bắt đầu từ 0, đảm bảo duy nhất và đủ 10 chữ số (09 + 8 số ngẫu nhiên dựa trên ID)
    K.SDT = '09' + CAST(10000000 + S.Rn AS VARCHAR),
    -- Địa chỉ: Số nhà thực tế + Tên đường thực tế
    K.DiaChiKH = N'Số ' + CAST(ABS(CHECKSUM(CAST(NEWID() AS VARBINARY))) % 300 + 1 AS NVARCHAR) + ' ' + S.Duong + N', TP.HCM'
FROM KhachHang K
JOIN CTE_Dest D ON K.MaKH = D.MaKH
JOIN CTE_Source S ON D.Rn = S.Rn;

-- =========================================
-- KIỂM TRA LẠI DỮ LIỆU KHÁCH HÀNG
-- =========================================
SELECT TOP 20 
    MaKH, 
    TenKH, 
    SDT, 
    DiaChiKH 
FROM KhachHang 
ORDER BY MaKH;

-- Kiểm tra xem có SĐT nào không bắt đầu bằng 0 hoặc không đủ 10 số không
SELECT 
    COUNT(*) AS SoLuongLoi 
FROM KhachHang 
WHERE LEN(SDT) <> 10 OR SDT NOT LIKE '0%';



USE VuonVietXuStore;
GO

-- KHAI BÁO BIẾN
DECLARE @i INT = 1;
DECLARE @MaxPN INT = 100; 
DECLARE @RandomMaSP INT;
DECLARE @RandomSoLuong INT;
DECLARE @RandomGiaNhap DECIMAL(18,2);

-- VÒNG LẶP CHẠY 100 LẦN
WHILE @i <= @MaxPN
BEGIN
    -- Lấy ngẫu nhiên 1 MaSP ĐÃ TỒN TẠI trong bảng SanPham
    SET @RandomMaSP = NULL;
    SELECT TOP 1 @RandomMaSP = MaSP FROM SanPham ORDER BY NEWID();

    -- Chỉ chạy Insert nếu lấy được sản phẩm
    IF @RandomMaSP IS NOT NULL
    BEGIN
        -- Random Số Lượng từ 10 đến 500
        SET @RandomSoLuong = ABS(CHECKSUM(NEWID())) % 491 + 10;

        -- Random Giá Nhập từ 50,000 đến 1,000,000 (làm tròn chẵn hàng ngàn)
        SET @RandomGiaNhap = (ABS(CHECKSUM(NEWID())) % 951 + 50) * 1000;

        -- Kiểm tra xem MaPhieuNhap hiện tại có tồn tại trong bảng PhieuNhap không
        IF EXISTS (SELECT 1 FROM PhieuNhap WHERE MaPhieuNhap = @i)
        BEGIN
            -- Chèn dữ liệu theo đúng chuẩn tên cột mới của bạn
            INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaSP, SoLuong, GiaNhap)
            VALUES (@i, @RandomMaSP, @RandomSoLuong, @RandomGiaNhap);
        END
    END

    -- Tăng biến đếm lên 1 để qua Phiếu Nhập tiếp theo
    SET @i = @i + 1;
END

PRINT N'Đã hoàn tất việc sinh ngẫu nhiên chi tiết cho 100 phiếu nhập!';
GO

-- Xem lại kết quả sau khi chạy
SELECT 
    ct.MaPhieuNhap, 
    sp.TenSP, 
    ct.SoLuong, 
    ct.GiaNhap
FROM ChiTietPhieuNhap ct
JOIN SanPham sp ON ct.MaSP = sp.MaSP
ORDER BY ct.MaPhieuNhap;
GO