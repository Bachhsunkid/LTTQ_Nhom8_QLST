using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom8_BTL_QLST.Model
{
    class NhanVien
    {
        private string maNhanVien;
        private string tenNhanVien;
        private string soDienThoai;
        private DateTime ngaySinh;
        private int gioiTinh;
        private string diaChi;

        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string TenNhanVien { get => tenNhanVien; set => tenNhanVien = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public DateTime NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public int GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}
