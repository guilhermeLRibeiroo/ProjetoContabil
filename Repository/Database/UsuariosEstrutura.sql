DROP TABLE usuarios;

CREATE TABLE usuarios (
	id INT PRIMARY KEY IDENTITY(1,1),
	login VARCHAR(100),
	senha VARCHAR(100),
	data_nascimento DATETIME2

);