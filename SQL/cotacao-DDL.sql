CREATE TABLE `cotacao` (
  `id` int NOT NULL AUTO_INCREMENT,
  `cnpjcomprador` varchar(18) NOT NULL,
  `cnpjfornecedor` varchar(18) NOT NULL,
  `numerocotacao` int DEFAULT NULL,
  `datacotacao` date NOT NULL,
  `dataentregacotacao` date DEFAULT NULL,
  `cep` varchar(8) NOT NULL,
  `logradouro` varchar(50) DEFAULT NULL,
  `complemento` varchar(50) DEFAULT NULL,
  `bairro` varchar(50) DEFAULT NULL,
  `uf` varchar(2) DEFAULT NULL,
  `observacao` tinytext,
  `status` varchar(1) NOT NULL,
  PRIMARY KEY (`id`)
);
