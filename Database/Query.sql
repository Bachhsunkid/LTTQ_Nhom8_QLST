﻿create view SachDo as
select * from Thu where Thu.SachDo = 1

create view ThuBiOm as
select Thu.* from Thu join Thu_SuKien on Thu.MaThu = Thu_SuKien.MaThu
		join SuKien on SuKien.MaSuKien = Thu_SuKien.MaSuKien
		where SuKien.TenSuKien = N'Ốm'

create function LocThu(@loaithu nvarchar(20)) returns table as
return( select Thu.* from Thu join Loai on Thu.MaLoai = Loai.MaLoai 
		where Loai.TenLoai = @loaithu)

create or alter function TimThu(@mathu nvarchar(20)) returns table as
return( select * from Thu where Thu.MaThu = @mathu)

-----
create function TimThuTheoTen(@tenthu nvarchar(20)) returns table
return (
select * from Thu where Thu.TenThu = @tenthu
)

go

select * from TimThuTheoTen(N'Voi')

go
create or alter function Timthutheoloai(@loaithu nvarchar(20)) returns table
return(
select Thu.* from Thu join Loai on thu.MaLoai = loai.MaLoai
where Loai.TenLoai = @loaithu
)


go
select * from Timthutheoloai(N'Voi')

go
create function Timthutheokieusinh(@tenks nvarchar(20)) returns table 
return(
select Thu.* from Thu join KieuSinh on Thu.MaKieuSinh = KieuSinh.MaKieuSinh
where KieuSinh.TenKieuSinh = @tenks
)

go
select * from Timthutheokieusinh(N'Đẻ trứng')

go
create function Timthutheonguongoc(@tenng nvarchar(20)) returns table 
return(
select Thu.* from Thu join NguonGoc on Thu.MaNguonGoc = NguonGoc.MaNguonGoc
where NguonGoc.TenNguonGoc = @tenng
)

go
select * from Timthutheonguongoc(N'Châu Phi')

----------------Câu 5---------------------
go
create function Timchuongtheomathu(@mathu nvarchar(20)) returns table 
return(
	select Chuong.* from Chuong join Loai on Loai.MaLoai = Chuong.MaLoai
	join Thu on Thu.MaLoai = Loai.MaLoai
	where Thu.MaThu = @mathu
)

go
select * from Timchuongtheomathu(N'Th014')

go
create function Timchuongtheoslthu(@slthu int) returns table 
return(
	select Chuong.* from Chuong join Loai on Chuong.MaLoai = Loai.MaLoai
	join Thu on Thu.MaLoai = Loai.MaLoai
	where thu.SoLuong = @slthu
)

go
select * from Timchuongtheoslthu(1)

go
create function Timchuongtheonvtrongcoi(@tennv nvarchar(20)) returns table
return(
	select Chuong.* from Chuong join NhanVien on Chuong.MaNhanVien = NhanVien.MaNhanVien
	where NhanVien.TenNhanVien like N'%'+@tennv
)

go
select * from Timchuongtheonvtrongcoi(N'Anh')

--------------Câu 9-------------------
--Theo mã thú
go
create procedure Tinhchiphitheomathu @mathu nvarchar(20),@tt int output as
begin
	select  @tt = sum(a.Tongtien)
	from (
	select Thu.MaThu,sum(Thu_ThucAn.SoLuongSang*ThucAn_Gia.DonGia) as "Tongtien",'TAS' as Loai
	from Thu join Thu_ThucAn on Thu.MaThu = Thu_ThucAn.MaThu 
	join ThucAn on ThucAn.MaThucAn = Thu_ThucAn.MaThucAnSang
	join ThucAn_Gia on ThucAn.MaThucAn= ThucAn_Gia.MaThucAn
	group by Thu.MaThu
	union 
	select Thu.MaThu,sum(Thu_ThucAn.SoLuongSang*ThucAn_Gia.DonGia),'TATrua' as Loai
	from Thu join Thu_ThucAn on Thu.MaThu = Thu_ThucAn.MaThu 
	join ThucAn on ThucAn.MaThucAn = Thu_ThucAn.MaThucAnTrua
	join ThucAn_Gia on ThucAn.MaThucAn= ThucAn_Gia.MaThucAn
	group by Thu.MaThu
	union 
	select Thu.MaThu,sum(Thu_ThucAn.SoLuongSang*ThucAn_Gia.DonGia),'TAToi' as Loai
	from Thu join Thu_ThucAn on Thu.MaThu = Thu_ThucAn.MaThu 
	join ThucAn on ThucAn.MaThucAn = Thu_ThucAn.MaThucAnToi
	join ThucAn_Gia on ThucAn.MaThucAn= ThucAn_Gia.MaThucAn
	group by Thu.MaThu)a
	where a.MaThu = @mathu
	group by a.MaThu

end
--theo ngày trong tháng
go
create procedure Tinhchiphitheongay @ngay datetime,@tt int output as
begin

	select @tt = sum(a.Tongtien)
	from (
	select thucan_gia.thang_namapdung ,sum(Thu_ThucAn.SoLuongSang*ThucAn_Gia.DonGia) as "Tongtien",'TAS' as Loai
	from Thu join Thu_ThucAn on Thu.MaThu = Thu_ThucAn.MaThu 
	join ThucAn on ThucAn.MaThucAn = Thu_ThucAn.MaThucAnSang
	join ThucAn_Gia on ThucAn.MaThucAn= ThucAn_Gia.MaThucAn
	group by thucan_gia.thang_namapdung
	union 
	select thucan_gia.thang_namapdung,sum(Thu_ThucAn.SoLuongSang*ThucAn_Gia.DonGia),'TATrua' as Loai
	from Thu join Thu_ThucAn on Thu.MaThu = Thu_ThucAn.MaThu 
	join ThucAn on ThucAn.MaThucAn = Thu_ThucAn.MaThucAnTrua
	join ThucAn_Gia on ThucAn.MaThucAn= ThucAn_Gia.MaThucAn
	group by thucan_gia.thang_namapdung
	union 
	select thucan_gia.thang_namapdung,sum(Thu_ThucAn.SoLuongSang*ThucAn_Gia.DonGia),'TAToi' as Loai
	from Thu join Thu_ThucAn on Thu.MaThu = Thu_ThucAn.MaThu 
	join ThucAn on ThucAn.MaThucAn = Thu_ThucAn.MaThucAnToi
	join ThucAn_Gia on ThucAn.MaThucAn= ThucAn_Gia.MaThucAn
	group by thucan_gia.thang_namapdung)a
	where a.thang_namapdung = @ngay
	group by a.thang_namapdung
end
----------------Cac quyery dung chung------------------
----------------Nhan su------------------
----------------Thu------------------
-- Lay ra danh sach thu -> fill vao data grid view Thu
select distinct * from Thu
select * from NguonGoc
create or alter view View_DanhSachThu as
	select MaThu, TenThu, MaLoai, SoLuong, SachDo, TenKhoaHoc, TenTA, MaKieuSinh, GioiTinh, NgayVao, MaNguonGoc, DacDiem, NgaySinh, TuoiTho, Anh
	from thu
select * from View_DanhSachThu
----------------Chuong------------------
----------------Bao cao------------------