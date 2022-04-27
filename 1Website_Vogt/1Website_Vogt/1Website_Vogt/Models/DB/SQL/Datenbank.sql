create database Datenbank collate utf8mb4_general_ci; 

use Datenbank; 

create table users(
	user_id int unsigned not null auto_increment, 
	vorname varchar(50) not null,
	nachname varchar(50) not null, 
    ort varchar(50) not null,
    strasse varchar(50) null,
    hausnummer int not null,
    postleitzahl int not null,
	email varchar(50) not null,
    zahlung int not null,
	
	constraint user_id_PK primary key(user_id)
);