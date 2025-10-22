using System;
using System.Collections.Generic;

namespace TachLayout.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string TenSp { get; set; }

    public decimal Gia { get; set; }

    public decimal? GiaKm { get; set; }

    public int? MaDanhMuc { get; set; }

    public string Hinh { get; set; }

    public string MoTa { get; set; }

    public string TagName { get; set; }

    public int SoLuong { get; set; }

    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual DanhMuc MaDanhMucNavigation { get; set; }
}
