using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TourMVC.Models
{
    public partial class TourDBContext : DbContext
    {
        public TourDBContext()
        {
        }

        public TourDBContext(DbContextOptions<TourDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DoanKhachHang> DoanKhachHang { get; set; }
        public virtual DbSet<DoanNhanVien> DoanNhanVien { get; set; }
        public virtual DbSet<GiaTourHienTai> GiaTourHienTai { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<TourChiPhi> TourChiPhi { get; set; }
        public virtual DbSet<TourChiPhiChiTiet> TourChiPhiChiTiet { get; set; }
        public virtual DbSet<TourChiTiet> TourChiTiet { get; set; }
        public virtual DbSet<TourDiaDiem> TourDiaDiem { get; set; }
        public virtual DbSet<TourDoan> TourDoan { get; set; }
        public virtual DbSet<TourGia> TourGia { get; set; }
        public virtual DbSet<TourKhachHang> TourKhachHang { get; set; }
        public virtual DbSet<TourLoai> TourLoai { get; set; }
        public virtual DbSet<TourLoaiChiPhi> TourLoaiChiPhi { get; set; }
        public virtual DbSet<TourNhanVien> TourNhanVien { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TourDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoanKhachHang>(entity =>
            {
                entity.ToTable("Doan_KhachHang");

                entity.Property(e => e.DoanKhachHangId).HasColumnName("Doan_KhachHang_ID");

                entity.Property(e => e.DoanId).HasColumnName("Doan_ID");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHang_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Doan)
                    .WithMany(p => p.DoanKhachHang)
                    .HasForeignKey(d => d.DoanId)
                    .HasConstraintName("Doan_KhachHang_Tour_Doan");

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.DoanKhachHang)
                    .HasForeignKey(d => d.KhachHangId)
                    .HasConstraintName("Doan_KhachHang_Tour_KhachHang");
            });

            modelBuilder.Entity<DoanNhanVien>(entity =>
            {
                entity.ToTable("Doan_NhanVien");

                entity.Property(e => e.DoanNhanVienId).HasColumnName("Doan_NhanVien_ID");

                entity.Property(e => e.DoanId).HasColumnName("Doan_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVien_ID");

                entity.Property(e => e.NhanVienNhiemVu)
                    .IsRequired()
                    .HasColumnName("NhanVien_NhiemVu")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Doan)
                    .WithMany(p => p.DoanNhanVien)
                    .HasForeignKey(d => d.DoanId)
                    .HasConstraintName("Doan_NhanVien_Tour_Doan");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.DoanNhanVien)
                    .HasForeignKey(d => d.NhanVienId)
                    .HasConstraintName("Doan_NhanVien_Tour_NhanVien");
            });

            modelBuilder.Entity<GiaTourHienTai>(entity =>
            {
                entity.HasKey(e => e.TourId)
                    .HasName("PK__Gia_Tour__D436A863E62A311E");

                entity.ToTable("Gia_Tour_HienTai");

                entity.Property(e => e.TourId)
                    .HasColumnName("Tour_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GiaId).HasColumnName("Gia_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Gia)
                    .WithMany(p => p.GiaTourHienTai)
                    .HasForeignKey(d => d.GiaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Gia_Tour_HienTai_Tour_Gia");

                entity.HasOne(d => d.Tour)
                    .WithOne(p => p.GiaTourHienTai)
                    .HasForeignKey<GiaTourHienTai>(d => d.TourId)
                    .HasConstraintName("Gia_Tour_HienTai_Tour");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.TourId).HasColumnName("Tour_ID");

                entity.Property(e => e.LoaiId).HasColumnName("Loai_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TourMoTa)
                    .IsRequired()
                    .HasColumnName("Tour_MoTa")
                    .HasMaxLength(1000);

                entity.Property(e => e.TourTen)
                    .IsRequired()
                    .HasColumnName("Tour_Ten")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Loai)
                    .WithMany(p => p.Tour)
                    .HasForeignKey(d => d.LoaiId)
                    .HasConstraintName("Tour_Tour_Loai");
            });

            modelBuilder.Entity<TourChiPhi>(entity =>
            {
                entity.HasKey(e => e.ChiPhiId)
                    .HasName("PK__Tour_Chi__EBF1394177AB9E10");

                entity.ToTable("Tour_ChiPhi");

                entity.Property(e => e.ChiPhiId).HasColumnName("ChiPhi_ID");

                entity.Property(e => e.ChiPhiTong).HasColumnType("decimal(14, 1)");

                entity.Property(e => e.DoanId).HasColumnName("Doan_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Doan)
                    .WithMany(p => p.TourChiPhi)
                    .HasForeignKey(d => d.DoanId)
                    .HasConstraintName("Tour_ChiPhi_Tour_Doan");
            });

            modelBuilder.Entity<TourChiPhiChiTiet>(entity =>
            {
                entity.HasKey(e => e.ChiPhiChiTietId)
                    .HasName("PK__Tour_Chi__25171F8A9FAF2F11");

                entity.ToTable("Tour_ChiPhi_ChiTiet");

                entity.Property(e => e.ChiPhiChiTietId).HasColumnName("ChiPhi_ChiTiet_ID");

                entity.Property(e => e.ChiPhi).HasColumnType("decimal(13, 1)");

                entity.Property(e => e.ChiPhiId).HasColumnName("ChiPhi_ID");

                entity.Property(e => e.LoaiChiPhiId).HasColumnName("LoaiChiPhi_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ChiPhiNavigation)
                    .WithMany(p => p.TourChiPhiChiTiet)
                    .HasForeignKey(d => d.ChiPhiId)
                    .HasConstraintName("Tour_ChiPhi_ChiTiet_Tour_ChiPhi");

                entity.HasOne(d => d.LoaiChiPhi)
                    .WithMany(p => p.TourChiPhiChiTiet)
                    .HasForeignKey(d => d.LoaiChiPhiId)
                    .HasConstraintName("Tour_ChiPhi_ChiTiet_Tour_LoaiChiPhi");
            });

            modelBuilder.Entity<TourChiTiet>(entity =>
            {
                entity.HasKey(e => e.ChiTietId)
                    .HasName("PK__Tour_Chi__202A63BFCA180F17");

                entity.ToTable("Tour_ChiTiet");

                entity.Property(e => e.ChiTietId).HasColumnName("ChiTiet_ID");

                entity.Property(e => e.ChiTietThuTu).HasColumnName("ChiTiet_ThuTu");

                entity.Property(e => e.DiaDiemId).HasColumnName("DiaDiem_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TourId).HasColumnName("Tour_ID");

                entity.HasOne(d => d.DiaDiem)
                    .WithMany(p => p.TourChiTiet)
                    .HasForeignKey(d => d.DiaDiemId)
                    .HasConstraintName("Tour_ChiTiet_Tour_DiaDiem");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourChiTiet)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("Tour_ChiTiet_Tour");
            });

            modelBuilder.Entity<TourDiaDiem>(entity =>
            {
                entity.HasKey(e => e.DiaDiemId)
                    .HasName("PK__Tour_Dia__4833C9E918951F33");

                entity.ToTable("Tour_DiaDiem");

                entity.Property(e => e.DiaDiemId).HasColumnName("DiaDiem_ID");

                entity.Property(e => e.DiaDiemMoTa)
                    .IsRequired()
                    .HasColumnName("DiaDiem_MoTa")
                    .HasMaxLength(1000);

                entity.Property(e => e.DiaDiemTen)
                    .IsRequired()
                    .HasColumnName("DiaDiem_Ten")
                    .HasMaxLength(100);

                entity.Property(e => e.DiaDiemThanhPho)
                    .IsRequired()
                    .HasColumnName("DiaDiem_ThanhPho")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TourDoan>(entity =>
            {
                entity.HasKey(e => e.DoanId)
                    .HasName("PK__Tour_Doa__A3F3C502D658990A");

                entity.ToTable("Tour_Doan");

                entity.Property(e => e.DoanId).HasColumnName("Doan_ID");

                entity.Property(e => e.DoanChiTiet)
                    .IsRequired()
                    .HasColumnName("Doan_ChiTiet")
                    .HasMaxLength(1000);

                entity.Property(e => e.DoanGiaTour)
                    .HasColumnName("Doan_GiaTour")
                    .HasColumnType("decimal(13, 1)");

                entity.Property(e => e.DoanNgayDi)
                    .HasColumnName("Doan_NgayDi")
                    .HasColumnType("date");

                entity.Property(e => e.DoanNgayVe)
                    .HasColumnName("Doan_NgayVe")
                    .HasColumnType("date");

                entity.Property(e => e.DoanTen)
                    .IsRequired()
                    .HasColumnName("Doan_Ten")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TourId).HasColumnName("Tour_ID");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourDoan)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("Tour_Doan_Tour");
            });

            modelBuilder.Entity<TourGia>(entity =>
            {
                entity.HasKey(e => e.GiaId)
                    .HasName("PK__Tour_Gia__84F8236A20736132");

                entity.ToTable("Tour_Gia");

                entity.Property(e => e.GiaId).HasColumnName("Gia_ID");

                entity.Property(e => e.GiaDenNgay)
                    .HasColumnName("Gia_DenNgay")
                    .HasColumnType("date");

                entity.Property(e => e.GiaSoTien)
                    .HasColumnName("Gia_SoTien")
                    .HasColumnType("decimal(13, 1)");

                entity.Property(e => e.GiaTuNgay)
                    .HasColumnName("Gia_TuNgay")
                    .HasColumnType("date");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TourId).HasColumnName("Tour_ID");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourGia)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("Tour_Gia_Tour");
            });

            modelBuilder.Entity<TourKhachHang>(entity =>
            {
                entity.HasKey(e => e.KhachHangId)
                    .HasName("PK__Tour_Kha__16A1551AA201CE44");

                entity.ToTable("Tour_KhachHang");

                entity.Property(e => e.KhachHangId).HasColumnName("KhachHang_ID");

                entity.Property(e => e.KhachHangChungMinhNhanDan)
                    .IsRequired()
                    .HasColumnName("KhachHang_ChungMinhNhanDan")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.KhachHangEmail)
                    .IsRequired()
                    .HasColumnName("KhachHang_Email")
                    .HasMaxLength(100);

                entity.Property(e => e.KhachHangNgaySinh)
                    .HasColumnName("KhachHang_NgaySinh")
                    .HasColumnType("date");

                entity.Property(e => e.KhachHangSoDienThoai)
                    .IsRequired()
                    .HasColumnName("KhachHang_SoDienThoai")
                    .HasMaxLength(12);

                entity.Property(e => e.KhachHangTen)
                    .IsRequired()
                    .HasColumnName("KhachHang_Ten")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TourLoai>(entity =>
            {
                entity.HasKey(e => e.LoaiId)
                    .HasName("PK__Tour_Loa__09F6C3C90D746885");

                entity.ToTable("Tour_Loai");

                entity.Property(e => e.LoaiId).HasColumnName("Loai_ID");

                entity.Property(e => e.LoaiMoTa)
                    .IsRequired()
                    .HasColumnName("Loai_MoTa")
                    .HasMaxLength(1000);

                entity.Property(e => e.LoaiTen)
                    .IsRequired()
                    .HasColumnName("Loai_Ten")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TourLoaiChiPhi>(entity =>
            {
                entity.HasKey(e => e.LoaiChiPhiId)
                    .HasName("PK__Tour_Loa__B98AEDC8BE04DC0C");

                entity.ToTable("Tour_LoaiChiPhi");

                entity.Property(e => e.LoaiChiPhiId).HasColumnName("LoaiChiPhi_ID");

                entity.Property(e => e.LoaiChiPhiMoTa)
                    .IsRequired()
                    .HasColumnName("LoaiChiPhi_MoTa")
                    .HasMaxLength(1000);

                entity.Property(e => e.LoaiChiPhiSoTien)
                    .HasColumnName("LoaiChiPhi_SoTien")
                    .HasColumnType("decimal(13, 1)");

                entity.Property(e => e.LoaiChiPhiTen)
                    .IsRequired()
                    .HasColumnName("LoaiChiPhi_Ten")
                    .HasMaxLength(100);

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TourNhanVien>(entity =>
            {
                entity.HasKey(e => e.NhanVienId)
                    .HasName("PK__Tour_Nha__A5A85E23B9A40457");

                entity.ToTable("Tour_NhanVien");

                entity.Property(e => e.NhanVienId).HasColumnName("NhanVien_ID");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NhanVienEmail)
                    .IsRequired()
                    .HasColumnName("NhanVien_Email")
                    .HasMaxLength(100);

                entity.Property(e => e.NhanVienNgaySinh)
                    .HasColumnName("NhanVien_NgaySinh")
                    .HasColumnType("date");

                entity.Property(e => e.NhanVienSoDienThoai)
                    .IsRequired()
                    .HasColumnName("NhanVien_SoDienThoai")
                    .HasMaxLength(12);

                entity.Property(e => e.NhanVienTen)
                    .IsRequired()
                    .HasColumnName("NhanVien_Ten")
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
