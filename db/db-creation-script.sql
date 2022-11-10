-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema Project_3
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema Project_3
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Project_3` DEFAULT CHARACTER SET utf8 ;
USE `Project_3` ;

-- -----------------------------------------------------
-- Table `Project_3`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`User` (
  `name` VARCHAR(45) NOT NULL,
  `login` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `last_login` TIMESTAMP NULL,
  `admin` TINYINT(1) NULL DEFAULT 0,
  `active` TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`login`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Target_group`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Target_group` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `description` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Object`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Object` (
  `id` VARCHAR(45) NOT NULL,
  `creator` VARCHAR(45) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `description` VARCHAR(400) NULL,
  `target_group` VARCHAR(45) NOT NULL,
  `location` VARCHAR(45) NOT NULL,
  `type` VARCHAR(45) NOT NULL,
  `model` VARCHAR(45) NOT NULL,
  `state` TINYINT(1) NOT NULL DEFAULT 1,
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Target_group_id` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Object_User_idx` (`creator` ASC),
  INDEX `fk_Object_Target_group1_idx` (`Target_group_id` ASC),
  CONSTRAINT `fk_Object_User`
    FOREIGN KEY (`creator`)
    REFERENCES `Project_3`.`User` (`login`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Object_Target_group1`
    FOREIGN KEY (`Target_group_id`)
    REFERENCES `Project_3`.`Target_group` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Inspection`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Inspection` (
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `maker` VARCHAR(45) NOT NULL,
  `Object_id` VARCHAR(45) NOT NULL,
  `reason` VARCHAR(100) NOT NULL,
  `observations` VARCHAR(400) NULL,
  `change_of_state` TINYINT(1) NULL,
  PRIMARY KEY (`timestamp`),
  INDEX `fk_Inspection_User1_idx` (`maker` ASC),
  INDEX `fk_Inspection_Object1_idx` (`Object_id` ASC),
  CONSTRAINT `fk_Inspection_User1`
    FOREIGN KEY (`maker`)
    REFERENCES `Project_3`.`User` (`login`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inspection_Object1`
    FOREIGN KEY (`Object_id`)
    REFERENCES `Project_3`.`Object` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Auditing_form`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Auditing_form` (
  `auditing_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `maker` VARCHAR(45) NOT NULL,
  `target_group_id` INT UNSIGNED NOT NULL,
  `created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `description` VARCHAR(400) NOT NULL,
  PRIMARY KEY (`auditing_id`),
  INDEX `fk_Auditing_User1_idx` (`maker` ASC),
  INDEX `fk_Auditing_Target_group1_idx` (`target_group_id` ASC),
  CONSTRAINT `fk_Auditing_User1`
    FOREIGN KEY (`maker`)
    REFERENCES `Project_3`.`User` (`login`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Auditing_Target_group1`
    FOREIGN KEY (`target_group_id`)
    REFERENCES `Project_3`.`Target_group` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Requirement`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Requirement` (
  `req_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Auditing_auditing_id` INT UNSIGNED NOT NULL,
  `description` VARCHAR(400) NOT NULL,
  `must` TINYINT(1) NOT NULL,
  PRIMARY KEY (`req_id`),
  INDEX `fk_Requirement_Auditing1_idx` (`Auditing_auditing_id` ASC),
  CONSTRAINT `fk_Requirement_Auditing1`
    FOREIGN KEY (`Auditing_auditing_id`)
    REFERENCES `Project_3`.`Auditing_form` (`auditing_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Auditing_logs`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Auditing_logs` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `maker` VARCHAR(45) NOT NULL,
  `object` VARCHAR(45) NOT NULL,
  `created` TIMESTAMP NULL,
  `description` VARCHAR(400) NULL,
  `result` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Auditing_logs_User1_idx` (`maker` ASC),
  INDEX `fk_Auditing_logs_Object1_idx` (`object` ASC),
  CONSTRAINT `fk_Auditing_logs_User1`
    FOREIGN KEY (`maker`)
    REFERENCES `Project_3`.`User` (`login`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Auditing_logs_Object1`
    FOREIGN KEY (`object`)
    REFERENCES `Project_3`.`Object` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Project_3`.`Requirement_result`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Project_3`.`Requirement_result` (
  `requirement_id` VARCHAR(45) NOT NULL,
  `Auditing_logs_id` INT UNSIGNED NOT NULL,
  `description` VARCHAR(400) NOT NULL,
  `must` TINYINT(1) NOT NULL,
  `result` TINYINT(1) NULL,
  PRIMARY KEY (`requirement_id`),
  INDEX `fk_Requirement_result_Auditing_logs1_idx` (`Auditing_logs_id` ASC),
  CONSTRAINT `fk_Requirement_result_Auditing_logs1`
    FOREIGN KEY (`Auditing_logs_id`)
    REFERENCES `Project_3`.`Auditing_logs` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
