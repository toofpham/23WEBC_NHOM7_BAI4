using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TachLayout.Models;

public partial class QuanLyBanHangContext : DbContext
{
    public QuanLyBanHangContext()
    {
    }

    public QuanLyBanHangContext(DbContextOptions<QuanLyBanHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WebSetting> WebSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=YOUNGTOOF;Initial Catalog=QuanLyBanHang;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.HasKey(e => e.BinhLuanId).HasName("PK__BinhLuan__54F56E503E50C1D1");

            entity.ToTable("BinhLuan");

            entity.Property(e => e.BinhLuanId).HasColumnName("BinhLuanID");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK_BinhLuan_SanPham");

            entity.HasOne(d => d.User).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_BinhLuan_User");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.ChiTietId).HasName("PK__ChiTietH__B117E9EA248C6FC1");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.ChiTietId).HasColumnName("ChiTietID");
            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", true)
                .HasColumnType("decimal(29, 2)");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_HoaDon");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietHoaDon_SanPham");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887658E3CD4");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.TenDanhMuc)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E02363802C");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNguoiNhan).HasMaxLength(255);
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xử lý");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_HoaDon_User");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081C6B42CAD3");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.Gia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiaKm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GiaKM");
            entity.Property(e => e.Hinh).HasMaxLength(255);
            entity.Property(e => e.TagName).HasMaxLength(255);
            entity.Property(e => e.TenSp)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("TenSP");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .HasConstraintName("FK_SanPham_DanhMuc");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__657CFA4C0FE65253");

            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.TagName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACF02A3E07");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Pass)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Customer");
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)1);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<WebSetting>(entity =>
        {
            entity.HasKey(e => e.WebSettingId).HasName("PK__WebSetti__5AF810C97D776C9A");

            entity.ToTable("WebSetting");

            entity.Property(e => e.WebSettingId).HasColumnName("WebSettingID");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Hotline).HasMaxLength(20);
            entity.Property(e => e.Logo).HasMaxLength(255);
            entity.Property(e => e.TenSite).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
