create database Negocios 2018
go
use Negocios2018
go
create table tb_contacto(
codcontac int primary key,
nomcontac varchar(20) not null,
dircontac varchar(80),
idpais char(3) references tb_paises
)
go
insert into tb_contacto values (1,'Elias Jurado Santos','Jr. Pedro Remy 190','001')
insert into tb_contacto values (2,'Carla Pretel Tinoco','Av. Los Héroes 356','002')

select * from tb_contacto

select codcontac,nomcontac,dircontac,(select NombrePais from tb_paises where Idpais = tb_contacto.idpais) pais from tb_contacto

update tb_contacto set nomcontac='Pepito',dircontac='lala',idpais=(Select idpais from tb_paises where NombrePais='Argentina') where codcontac=1

