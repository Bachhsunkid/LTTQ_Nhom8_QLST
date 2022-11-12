using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nhom8_BTL_QLST.Model
{
    class Chuong
    {
        private string maChuong;
        private string maLoai;
        private string maKhu;
        private int dienTich;
        private int chieuCao;
        private string maTrangThai;
        private string maNhanVien;
        private string ghiChu;

        public string MaChuong { get => maChuong; set => maChuong = value; }
        public string MaLoai { get => maLoai; set => maLoai = value; }
        public string MaKhu { get => maKhu; set => maKhu = value; }
        public int DienTich { get => dienTich; set => dienTich = value; }
        public int ChieuCao { get => chieuCao; set => chieuCao = value; }
        public string MaTrangThai { get => maTrangThai; set => maTrangThai = value; }
        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
    }
}
