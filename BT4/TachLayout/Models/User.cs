using System;
using System.Collections.Generic;

namespace TachLayout.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Pass { get; set; }

    public byte TrangThai { get; set; }

    public string Role { get; set; }

    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
