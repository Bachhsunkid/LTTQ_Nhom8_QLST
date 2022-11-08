create database Taikhoan
go
use Taikhoan
go
create table Taikhoan(
	username varchar(20),
	pass varchar(20),
	email varchar(50),
	constraint pk_Taikhoan primary key (username)
)