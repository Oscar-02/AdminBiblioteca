CREATE DATABASE BiblioDB;

USE BiblioDB;

CREATE TABLE AdminUsers
(
    User_ID int NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255),
    LastName varchar(255),
    DUI VARCHAR(10) UNIQUE NOT NULL,
    Birth DATETIME,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(128) NOT NULL,
    PRIMARY KEY (User_ID)
);

CREATE TABLE ClientUsers
(
    Client_ID int NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255),
    LastName VARCHAR(255),
    Carnet VARCHAR(8) NOT NULL UNIQUE,
    Birth DATETIME,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(128) NOT NULL,
    PRIMARY KEY (Client_ID) 
);