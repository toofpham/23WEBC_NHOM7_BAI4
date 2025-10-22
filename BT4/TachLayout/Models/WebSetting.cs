using System;
using System.Collections.Generic;

namespace TachLayout.Models;

public partial class WebSetting
{
    public int WebSettingId { get; set; }

    public string TenSite { get; set; }

    public string Logo { get; set; }

    public string DiaChi { get; set; }

    public string Email { get; set; }

    public string Hotline { get; set; }

    public int? DungLuongToiDa { get; set; }

    public int? RequestToiDa { get; set; }
}
