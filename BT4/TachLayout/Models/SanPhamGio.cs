namespace TachLayout.Models
{
    public class SanPhamGio
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public string Hinh { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => Gia * SoLuong;
    }
}
