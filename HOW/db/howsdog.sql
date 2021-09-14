-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema howsdog
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema howsdog
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `howsdog` DEFAULT CHARACTER SET utf8 ;
USE `howsdog` ;

-- -----------------------------------------------------
-- Table `howsdog`.`cliente`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `howsdog`.`cliente` (
  `idCliente` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nmCliente` VARCHAR(100) NULL,
  `CEP` VARCHAR(10) NULL,
  `logradouro` VARCHAR(100) NULL,
  `bairro` VARCHAR(100) NULL,
  `cidade` VARCHAR(100) NULL,
  `enderecoNumero` VARCHAR(45) NULL,
  PRIMARY KEY (`idCliente`),
  UNIQUE INDEX `idCliente_UNIQUE` (`idCliente` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `howsdog`.`tipoProduto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `howsdog`.`tipoProduto` (
  `idTipoProduto` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nmTipoProduto` VARCHAR(100) NULL,
  `dsTipoProduto` VARCHAR(240) NULL,
  PRIMARY KEY (`idTipoProduto`),
  UNIQUE INDEX `idTipoProduto_UNIQUE` (`idTipoProduto` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `howsdog`.`produto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `howsdog`.`produto` (
  `idProduto` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `nmProduto` VARCHAR(100) NOT NULL,
  `dsProduto` VARCHAR(240) NULL,
  `valorVenda` DECIMAL(15,2) NULL,
  `idTipoProduto` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idProduto`),
  UNIQUE INDEX `idProduto_UNIQUE` (`idProduto` ASC) ,
  INDEX `fk_produto_tipoProduto_idx` (`idTipoProduto` ASC) ,
  CONSTRAINT `fk_produto_tipoProduto`
    FOREIGN KEY (`idTipoProduto`)
    REFERENCES `howsdog`.`tipoProduto` (`idTipoProduto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `howsdog`.`pedido`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `howsdog`.`pedido` (
  `idPedido` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `dtPedido` DATETIME NULL,
  `previsaoPreparo` DATETIME NULL,
  `previsaoEntrega` DATETIME NULL,
  `totalPedido` DECIMAL(15,2) NULL,
  `obsPedido` VARCHAR(400) NULL,
  `idCliente` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idPedido`),
  INDEX `fk_pedido_cliente1_idx` (`idCliente` ASC) ,
  CONSTRAINT `fk_pedido_cliente1`
    FOREIGN KEY (`idCliente`)
    REFERENCES `howsdog`.`cliente` (`idCliente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `howsdog`.`pedidoItens`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `howsdog`.`pedidoItens` (
  `idPedidoItens` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `valorUnit` DECIMAL(15,2) NULL,
  `quantidade` DECIMAL(15,2) NULL DEFAULT 1.0,
  `idPedido` INT UNSIGNED NOT NULL,
  `idProduto` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idPedidoItens`),
  INDEX `fk_pedidoItens_pedido1_idx` (`idPedido` ASC) ,
  INDEX `fk_pedidoItens_produto1_idx` (`idProduto` ASC) ,
  CONSTRAINT `fk_pedidoItens_pedido1`
    FOREIGN KEY (`idPedido`)
    REFERENCES `howsdog`.`pedido` (`idPedido`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_pedidoItens_produto1`
    FOREIGN KEY (`idProduto`)
    REFERENCES `howsdog`.`produto` (`idProduto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `howsdog`.`Cliente`
-- -----------------------------------------------------
START TRANSACTION;
USE `howsdog`;
INSERT INTO `howsdog`.`cliente` (`idCliente`, `nmCliente`) VALUES (1, 'Genérico');

COMMIT;

-- -----------------------------------------------------
-- Data for table `howsdog`.`tipoProduto`
-- -----------------------------------------------------
START TRANSACTION;
USE `howsdog`;
INSERT INTO `howsdog`.`tipoProduto` (`idTipoProduto`, `nmTipoProduto`, `dsTipoProduto`) VALUES (1, 'Simples', '1 salsicha, 15 cm');
INSERT INTO `howsdog`.`tipoProduto` (`idTipoProduto`, `nmTipoProduto`, `dsTipoProduto`) VALUES (2, 'Duplo', '2 salsichas, 15 cm');
INSERT INTO `howsdog`.`tipoProduto` (`idTipoProduto`, `nmTipoProduto`, `dsTipoProduto`) VALUES (3, 'Grande', '2 salsichas, 30 cm');

COMMIT;


-- -----------------------------------------------------
-- Data for table `howsdog`.`produto`
-- -----------------------------------------------------
START TRANSACTION;
USE `howsdog`;

INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Tradicional', 'Ervilha, milho, vinagrete e batata palha', 5, 1);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Bacon', 'Bacon, ervilha, milho, vinagrete e batata palha', 7.50, 1);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Calabresa', 'Calabresa, ervilha, milho, vinagrete e batata palha', 7.50, 1);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Frango', 'Frango, ervilha, milho, vinagrete e batata palha', 9, 1);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Filé', 'Carne, ervilha, milho, vinagrete e batata palha', 10, 1);

INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Tradicional', 'Ervilha, milho, vinagrete e batata palha', 7.5, 2);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Bacon', 'Bacon, ervilha, milho, vinagrete e batata palha', 9.50, 2);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Calabresa', 'Calabresa, ervilha, milho, vinagrete e batata palha', 9.50, 2);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Frango', 'Frango, ervilha, milho, vinagrete e batata palha', 11, 2);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Filé', 'Carne, ervilha, milho, vinagrete e batata palha', 12, 2);

INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Tradicional', 'Ervilha, milho, vinagrete e batata palha', 8, 3);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Bacon', 'Bacon, ervilha, milho, vinagrete e batata palha', 9.50, 3);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Calabresa', 'Calabresa, ervilha, milho, vinagrete e batata palha', 11.50, 3);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Frango', 'Frango, ervilha, milho, vinagrete e batata palha', 13, 3);
INSERT INTO `howsdog`.`produto` (`idProduto`, `nmProduto`, `dsProduto`, `valorVenda`, `idTipoProduto`) VALUES (DEFAULT, 'Filé', 'Carne, ervilha, milho, vinagrete e batata palha', 14.5, 3);

COMMIT;

