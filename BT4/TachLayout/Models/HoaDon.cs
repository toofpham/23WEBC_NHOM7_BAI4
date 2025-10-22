using System;
using System.Collections.Generic;

namespace TachLayout.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int? UserId { get; set; }

    public string TenNguoiNhan { get; set; }

    public string Sdt { get; set; }

    public string DiaChi { get; set; }

    public DateTime NgayLap { get; set; }

    public decimal TongTien { get; set; }

    public string TrangThai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual User User { get; set; }
}
