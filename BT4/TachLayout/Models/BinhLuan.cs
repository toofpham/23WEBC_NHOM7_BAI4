using System;
using System.Collections.Generic;

namespace TachLayout.Models;

public partial class BinhLuan
{
    public int BinhLuanId { get; set; }

    public int? UserId { get; set; }

    public int? MaSp { get; set; }

    public string Content { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual SanPham MaSpNavigation { get; set; }

    public virtual User User { get; set; }
}
