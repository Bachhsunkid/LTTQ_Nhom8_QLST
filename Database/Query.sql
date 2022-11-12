create view SachDo as
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
create or alter view View_NhanVien as
	select nhanvien.manhanvien N'Mã nhân viên', tennhanvien  N'Tên nhân viên', 
	DienThoai  N'Số điện thoại', NgaySinh  N'Ngày sinh', 
	case gioitinh 
		when 1 then N'Nam'
		when 0 then N'Nữ'
		else N'Khác'
	end as N'Giới tính', DiaChi  N'Địa chỉ'--, machuong  N'Mã chuồng'
	from NhanVien --join chuong on chuong.manhanvien = nhanvien.manhanvien

-- nhan vien dang bi thieu gioi tinh -> chay lenh duoi di
alter table NhanVien 
add GioiTinh int

update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV01'
update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV02'
update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV03'
update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV04'
update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV05'
update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV06'
update NhanVien set GioiTinh = 1 where MaNhanVien = N'NV07'

select * from NhanVien
----------------Thu------------------
-- view DanhSachThu ->fill vao datagridview

create or alter view view_thu as
	select thu.mathu as N'Mã thú', TenThu , TenLoai , SoLuong as N'Số lượng',
	MaChuong as N'Mã chuồng', TenKhoaHoc as N'Tên khoa học', TenTA as N'Tên tiếng anh', TenKieuSinh, 
	GioiTinh as N'Giới tính', SachDo as N'Sách đỏ', thu.NgayVao as N'Ngày vào', TenNguonGoc, DacDiem as N'Đặc điểm', 
	NgaySinh as N'Ngày sinh', TuoiTho as N'Tuổi thọ', Anh as N'Ảnh'
	from Thu join loai on thu.MaLoai = loai.MaLoai
			join NguonGoc on thu.MaNguonGoc = NguonGoc.MaNguonGoc
			join KieuSinh on thu.MaKieuSinh = KieuSinh.MaKieuSinh
			join Thu_Chuong on thu.MaThu = Thu_Chuong.MaThu
select * from View_thu
--trigger xoa thu
create or alter trigger xoaThu on Thu
instead of delete as
begin 
	declare @mathu nvarchar(20) 
	select @mathu = mathu from deleted
	delete from Thu_Chuong where MaThu = @mathu
	delete from Thu_ThucAn where MaThu = @mathu
	delete from Thu_SuKien where MaThu = @mathu
	delete from Thu where MaThu = @mathu
end

delete from Thu where mathu = N'Th011'

--trigger cap nhat so luong thu
create or alter trigger Trg_ThuChuong_CapNhatSLThu on Thu_Chuong
for insert, delete, update as
begin
	declare @machuong_IN nvarchar(20)
	declare @machuong_DE nvarchar(20)

	select @machuong_IN = machuong from inserted
	select @machuong_DE = machuong from deleted


	update Chuong set SoLuongThu = ISNULL(SoLuongThu, 0) + ISNULL(thu.SoLuong,0)
	from inserted join Thu on inserted.MaThu = Thu.MaThu
	where Chuong.MaChuong = @machuong_IN

	update Chuong set SoLuongThu = ISNULL(SoLuongThu, 0) + ISNULL(thu.SoLuong,0)
	from inserted join Thu on inserted.MaThu = Thu.MaThu
	where Chuong.MaChuong = @machuong_DE

end

create or alter procedure Proc_Thu_filter(@ten nvarchar(255), @loai nvarchar(255), @kieusinh nvarchar(255), @nguongoc nvarchar(255))
as begin
	declare @query nvarchar(255)

	if @ten = ''
		set @ten = ''
	else
		set @ten = ' and TenThu = N'''+@ten+''' '

	if @loai = ''
		set @loai = ''
	else
		set @loai = ' and TenLoai = N'''+@loai+''' '

	if @kieusinh = ''
		set @kieusinh = ''
	else
		set @kieusinh = ' and TenKieuSinh = N'''+@kieusinh+''' '

	if @nguongoc = ''
		set @nguongoc = ''
	else
		set @nguongoc = ' and TenNguonGoc = N'''+@nguongoc+''' '

	set @query = 'select * from view_thu where 1=1 ' + @ten + @loai + @kieusinh + @nguongoc
	print @query
	exec sp_executesql @query
end
exec Proc_Thu_filter '','',N'',N'Châu Á'


----------------Chuong------------------

select * from chuong

create or alter view View_Chuong_DanhSachChuong as
	select Chuong.MaChuong as N'Mã chuồng', TenLoai as N'Tên loài' , 
	TenKhu as N'Tên khu', DienTich as N'Diện tích', 
	ChieuCao as N'Chiều cao', SoLuongThu, 
	TrangThai.TenTrangThai as N'Trạng thái', TenNhanVien, Chuong.GhiChu as N'Ghi chú'
	from Chuong join Loai on chuong.MaLoai = Loai.MaLoai
				join Khu on chuong.MaKhu = Khu.MaKhu
				join TrangThai on Chuong.MaTrangThai = TrangThai.MaTrangThai
				join NhanVien on Chuong.MaNhanVien = NhanVien.MaNhanVien
				--join Thu_Chuong on Thu_Chuong.MaChuong = Chuong.MaChuong
				--join thu on thu.MaThu = Thu_Chuong.MaThu

select * from View_Chuong_DanhSachChuong

--trigger xoa chuong
create or alter trigger xoaChuong on Chuong
instead of delete as
begin 
	declare @maChuong nvarchar(20) 
	select @maChuong = MaChuong from deleted
	delete from Thu_Chuong where MaThu = @maChuong
	delete from Chuong where MaChuong = @maChuong
end

delete from Chuong where maChuong = N'C222'
--proc loc chuong
create or alter procedure Proc_Chuong_filter(@mathu nvarchar(255), @manhanvien nvarchar(255), @soluong int)
as begin
	declare @query nvarchar(255)

	if @mathu = ''
		set @mathu = ''
	else
		set @mathu = ' and Thu.MaThu = N'''+@mathu+''' '

	if @manhanvien = ''
		set @manhanvien = ''
	else
		set @manhanvien = ' and TenNhanVien = N'''+@manhanvien+''' '

	if @soluong = ''
		set @soluong = ''
	else
		set @soluong = ' and SoLuongThu <= N'''+@soluong+''' '


	set @query = 'select * from View_Chuong_DanhSachChuong where 1=1 ' + @mathu + @manhanvien + @soluong
	print @query
	exec sp_executesql @query
end

insert into Chuong(machuong, maloai, makhu, dientich, chieucao, SoLuongThu, matrangthai, manhanvien, ghichu) 
values(N'C111', N'L010',N'K02', '4','10', N'0',N'TT01', N'NV04',N'')

----------------Bao cao------------------