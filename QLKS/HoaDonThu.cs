//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKS
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDonThu
    {
        public int ID { get; set; }
        public string IDNhanVien { get; set; }
        public string IDKhachHang { get; set; }
        public string LyDo { get; set; }
        public string DachSachPhong { get; set; }
        public Nullable<decimal> TongTien { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}
