using System;
using System.Collections.Generic;

namespace TachLayout.Models;

public partial class ChiTietHoaDon
{
    public int ChiTietId { get; set; }

    public int MaHd { get; set; }

    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual HoaDon MaHdNavigation { get; set; }

    public virtual SanPham MaSpNavigation { get; set; }
}
