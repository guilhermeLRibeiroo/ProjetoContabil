
-- Categorias
DROP TABLE IF EXISTS categorias;

CREATE TABLE categorias (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100) NOT NULL
);

--Contabilidades
DROP TABLE IF EXISTS contabilidades;

CREATE TABLE contabilidades(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100) NOT NULL
);

--Usuarios
DROP TABLE IF EXISTS usuarios;

CREATE TABLE usuarios (
	id INT PRIMARY KEY IDENTITY(1,1),
	login VARCHAR(100),
	senha VARCHAR(100),
	data_nascimento DATETIME2,
	id_contabilidade INT,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id)
);

--Clientes
DROP TABLE IF EXISTS clientes;

CREATE TABLE clientes (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	cpf VARCHAR(100),
	id_contabilidade INT,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id)
);

--Contas a Pagar
DROP TABLE IF EXISTS contas_pagar;

CREATE TABLE contas_pagar (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	id_categoria INT,
	FOREIGN KEY (id_categoria) REFERENCES categorias(id),
	nome VARCHAR(100),
	valor DECIMAL(18,2),
	data_vencimento DATETIME2,
	data_pagamento DATETIME2
);

DROP TABLE IF EXISTS cartoes_credito;

CREATE TABLE cartoes_credito(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	numero VARCHAR(100),
	data_vencimento DATETIME2,
	cvv VARCHAR(100)
);


--Contas a Receber
DROP TABLE IF EXISTS contas_receber;

CREATE TABLE contas_receber (
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT,
	FOREIGN KEY (id_cliente) REFERENCES clientes(id),
	id_categoria INT,
	FOREIGN KEY (id_categoria) REFERENCES categorias(id),
	nome VARCHAR(100),
	valor DECIMAL(18,2),
	data_pagamento DATETIME2
);