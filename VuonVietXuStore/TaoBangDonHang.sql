-- ============================================================
-- Script tạo bảng DonHang và ChiTietDonHang
-- Chạy file này trong SQL Server Management Studio
-- Database: VuonVietXuStore
-- ============================================================

USE VuonVietXuStore;
GO

-- ── Bảng DonHang ─────────────────────────────────────────────
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DonHang') AND type = 'U')
BEGIN
    CREATE TABLE DonHang (
        MaDonHang   INT IDENTITY(1,1) PRIMARY KEY,
        MaKH        INT NOT NULL,
        NgayDatHang DATETIME NOT NULL DEFAULT GETDATE(),
        TongTien    DECIMAL(18,2) NOT NULL DEFAULT 0,
        TrangThai   NVARCHAR(50)  NOT NULL DEFAULT N'Hoàn thành',
        GhiChu      NVARCHAR(500) NULL,

        CONSTRAINT FK_DonHang_KhachHang FOREIGN KEY (MaKH)
            REFERENCES KhachHang(MaKH)
    );
    PRINT N'✅ Đã tạo bảng DonHang thành công.';
END
ELSE
BEGIN
    PRINT N'ℹ️  Bảng DonHang đã tồn tại, bỏ qua.';
END
GO

-- ── Bảng ChiTietDonHang ───────────────────────────────────────
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ChiTietDonHang') AND type = 'U')
BEGIN
    CREATE TABLE ChiTietDonHang (
        MaChiTiet   INT IDENTITY(1,1) PRIMARY KEY,
        MaDonHang   INT NOT NULL,
        MaSP        INT NOT NULL,
        SoLuong     INT NOT NULL DEFAULT 1,
        GiaBan      DECIMAL(18,2) NOT NULL DEFAULT 0,

        CONSTRAINT FK_ChiTietDonHang_DonHang FOREIGN KEY (MaDonHang)
            REFERENCES DonHang(MaDonHang),
        CONSTRAINT FK_ChiTietDonHang_SanPham FOREIGN KEY (MaSP)
            REFERENCES SanPham(MaSP)
    );
    PRINT N'✅ Đã tạo bảng ChiTietDonHang thành công.';
END
ELSE
BEGIN
    PRINT N'ℹ️  Bảng ChiTietDonHang đã tồn tại, bỏ qua.';
END
GO

-- ── Bảng LichSuKho (tùy chọn, dùng cho tab Điều chỉnh kho) ───
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'LichSuKho') AND type = 'U')
BEGIN
    CREATE TABLE LichSuKho (
        MaLSK           INT IDENTITY(1,1) PRIMARY KEY,
        MaSP            INT NOT NULL,
        LoaiDieuChinh   NVARCHAR(10) NOT NULL,   -- 'Nhập' hoặc 'Xuất'
        SoLuong         INT NOT NULL,
        LyDo            NVARCHAR(500) NULL,
        NgayDieuChinh   DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_LichSuKho_SanPham FOREIGN KEY (MaSP)
            REFERENCES SanPham(MaSP)
    );
    PRINT N'✅ Đã tạo bảng LichSuKho thành công.';
END
ELSE
BEGIN
    PRINT N'ℹ️  Bảng LichSuKho đã tồn tại, bỏ qua.';
END
GO

PRINT N'';
PRINT N'🎉 Hoàn tất! Hãy chạy lại ứng dụng và thử lại tab Bán hàng và Kho hàng.';
