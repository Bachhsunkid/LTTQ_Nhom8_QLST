
use QLVuonThu
go 
create table Loai
(
	MaLoai nvarchar(20), --L001
	TenLoai nvarchar(100), -- Chó, hổ, sư tử, voi, gấu....
	GhiChu text -- khong can nhap du
	constraint pk_Loai primary key (MaLoai)
)
create table NhanVien
(
	MaNhanVien nvarchar(20),
	TenNhanVien nvarchar(100),
	NgaySinh datetime,
	DiaChi nvarchar(200),
	DienThoai nvarchar(20),
	constraint pk_NhanVien primary key (MaNhanVien)
)
create table LyDo
(
	MaLyDo nvarchar(20), --LD01
	TenLyDo nvarchar(100), --Ngo doc, Thoi tiet lạnh, xung dột bầy đàn...
	GhiChu text,
	constraint pk_LyDo primary key (MaLyDo)
)
create table KhacPhuc
(
	MaKhacPhuc nvarchar(20),
	TenCachKhacPhuc nvarchar(100), -- Kiểm tra thức ăn, thêm che chắn cho chuồng, đổi chuồng...
	GhiChu text,
	constraint pk_KhacPhuc primary key (MaKhacPhuc)
)
create table TrangThai
(
	MaTrangThai nvarchar(20),
	TenTrangThai nvarchar(100), -- Trống, còn chỗ, đầy.
	GhiChu text,
	constraint pk_TrangThai primary key (MaTrangThai)
)
create table Khu
(
	MaKhu nvarchar(20),
	TenKhu nvarchar(100), --Khu Chim, khu động vật hoang dã, khu động vật bò sát, khu động vật quý hiếm...
	GhiChu text,
	constraint pk_Khu primary key (MaKhu)
)
create table Que
(
	MaQue nvarchar(20),
	TenQue nvarchar(100), --Việt Nam, Nam Phi, Lào, Trung Quốc...
	constraint pk_Que primary key (MaQue)
)
create table KieuSinh
(
	MaKieuSinh nvarchar(20),
	TenKieuSinh nvarchar(100), --De trung, đẻ 1 lần 1 con, đẻ 1 lần nhiều con.
	GhiChu text,
	constraint pk_KieuSinh primary key(MaKieuSinh)
)
create table NguonGoc
(
	MaNguonGoc nvarchar(20),
	TenNguonGoc nvarchar(100), --Châu Phi, châu á, châu mỹ...
	GhiChu nvarchar,
	constraint pk_NguonGoc primary key (MaNguonGoc)
)
create table ThucAn
(
	MaThucAn nvarchar(20),
	TenThucAn nvarchar(100), -- thịt bò, thịt gà, cá, cỏ voi, cỏ Ubon Paspalum, chuối, cà rốt,...
	CongDung nvarchar(200), -- Bổ sung đạm, bổ sung chất xơ,...
	MaDonVi nvarchar(20), --Kg, 
	constraint pk_ThucAn primary key (MaThucAn)
)
create table Chuong
(
	MaChuong nvarchar(20),
	MaLoai nvarchar(20),
	MaKhu nvarchar(20),
	DienTich float,
	ChieuCao float,
	SoLuongThu int,
	MaTrangThai nvarchar(20),
	MaNhanVien nvarchar(20),
	GhiChu text,
	constraint pk_Chuong primary key (MaChuong),
	foreign key (MaLoai) references Loai(MaLoai),
	foreign key (MaKhu) references Khu(MaKhu),
	foreign key (MaTrangThai) references TrangThai(MaTrangThai),
	foreign key (MaNhanVien) references NhanVien(MaNhanVien)
)
create table SuKien
(
	MaSuKien nvarchar(20),
	TenSuKien nvarchar(100),-- ốm, bỏ ăn, chết...
	GhiChu text,
	constraint pk_SuKien primary key (MaSuKien)
)
create table Thu
(
	MaThu nvarchar(20),
	TenThu nvarchar(100), -- VD:Báo gấm
	MaLoai nvarchar(20),
	SoLuong int,
	SachDo bit, -- 0, 1.
	TenKhoaHoc nvarchar(100),
	TenTA nvarchar(100), --tra wiki. Clouded Leopard
	TenTV nvarchar(100), --Báo gấm
	MaKieuSinh nvarchar(20),
	GioiTinh nvarchar(20),
	NgayVao datetime,
	MaNguonGoc nvarchar(20),
	DacDiem nvarchar(200),
	NgaySinh datetime,
	Anh nvarchar(255), -- Mở ảnh lên copy link vào. VD: --https://vi.wikipedia.org/wiki/B%C3%A1o_g%E1%BA%A5m#/media/T%E1%BA%ADp_tin:Neofelis_nebulosa.jpg
	TuoiTho int,
	MaQue nvarchar(20),
	constraint pk_Thu primary key (MaThu),
	foreign key(MaLoai) references Loai(MaLoai),
	foreign key(MaKieuSinh) references KieuSinh(MaKieuSinh),
	foreign key(MaNguonGoc) references NguonGoc(MaNguonGoc),
	foreign key(MaQue) references Que(MaQue)
)
create table Thu_SuKien
(
	MaSuKien nvarchar(20),
	MaThu nvarchar(20),
	NgayBatDau datetime,
	MaLyDo nvarchar(20),
	MaKhacPhuc nvarchar(20),
	NgayKetThuc datetime,
	constraint pk_Thu_SuKien primary key(MaSuKien,MaThu),
	foreign key(MaLyDo) references LyDo(MaLyDo),
	foreign key(MaKhacPhuc) references KhacPhuc(MaKhacPhuc),
	foreign key(MaThu) references Thu(MaThu),
	foreign key(MaSuKien) references SuKien(MaSuKien)
)
create table ThucAn_Gia
(
	MaGia nvarchar(20),
	MaThucAn nvarchar(20),
	DonGia money, -- 10000, 20000, 80000....
	Thang_namapdung datetime,
	constraint pk_ThucAn_Gia primary key(MaGia),
	foreign key(MaThucAn) references ThucAn(MaThucAn)
)
create table Thu_ThucAn
(
	MaThu nvarchar(20),
	MaThucAnSang nvarchar(20),
	SoLuongSang int, -- 1, 2, 3
	MaThucAnTrua nvarchar(20),
	SoLuongTrua int, -- 1, 2, 3
	MaThucAnToi nvarchar(20),
	SoLuongToi int, -- 1, 2, 3
	constraint pk_Thu_ThucAn primary key(MaThu),
	foreign key(MaThucAnSang) references ThucAn(MaThucAn),
	foreign key(MaThucAnTrua) references ThucAn(MaThucAn),
	foreign key(MaThucAnToi) references ThucAn(MaThucAn),
	foreign key(MaThu) references Thu(MaThu)
)

create table Thu_Chuong(
	MaChuong nvarchar(20),
	MaThu nvarchar(20),
	NgayVao date,
	LyDoVao text,
	constraint pk_Thu_Chuong primary key(MaThu,Machuong),
	foreign key(MaThu) references Thu(MaThu),
	foreign key(MaChuong) references Chuong(MaChuong)
)
create table Taikhoan(
	username varchar(20),
	pass varchar(20),
	email varchar(50),
	constraint pk_Taikhoan primary key (username)
)

insert into Taikhoan values('abc','123','ab@mm.mm')


--insert thu_chuong
insert into Thu_Chuong values(N'C01',N'Th01',N'2012-12-10',null)
insert into Thu_Chuong values(N'C02',N'Th02',N'2014-10-10',null)
insert into Thu_Chuong values(N'C03',N'Th03',N'2014-10-10',null)
insert into Thu_Chuong values(N'C04',N'Th04',N'2016-8-10',null)
insert into Thu_Chuong values(N'C05',N'Th05',N'2012-12-10',null)
insert into Thu_Chuong values(N'C06',N'Th06',N'2015-12-10',null)
insert into Thu_Chuong values(N'C07',N'Th07',N'2016-12-10',null)
insert into Thu_Chuong values(N'C08',N'Th08',N'2017-12-10',null)
insert into Thu_Chuong values(N'C09',N'Th09',N'2013-12-10',null)
insert into Thu_Chuong values(N'C010',N'Th010',N'2017-1-10',null)
insert into Thu_Chuong values(N'C011',N'Th011',N'2013-2-10',null)
insert into Thu_Chuong values(N'C012',N'Th012',N'2017-11-10',null)
insert into Thu_Chuong values(N'C013',N'Th013',N'2012-12-10',null)
insert into Thu_Chuong values(N'C014',N'Th014',N'2016-12-10',null)
insert into Thu_Chuong values(N'C015',N'Th015',N'2014-4-10',null)
insert into Thu_Chuong values(N'C016',N'Th016',N'2013-2-10',null)
insert into Thu_Chuong values(N'C017',N'Th017',N'2013-2-10',null)
insert into Thu_Chuong values(N'C018',N'Th018',N'2013-2-10',null)
insert into Thu_Chuong values(N'C019',N'Th019',N'2011-6-10',null)
insert into Thu_Chuong values(N'C020',N'Th020',N'2013-2-10',null)
insert into Thu_Chuong values(N'C021',N'Th021',N'2015-2-10',null)
insert into Thu_Chuong values(N'C022',N'Th022',N'2013-8-10',null)

--1insert thucan
insert into ThucAn values (N'TA001',N'Thịt bò',N'Bổ sung đạm',N'300kg')
insert into ThucAn values (N'TA002',N'Thịt gà',N'Bổ sung đạm',N'200kg')
insert into ThucAn values (N'TA003',N'Thịt cá',N'Bổ sung đạm',N'250kg')
insert into ThucAn values (N'TA004',N'Thịt cỏ voi',N'Bổ sung chất xơ',N'500kg')
insert into ThucAn values (N'TA005',N'Chuối ',N'Bổ sung chất xơ, vitamin',N'300kg')
insert into ThucAn values (N'TA006',N'Cà rốt',N'Bổ sung chất xơ',N'300kg')
insert into ThucAn values (N'TA007',N'Cỏ Ubon Paspalum',N'Bổ sung chất xơ',N'300kg')
update ThucAn set TenThucAn = N'Cỏ voi' where MaThucAn = N'TA004'

--2insert thucan_gia
insert into ThucAn_Gia values(N'G001',N'TA001',70000,N'2022-6-11')
insert into ThucAn_Gia values(N'G002',N'TA002',40000,N'2022-4-11')
insert into ThucAn_Gia values(N'G003',N'TA003',50000,N'2022-3-11')
insert into ThucAn_Gia values(N'G004',N'TA004',40000,N'2022-6-11')
insert into ThucAn_Gia values(N'G005',N'TA005',25000,N'2022-5-11')
insert into ThucAn_Gia values(N'G006',N'TA006',25000,N'2022-5-11')
insert into ThucAn_Gia values(N'G007',N'TA007',35000,N'2022-5-11')

--3insert loai
insert into Loai values(N'L001',N'Voi',null)
insert into Loai values(N'L002',N'Gấu Trúc',null)
insert into Loai values(N'L003',N'Hổ',null)
insert into Loai values(N'L004',N'Đà Điểu',null)
insert into Loai values(N'L005',N'Chim Công',null)
insert into Loai values(N'L006',N'Khỉ',null)
insert into Loai values(N'L007',N'Tê Gíac',null)
insert into Loai values(N'L008',N'Báo',null)
insert into Loai values(N'L009',N'Ngựa Vằn',null)
insert into Loai values(N'L010',N'Hưu Cao Cổ',null)
insert into Loai values(N'L011',N'Nai',null)
insert into Loai values(N'L012',N'Vẹt',null)
insert into Loai values(N'L013',N'Cá Sấu',null)
insert into Loai values(N'L014',N'Tinh Tinh',null)
insert into Loai values(N'L015',N'Hà Mã',null)
insert into Loai values(N'L016',N'Chim Cánh Cụt',null)
insert into Loai values(N'L017',N'Gấu Bắc Cực',null)
insert into Loai values(N'L018',N'Sư Tử',null)
insert into Loai values(N'L019',N'Voọc Xám',null)
insert into Loai values(N'L020',N'Vượn Đen',null)
insert into Loai values(N'L021',N'Cáo',null)
insert into Loai values(N'L022',N'Báo Gấm',null)



--4insert Nguongoc
insert into NguonGoc values(N'NG01',N'Châu Phi',null)
insert into NguonGoc values(N'NG02',N'Châu Âu',null)
insert into NguonGoc values(N'NG03',N'Châu Mĩ',null)
insert into NguonGoc values(N'NG04',N'Châu Á',null)
insert into NguonGoc values(N'NG05',N'Bắc Cực',null)

--5insert Que
insert into Que values(N'Q01',N'Ai Cập')
insert into Que values(N'Q02',N'Libya')
insert into Que values(N'Q03',N'Tunisia')
insert into Que values(N'Q04',N'Algeria')
insert into Que values(N'Q05',N'Maroc')
insert into Que values(N'Q06',N'Hoa Kì')
insert into Que values(N'Q07',N'Canada')
insert into Que values(N'Q08',N'Mexico')
insert into Que values(N'Q09',N'Brazil')
insert into Que values(N'Q10',N'Đức')
insert into Que values(N'Q11',N'Italy')
insert into Que values(N'Q12',N'Việt Nam')
insert into Que values(N'Q13',N'Singapore')
insert into Que values(N'Q14',N'Bắc Cực')


--6insert kieusinh
insert into KieuSinh values(N'KS01',N'Đẻ trứng',null)
insert into KieuSinh values(N'KS02',N'Đẻ 1 lần 1 con',null)
insert into KieuSinh values(N'KS03',N'Đẻ 1 lần nhiều con',null)


--7insert sukien
insert into SuKien values(N'SK01', N'Ốm',null)
insert into SuKien values(N'SK02', N'Bỏ ăn',null)
insert into SuKien values(N'SK03', N'Tiêu chảy',null)
insert into SuKien values(N'SK04', N'Chết',null)


--8insert khacphuc
insert into KhacPhuc values(N'KP01',N'Kiểm tra thức ăn', null)
insert into KhacPhuc values(N'KP02',N'Khám tra sức khỏe', null)
insert into KhacPhuc values(N'KP03',N'Cải tạo lại chuồng', null)


--9insert lydo
insert into LyDo values (N'LD01',N'Ngộ độc',null)
insert into LyDo values (N'LD02',N'Tiêu chảy',null)
insert into LyDo values (N'LD03',N'Xung đột',null)
insert into LyDo values (N'LD04',N'Bị Lạnh',null)

--10insert nhanvien
insert into NhanVien values(N'NV01',N'Nguyễn Hùng Anh',N'1987-6-12',N'Huỳnh Thúc Kháng,Đống Đa,Hà Nội',N'0366458721')
insert into NhanVien values(N'NV02',N'Nguyễn Mạnh Hùng',N'1989-8-10',N'Khúc Thừa Dụ,Cầu Giấy,Hà Nội',N'0366498700')
insert into NhanVien values(N'NV03',N'Trần Anh Minh',N'1979-8-28',N'Nguyễn Khang,Cầu Giấy,Hà Nội',N'0974153264')
insert into NhanVien values(N'NV04',N'Đặng Dũng',N'1974-12-2',N'Dịch Vọng,Cầu Giấy,Hà Nội',N'0365789214')
insert into NhanVien values(N'NV05',N'Quốc Anh',N'1980-2-12',N'Khúc Thừa Dụ,Cầu Giấy,Hà Nội',N'084532174')
insert into NhanVien values(N'NV06',N'Nguyễn Thiên Tùng',N'1982-7-12',N'Trần Quốc Vượng,Cầu Giấy,Hà Nội',N'0741256222')
insert into NhanVien values(N'NV07',N'Nguyễn Thiện Hùng',N'1987-5-11',N'Khúc Thừa Dụ,Cầu Giấy,Hà Nội',N'0366789456')
--11insert trangthai
insert into TrangThai values(N'TT01',N'Đầy',null)
insert into TrangThai values(N'TT02',N'Trống',null)
insert into TrangThai values(N'TT03',N'Còn 1 chỗ',null)

--12insert khu
insert into Khu values(N'K01',N'Khu chim',null)
insert into Khu values(N'K02',N'Khu động vật hoang dã',null)
insert into Khu values(N'K03',N'Khu quý hiếm',null)
insert into Khu values(N'K04',N'Khu động vật bò sát',null)

--13insert chuong
insert into Chuong values(N'C01',N'L001',N'K02',4.0,6.0,2,N'TT01',N'NV01',null)
insert into Chuong values(N'C02',N'L002',N'K02',4.0,6.0,2,N'TT01',N'NV01',null)
insert into Chuong values(N'C03',N'L003',N'K02',4.0,6.0,2,N'TT01',N'NV01',null)
insert into Chuong values(N'C04',N'L004',N'K01',6.0,6.0,3,N'TT01',N'NV02',null)
insert into Chuong values(N'C05',N'L005',N'K01',4.0,6.0,2,N'TT01',N'NV02',null)
insert into Chuong values(N'C06',N'L006',N'K02',4.0,6.0,2,N'TT03',N'NV02',null)
insert into Chuong values(N'C07',N'L007',N'K02',7.0,6.0,1,N'TT03',N'NV03',null)
insert into Chuong values(N'C08',N'L008',N'K02',4.0,6.0,1,N'TT02',N'NV03',null)
insert into Chuong values(N'C09',N'L009',N'K02',4.0,6.0,1,N'TT03',N'NV03',null)
insert into Chuong values(N'C010',N'L010',N'K02',4.0,10.0,2,N'TT01',N'NV04',null)
insert into Chuong values(N'C011',N'L011',N'K02',4.0,6.0,2,N'TT01',N'NV04',null)
insert into Chuong values(N'C012',N'L012',N'K01',4.0,6.0,3,N'TT01',N'NV04',null)
insert into Chuong values(N'C013',N'L013',N'K04',4.0,6.0,1,N'TT03',N'NV05',null)
insert into Chuong values(N'C014',N'L014',N'K02',4.0,6.0,2,N'TT01',N'NV05',null)
insert into Chuong values(N'C015',N'L015',N'K04',4.0,6.0,2,N'TT02',N'NV05',null)
insert into Chuong values(N'C016',N'L016',N'K01',5.0,6.0,6,N'TT01',N'NV06',null)
insert into Chuong values(N'C017',N'L017',N'K02',4.0,6.0,1,N'TT01',N'NV06',null)
insert into Chuong values(N'C018',N'L018',N'K02',5.0,6.0,1,N'TT01',N'NV06',null)
insert into Chuong values(N'C019',N'L019',N'K03',4.0,6.0,1,N'TT01',N'NV07',null)
insert into Chuong values(N'C020',N'L020',N'K03',4.0,6.0,1,N'TT01',N'NV07',null)
insert into Chuong values(N'C021',N'L021',N'K02',4.0,6.0,2,N'TT01',N'NV07',null)
insert into Chuong values(N'C022',N'L022',N'K03',4.0,6.0,1,N'TT01',N'NV07',null)




--14insert thu
insert into Thu values(N'Th01',N'Voi',N'L001',3,0,N'Mammalia',N'Elephant',N'Voi',N'KS02',N'Cái',N'2018-6-15',N'NG04',N'Da sần sùi, vòi dài chân to, tai to, đôi ngà trắng to dài',N'2012-12-10',N'D:\Lap trinh truc quan\300px-Elephant_at_Indianapolis_Zoo.jpg',70,N'Q12')
insert into Thu values(N'Th02',N'Gấu Trúc',N'L002',2,0,N'Ailuropoda melanoleuca',N'Giant Panda',N'Gấu Trúc',N'KS02',N'Đực',N'2018-6-15',N'NG04',N'nhiều lông, có 2 màu lông là đen trắng trên 1 cơ thể',N'2014-10-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Grosser_Panda.JPG/1280px-Grosser_Panda.JPG',70,N'Q12')
insert into Thu values(N'Th03',N'Hổ',N'L003',2,0,N'Panthera',N'Tiger',N'Hổ',N'KS02',N'Cái',N'2018-7-15',N'NG04',N'Là loài thú ăn thịt ,có các sọc vằn dọc sẫm màu trên bộ lông màu đỏ cam với phần bụng trắng',N'2014-10-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Eye_for_an_eye_%28crop%29.jpg/800px-Eye_for_an_eye_%28crop%29.jpg',60,N'Q13')
insert into Thu values(N'Th04',N'Đà Điểu',N'L004',3,0,N'Aves',N'Ostrich',N'Đà Điểu',N'KS01',N'Cái',N'2018-5-15',N'NG01',N'Là loài chim lớn, không biết bay',N'2016-8-10',N'https://upload.wikimedia.org/wikipedia/commons/b/b4/Struthio_camelus_in_Serengeti_crop.jpg',40,N'Q08')
insert into Thu values(N'Th05',N'Chim công',N'L005',2,0,N'Phasianidae',N'Pavonina',N'Chim công',N'KS01',N'Đực',N'2018-1-15',N'NG04',N'Đầu và cổ của loài công này được che phủ bởi một lớp lông màu xanh lam được sắp xếp như vảy cá',N'2012-12-10',N'https://e.khoahoc.tv/photos/image/2015/12/03/chim-cong.jpg',30,N'Q01')
insert into Thu values(N'Th06',N'Khỉ',N'L006',2,0,N'Monkey',N'Monkey',N'Khỉ',N'KS02',N'Cái',N'2018-6-15',N'NG04',N'loài động vật 4 chân thuộc lớp thú và bộ linh trưởng',N'2015-12-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Cebus_albifrons_edit.jpg/450px-Cebus_albifrons_edit.jpg',40,N'Q07')
insert into Thu values(N'Th07',N'Tê giác',N'L007',1,0,N'Rhinocerotidae',N'Rhino',N'Tê giác',N'KS02',N'Cái',N'2018-6-15',N'NG01',N'Động vật trên cạn lớn nhất, có một hoặc hai sừng và lớp da bảo vệ dày',N'2016-12-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/6/63/Diceros_bicornis.jpg/420px-Diceros_bicornis.jpg',50,N'Q04')
insert into Thu values(N'Th08',N'Báo',N'L008',1,0,N'Cheetah',N'Cheetah',N'Báo',N'KS02',N'Cái',N'2018-6-15',N'NG03',N'cơ thể thon gọn, mảnh mai và cao nhỏng',N'2017-12-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/TheCheethcat.jpg/450px-TheCheethcat.jpg',50,N'Q06')
insert into Thu values(N'Th09',N'Ngựa vằn',N'L009',1,0,N'Zebra',N'Zebra',N'Ngựa vằn',N'KS02',N'Đực',N'2018-9-15',N'NG03',N'Các sọc đen và trắng đặc trưng trên người chúng.',N'2013-12-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Plains_Zebra_Equus_quagga.jpg/800px-Plains_Zebra_Equus_quagga.jpg',60,N'Q03')
insert into Thu values(N'Th010',N'Hưu cao cổ',N'L010',2,0,N'Giraffa',N'Giraffa',N'Hưu cao cổ',N'KS02',N'Cái',N'2018-6-15',N'NG03',N'Có cái cổ rất dài',N'2017-1-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/Giraffe_Mikumi_National_Park.jpg/800px-Giraffe_Mikumi_National_Park.jpg',70,N'Q08')
insert into Thu values(N'Th011',N'Nai',N'L011',2,0,N'Rusa unicolor',N'Sambar deer',N'Nai',N'KS02',N'Cái',N'2018-10-15',N'NG04',N'Đặc điểm nổi bật nhất của chúng là trên cổ có một đường sọc màu nâu sẫm chạy dọc sống lưng đến tận đuôi',N'2013-2-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Sambar_deer_Cervus_unicolor.jpg/420px-Sambar_deer_Cervus_unicolor.jpg',50,N'Q12')
insert into Thu values(N'Th012',N'Vẹt',N'L012',3,0,N'Parrot',N'Parrot',N'Voi',N'KS01',N'Đực',N'2018-6-15',N'NG04',N'mỏ khỏe để đập vỡ vỏ hạt,cũng như rất cong, tư thế thẳng đứng, chân khỏe và chân có móng.',N'2017-11-10',N'https://upload.wikimedia.org/wikipedia/commons/a/a5/Parrot_montage.jpg',30,N'Q13')
insert into Thu values(N'Th013',N'Cá Sấu',N'L013',1,0,N' Crocodylidae',N'crocodile',N'Cá sấu',N'KS02',N'Cái',N'2018-9-15',N'NG03',N'Da sần sùi, mỏ dài hàm răng to',N'2012-12-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Crocodile_Feast_AdF.jpg/1280px-Crocodile_Feast_AdF.jpg',80,N'Q06')
insert into Thu values(N'Th014',N'Tinh Tinh',N'L014',2,0,N'Pan troglodytes',N'Pan troglodytes',N'Tinh Tinh',N'KS02',N'Cái',N'2018-6-15',N'NG01',N'ân nặng 40 và 65 kg (88 và 143 lb) và cao 1,6 đến 1,3 m (5 ft 3 in đến 4 ft 3 in) khi đứng thẳng.',N'2016-12-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/500px_photo_%28188689961%29.jpeg/1024px-500px_photo_%28188689961%29.jpeg',70,N'Q02')
insert into Thu values(N'Th015',N'Hà Mã',N'L015',2,0,N'Hippopotamidae',N'Hippopotamidae',N'Hà Mã',N'KS02',N'Cái',N'2018-3-20',N'NG01',N'là một trong những loài thú có vú trên cạn lớn nhất và là động vật móng guốc chẵn nặng nhất',N'2014-4-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/b/be/Hippo_memphis.jpg/450px-Hippo_memphis.jpg',50,N'Q05')
insert into Thu values(N'Th016',N'Chim cánh cụt',N'L016',6,0,N'Sphenisciformes',N'penguin',N'Chim cánh cụt',N'KS01',N'Cái',N'2018-9-15',N'NG05',N'chim cánh cụt có bộ lông tương phản nhau gồm các mảng sáng và tối và chân chèo để bơi lội',N'2013-2-10',N'https://upload.wikimedia.org/wikipedia/commons/6/69/Manchot_01.jpg',50,N'Q14')
insert into Thu values(N'Th017',N'Gấu Bắc Cực',N'L017',1,0,N'Ursus maritimus',N'Ursus maritimus',N'Gấu bắc cực',N'KS02',N'Cái',N'2018-11-15',N'NG05',N'Có bộ lông dày và trắng',N'2013-2-10',N'https://image.thanhnien.vn/1200x630/Uploaded/2022/znosgo/2020_07_22/gaubaccuc_fqkj.jpg',80,N'Q14')
insert into Thu values(N'Th018',N'Sư tử',N'L019',1,0,N'Panthera leo',N'Lion',N'Sư tử',N'KS02',N'Cái',N'2018-9-15',N'NG02',N'bộ bờm dày ',N'2013-2-10',N'https://upload.wikimedia.org/wikipedia/commons/7/73/Lion_waiting_in_Namibia.jpg',50,N'Q10')
insert into Thu values(N'Th019',N'Vọoc Xám',N'L019',1,1,N'Trachypithecus phayrei',N'Trachypithecus phayrei',N'Voọc Xám',N'KS02',N'Cái',N'2018-2-15',N'NG04',N'Voọc xám có bộ lông màu tro xám nâu trên lưng. Phía bụng thì lông trắng. Lông trên đầu và đuôi đậm hơn',N'2011-6-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/Phayre%27s_Langur%2C_Trachypithecus_phayrei_in_Phu_Khieo_Wildlife_Sanctuary_%2821134240148%29.jpg/420px-Phayre%27s_Langur%2C_Trachypithecus_phayrei_in_Phu_Khieo_Wildlife_Sanctuary_%2821134240148%29.jpg',50,N'Q12')
insert into Thu values(N'Th020',N'Vượn Đen',N'L020',1,1,N'Nomascus concolor',N'Nomascus concolor',N'Vượn Đen',N'KS02',N'Đực',N'2018-9-11',N'NG04',N'Chiều dài từ đầu đến cuối thân là 45–64 cm và cân nặng 5,7 kg.',N'2013-2-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/White_Cheeked_Gibbon_Male.jpg/420px-White_Cheeked_Gibbon_Male.jpg',50,N'Q13')
insert into Thu values(N'Th021',N'Cáo',N'L021',2,1,N'fox',N'fox',N'Cáo',N'KS02',N'Cái',N'2018-9-15',N'NG04',N' có mõm dài, hẹp, đuôi rậm, mắt xếch và tai nhọn. ',N'2015-2-10',N'https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Vulpes_vulpes_laying_in_snow.jpg/420px-Vulpes_vulpes_laying_in_snow.jpg',50,N'Q12')
insert into Thu values(N'Th022',N'Báo gấm',N'L022',1,1,N'Neofelis nebulosa',N'Clouded Leopard',N'Báo Gấm',N'KS02',N'Cái',N'2018-9-15',N'NG04',N'một loài mèo cỡ trung bình trong Họ Mèo, toàn thân dài 60 tới 110 cm (2 - 3,6) và cân nặng khoảng 11 – 20 kg (25 lbs 4oz - 44 lbs). Lông chúng màu nâu hay hung, điểm hoa elip lớn, hình dạng không đều, gờ màu sẫm trông giống như đám mây, vì thế tên khoa học và một số tiếng nước ngoài đều nhắc tới mây',N'2013-8-10',N'https://upload.wikimedia.org/wikipedia/commons/7/7d/Neofelis_nebulosa.jpg',60,N'Q13')




--15insser tthu_sukien
insert into Thu_SuKien values(N'SK01',N'Th01',N'2021-5-25',N'LD01',N'KP01',N'2021-6-5')
insert into Thu_SuKien values(N'SK02',N'Th02',N'2021-6-15',N'LD01',N'KP01',N'2021-6-25')
insert into Thu_SuKien values(N'SK03',N'Th03',N'2021-7-1',N'LD01',N'KP01',N'2021-7-5')
insert into Thu_SuKien values(N'SK04',N'Th04',N'2021-8-1',N'LD01',N'KP01',N'2021-8-5')
insert into Thu_SuKien values(N'SK01',N'Th05',N'2021-7-25',N'LD01',N'KP01',N'2021-7-29')
insert into Thu_SuKien values(N'SK01',N'Th06',N'2021-8-25',N'LD01',N'KP01',N'2021-8-30')
insert into Thu_SuKien values(N'SK01',N'Th07',N'2021-5-15',N'LD01',N'KP01',N'2021-5-20')
insert into Thu_SuKien values(N'SK02',N'Th08',N'2021-5-13',N'LD02',N'KP02',N'2021-5-15')
insert into Thu_SuKien values(N'SK02',N'Th09',N'2021-8-25',N'LD02',N'KP02',N'2021-8-26')
insert into Thu_SuKien values(N'SK02',N'Th010',N'2021-8-17',N'LD02',N'KP02',N'2021-8-19')
insert into Thu_SuKien values(N'SK02',N'Th011',N'2021-9-25',N'LD02',N'KP02',N'2021-9-27')
insert into Thu_SuKien values(N'SK03',N'Th012',N'2021-9-2',N'LD03',N'KP03',N'2021-9-5')
insert into Thu_SuKien values(N'SK02',N'Th013',N'2021-9-20',N'LD02',N'KP02',N'2021-9-23')
insert into Thu_SuKien values(N'SK02',N'Th014',N'2021-10-12',N'LD02',N'KP02',N'2021-10-14')
insert into Thu_SuKien values(N'SK02',N'Th015',N'2021-10-25',N'LD02',N'KP02',N'2021-10-28')
insert into Thu_SuKien values(N'SK03',N'Th016',N'2021-11-11',N'LD03',N'KP03',N'2021-11-13')
insert into Thu_SuKien values(N'SK03',N'Th017',N'2021-11-18',N'LD03',N'KP03',N'2021-11-20')
insert into Thu_SuKien values(N'SK03',N'Th018',N'2021-11-25',N'LD03',N'KP03',N'2021-11-29')
insert into Thu_SuKien values(N'SK02',N'Th019',N'2021-12-1',N'LD02',N'KP02',N'2021-12-6')
insert into Thu_SuKien values(N'SK02',N'Th020',N'2021-11-5',N'LD02',N'KP02',N'2021-11-9')
insert into Thu_SuKien values(N'SK02',N'Th021',N'2021-11-11',N'LD02',N'KP02',N'2021-11-13')
insert into Thu_SuKien values(N'SK02',N'Th022',N'2021-11-28',N'LD02',N'KP02',N'2021-11-30')






--16insert thu_thucan
insert into Thu_ThucAn values(N'Th01',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th02',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th03',N'TA001',1,N'TA002',2,N'TA003',1)
insert into Thu_ThucAn values(N'Th04',N'TA005',1,N'TA006',2,N'TA007',1)
insert into Thu_ThucAn values(N'Th05',N'TA005',1,N'TA006',2,N'TA004',1)
insert into Thu_ThucAn values(N'Th06',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th07',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th08',N'TA001',1,N'TA002',2,N'TA003',1)
insert into Thu_ThucAn values(N'Th09',N'TA005',1,N'TA006',2,N'TA007',1)
insert into Thu_ThucAn values(N'Th010',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th011',N'TA005',1,N'TA006',2,N'TA007',1)
insert into Thu_ThucAn values(N'Th012',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th013',N'TA001',1,N'TA002',2,N'TA003',1)
insert into Thu_ThucAn values(N'Th014',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th016',N'TA005',1,N'TA006',2,N'TA003',1)
insert into Thu_ThucAn values(N'Th017',N'TA005',1,N'TA006',2,N'TA003',1)
insert into Thu_ThucAn values(N'Th018',N'TA001',1,N'TA002',2,N'TA003',1)
insert into Thu_ThucAn values(N'Th019',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th020',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th021',N'TA005',1,N'TA006',2,N'TA005',1)
insert into Thu_ThucAn values(N'Th022',N'TA001',1,N'TA002',2,N'TA003',1)



