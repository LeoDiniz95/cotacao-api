CREATE TABLE `cotacaoitem` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `idcotacao` INT NOT NULL,
  `descricao` VARCHAR(45) NOT NULL,
  `numeroitem` INT NOT NULL,
  `quantidade` INT NOT NULL,
  `preco` DECIMAL(10,2) NULL,
  `marca` VARCHAR(45) NULL,
  `unidade` VARCHAR(45) NULL,
  `status` VARCHAR(1) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_cotacao_idx` (`idcotacao` ASC) VISIBLE,
  CONSTRAINT `fk_cotacao`
    FOREIGN KEY (`idcotacao`)
    REFERENCES `sys`.`cotacao` (`id`)
    ON DELETE CASCADE);
