using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BE.Models
{
    public partial class db_websitebanhangContext : DbContext
    {
        public db_websitebanhangContext()
        {
        }

        public db_websitebanhangContext(DbContextOptions<db_websitebanhangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; } = null!;
        public virtual DbSet<Chitietsanpham> Chitietsanphams { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Danhmuc> Danhmucs { get; set; } = null!;
        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Khuyenmai> Khuyenmais { get; set; } = null!;
        public virtual DbSet<Nhanhieu> Nhanhieus { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<Trangthai> Trangthais { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=db_websitebanhang.mssql.somee.com;packet size=4096;user id=huanhuynh_SQLLogin_1;pwd=l47d3tg9sr;data source=db_websitebanhang.mssql.somee.com;persist security info=False;initial catalog=db_websitebanhang;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.MaBlog)
                    .HasName("PK__Blog__E62FCFE48A9255C9");

                entity.ToTable("Blog");

                entity.Property(e => e.MaBlog)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maBlog")
                    .IsFixedLength();

                entity.Property(e => e.Hinhanh)
                    .HasMaxLength(100)
                    .HasColumnName("hinhanh");

                entity.Property(e => e.MaDanhmuc).HasColumnName("maDanhmuc");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("date")
                    .HasColumnName("ngaytao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Noidung)
                    .HasColumnType("text")
                    .HasColumnName("noidung");

                entity.Property(e => e.Tieude)
                    .HasMaxLength(100)
                    .HasColumnName("tieude");

                entity.HasOne(d => d.MaDanhmucNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.MaDanhmuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Blog__maDanhmuc__45F365D3");
            });

            modelBuilder.Entity<Chitiethoadon>(entity =>
            {
                entity.HasKey(e => e.IdChitietHd)
                    .HasName("PK__Chitieth__0D92AED0066E8064");

                entity.ToTable("Chitiethoadon");

                entity.Property(e => e.IdChitietHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_chitietHD")
                    .IsFixedLength();

                entity.Property(e => e.IdChitietSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_chitietSP")
                    .IsFixedLength();

                entity.Property(e => e.MaHoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maHoadon")
                    .IsFixedLength();

                entity.Property(e => e.SoLuong).HasColumnName("soLuong");

                entity.Property(e => e.TrangThai).HasColumnName("trangThai");

                entity.HasOne(d => d.IdChitietSpNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.IdChitietSp)
                    .HasConstraintName("FK__Chitietho__id_ch__656C112C");

                entity.HasOne(d => d.MaHoadonNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.MaHoadon)
                    .HasConstraintName("FK__Chitietho__maHoa__6477ECF3");
            });

            modelBuilder.Entity<Chitietsanpham>(entity =>
            {
                entity.HasKey(e => e.IdChitietSp)
                    .HasName("PK__Chitiets__0D92386CB14C4876");

                entity.ToTable("Chitietsanpham");

                entity.Property(e => e.IdChitietSp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_chitietSP")
                    .IsFixedLength();

                entity.Property(e => e.Gia)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("gia");

                entity.Property(e => e.HinhAnh)
                    .HasMaxLength(100)
                    .HasColumnName("hinhAnh");

                entity.Property(e => e.MaMau).HasColumnName("maMau");

                entity.Property(e => e.MaSanpham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSanpham")
                    .IsFixedLength();

                entity.Property(e => e.MaSize).HasColumnName("maSize");

                entity.Property(e => e.SoLuongTon).HasColumnName("soLuongTon");

                entity.HasOne(d => d.MaMauNavigation)
                    .WithMany(p => p.Chitietsanphams)
                    .HasForeignKey(d => d.MaMau)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chitietsa__maMau__5070F446");

                entity.HasOne(d => d.MaSanphamNavigation)
                    .WithMany(p => p.Chitietsanphams)
                    .HasForeignKey(d => d.MaSanpham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chitietsa__maSan__4F7CD00D");

                entity.HasOne(d => d.MaSizeNavigation)
                    .WithMany(p => p.Chitietsanphams)
                    .HasForeignKey(d => d.MaSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Chitietsa__maSiz__5165187F");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.MaMau)
                    .HasName("PK__Color__27572EAEDC6FBB8A");

                entity.ToTable("Color");

                entity.Property(e => e.MaMau)
                    .ValueGeneratedNever()
                    .HasColumnName("maMau");

                entity.Property(e => e.TenMau)
                    .HasMaxLength(50)
                    .HasColumnName("tenMau");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.MaComment)
                    .HasName("PK__Comment__F138E06A38EA652E");

                entity.ToTable("Comment");

                entity.Property(e => e.MaComment)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maComment")
                    .IsFixedLength();

                entity.Property(e => e.MaBlog)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maBlog")
                    .IsFixedLength();

                entity.Property(e => e.MaCmCha)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ma_cmCha")
                    .IsFixedLength();

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasColumnName("ngayTao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoiDung)
                    .HasColumnType("text")
                    .HasColumnName("noiDung");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(20)
                    .HasColumnName("taikhoan");

                entity.HasOne(d => d.MaBlogNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MaBlog)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__maBlog__5629CD9C");

                entity.HasOne(d => d.TaikhoanNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Taikhoan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__taikhoa__5535A963");
            });

            modelBuilder.Entity<Danhmuc>(entity =>
            {
                entity.HasKey(e => e.MaDanhmuc)
                    .HasName("PK__Danhmuc__62CC157E651A48D8");

                entity.ToTable("Danhmuc");

                entity.Property(e => e.MaDanhmuc)
                    .ValueGeneratedNever()
                    .HasColumnName("maDanhmuc");

                entity.Property(e => e.TenDanhmuc)
                    .HasMaxLength(50)
                    .HasColumnName("tenDanhmuc");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.MaHoadon)
                    .HasName("PK__Hoadon__3D68D7A87D81D7B3");

                entity.ToTable("Hoadon");

                entity.Property(e => e.MaHoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maHoadon")
                    .IsFixedLength();

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .HasColumnName("diaChi");

                entity.Property(e => e.IdTrangthai).HasColumnName("id_Trangthai");

                entity.Property(e => e.NgayGiao)
                    .HasColumnType("date")
                    .HasColumnName("ngayGiao");

                entity.Property(e => e.NgayTao)
                    .HasColumnType("date")
                    .HasColumnName("ngayTao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(20)
                    .HasColumnName("taikhoan");

                entity.Property(e => e.TongTien)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("tongTien");

                entity.HasOne(d => d.IdTrangthaiNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.IdTrangthai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Hoadon__id_Trang__5FB337D6");

                entity.HasOne(d => d.TaikhoanNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.Taikhoan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Hoadon__taikhoan__60A75C0F");
            });

            modelBuilder.Entity<Khuyenmai>(entity =>
            {
                entity.HasKey(e => e.MaKhuyenmai)
                    .HasName("PK__Khuyenma__BF71FFDB6F1C1EFC");

                entity.ToTable("Khuyenmai");

                entity.Property(e => e.MaKhuyenmai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhuyenmai")
                    .IsFixedLength();

                entity.Property(e => e.PhanTram).HasColumnName("phanTram");

                entity.Property(e => e.TenKhuyenmai)
                    .HasMaxLength(50)
                    .HasColumnName("tenKhuyenmai");
            });

            modelBuilder.Entity<Nhanhieu>(entity =>
            {
                entity.HasKey(e => e.MaNhan)
                    .HasName("PK__Nhanhieu__83085ED009DF0814");

                entity.ToTable("Nhanhieu");

                entity.Property(e => e.MaNhan)
                    .ValueGeneratedNever()
                    .HasColumnName("maNhan");

                entity.Property(e => e.TenNhan)
                    .HasMaxLength(50)
                    .HasColumnName("tenNhan");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.MaReview)
                    .HasName("PK__Review__E7CAD939985A4CFA");

                entity.ToTable("Review");

                entity.Property(e => e.MaReview)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maReview")
                    .IsFixedLength();

                entity.Property(e => e.MaSanpham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSanpham")
                    .IsFixedLength();

                entity.Property(e => e.NgayNhap)
                    .HasColumnType("date")
                    .HasColumnName("ngayNhap")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoiDung)
                    .HasColumnType("text")
                    .HasColumnName("noiDung");

                entity.Property(e => e.SoSao)
                    .HasColumnName("soSao")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(20)
                    .HasColumnName("taikhoan");

                entity.HasOne(d => d.MaSanphamNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MaSanpham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__maSanpha__5BE2A6F2");

                entity.HasOne(d => d.TaikhoanNavigation)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Taikhoan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__taikhoan__5AEE82B9");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSanpham)
                    .HasName("PK__Sanpham__BE132EC9805F5A97");

                entity.ToTable("Sanpham");

                entity.Property(e => e.MaSanpham)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maSanpham")
                    .IsFixedLength();

                entity.Property(e => e.MaDanhmuc).HasColumnName("maDanhmuc");

                entity.Property(e => e.MaKhuyenmai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhuyenmai")
                    .IsFixedLength();

                entity.Property(e => e.MaNhan).HasColumnName("maNhan");

                entity.Property(e => e.MoTa)
                    .HasColumnType("text")
                    .HasColumnName("moTa");

                entity.Property(e => e.SoLuongyeuthich).HasColumnName("soLuongyeuthich");

                entity.Property(e => e.TenSanpham)
                    .HasMaxLength(100)
                    .HasColumnName("tenSanpham");

                entity.HasOne(d => d.MaDanhmucNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaDanhmuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sanpham__maDanhm__4AB81AF0");

                entity.HasOne(d => d.MaKhuyenmaiNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaKhuyenmai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sanpham__maKhuye__4BAC3F29");

                entity.HasOne(d => d.MaNhanNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaNhan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sanpham__maNhan__49C3F6B7");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.MaSize)
                    .HasName("PK__Size__D83773A39ACF66DD");

                entity.ToTable("Size");

                entity.Property(e => e.MaSize)
                    .ValueGeneratedNever()
                    .HasColumnName("maSize");

                entity.Property(e => e.Chieucao).HasColumnName("chieucao");

                entity.Property(e => e.Chieudai).HasColumnName("chieudai");

                entity.Property(e => e.Chieurong).HasColumnName("chieurong");
            });

            modelBuilder.Entity<Trangthai>(entity =>
            {
                entity.ToTable("Trangthai");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TenTrangthai)
                    .HasMaxLength(50)
                    .HasColumnName("tenTrangthai");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Taikhoan)
                    .HasName("PK__Users__FE7B87316BDC3622");

                entity.Property(e => e.Taikhoan)
                    .HasMaxLength(20)
                    .HasColumnName("taikhoan");

                entity.Property(e => e.DiaChi)
                    .HasMaxLength(100)
                    .HasColumnName("diaChi");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Matkhau)
                    .HasMaxLength(20)
                    .HasColumnName("matkhau");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sdt")
                    .IsFixedLength();

                entity.Property(e => e.Ten)
                    .HasMaxLength(50)
                    .HasColumnName("ten");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
