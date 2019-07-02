DROP TABLE IF EXISTS clientes;

CREATE TABLE clientes (
	
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	cpf VARCHAR(100),
	id_contabilidade INT,
	FOREIGN KEY (id_contabilidade) REFERENCES contabilidades(id)
	
);