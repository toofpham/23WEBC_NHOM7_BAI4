using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TachLayout.Models;

[Table("WebSetting")]
public partial class WebSetting
{
    [Key] // Khóa chính
    public int WebSettingId { get; set; }

    public string TenSite { get; set; }

    public string Logo { get; set; }

    public string DiaChi { get; set; }

    public string Email { get; set; }

    public string HotLine { get; set; }

    public int? DungLuongToiDa { get; set; }

    public int? RequestToiDa { get; set; }
}
