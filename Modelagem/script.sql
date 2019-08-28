SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema reports_system
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema reports_system
-- -----------------------------------------------------
CREATE DATABASE IF NOT EXISTS `reports_system` DEFAULT CHARACTER SET utf8 ;
USE `reports_system` ;

-- -----------------------------------------------------
-- Table `reports_system`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`usuario` (
  `id_usuario` INT NOT NULL,
  `nome` VARCHAR(50) NOT NULL,
  `login` VARCHAR(20) NOT NULL,
  `senha` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`id_usuario`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `reports_system`.`perfil`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`perfil` (
  `id_perfil` INT NOT NULL,
  `descricao` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`id_perfil`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `reports_system`.`vinculo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`vinculo` (
  `id_vinculo` INT NOT NULL,
  `ies` VARCHAR(5) NOT NULL,
  `orgao` VARCHAR(45) NOT NULL,
  `turma` VARCHAR(10) NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`id_vinculo`),
  INDEX `fk_vinculo_usuario1_idx` (`usuario_id` ASC) ,
  CONSTRAINT `fk_vinculo_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `reports_system`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `reports_system`.`atividade`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`atividade` (
  `id_atividade` INT NOT NULL,
  `descricao` VARCHAR(45) NOT NULL,
  `data_atividade` DATE NOT NULL,
  `data_cadastro` DATE NOT NULL,
  `usuario_id` INT NOT NULL,
  PRIMARY KEY (`id_atividade`),
  INDEX `fk_atividade_usuario1_idx` (`usuario_id` ASC) ,
  CONSTRAINT `fk_atividade_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `reports_system`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `reports_system`.`relatorio`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`relatorio` (
  `id_relatorio` INT NOT NULL,
  `descricao` VARCHAR(200) NOT NULL,
  `data_inicio` DATE NOT NULL,
  `data_fim` DATE NOT NULL,
  `usuario_id` INT NOT NULL,
  `atividade_id` INT NOT NULL,
  PRIMARY KEY (`id_relatorio`),
  INDEX `fk_relatorio_usuario1_idx` (`usuario_id` ASC) ,
  INDEX `fk_relatorio_atividade1_idx` (`atividade_id` ASC) ,
  CONSTRAINT `fk_relatorio_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `reports_system`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_relatorio_atividade1`
    FOREIGN KEY (`atividade_id`)
    REFERENCES `reports_system`.`atividade` (`id_atividade`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `reports_system`.`usuario_perfil`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`usuario_perfil` (
  `usuario_id` INT NOT NULL,
  `perfil_id` INT NOT NULL,
  PRIMARY KEY (`usuario_id`, `perfil_id`),
  INDEX `fk_usuario_has_perfil_perfil1_idx` (`perfil_id` ASC) ,
  INDEX `fk_usuario_has_perfil_usuario_idx` (`usuario_id` ASC) ,
  CONSTRAINT `fk_usuario_has_perfil_usuario`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `reports_system`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_usuario_has_perfil_perfil1`
    FOREIGN KEY (`perfil_id`)
    REFERENCES `reports_system`.`perfil` (`id_perfil`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `reports_system`.`log_system`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `reports_system`.`log_system` (
  `id_log` INT NOT NULL,
  `id_usuario` INT NULL,
  `nome` VARCHAR(50) NULL,
  `pagina_acessada` VARCHAR(45) NULL,
  PRIMARY KEY (`id_log`))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
