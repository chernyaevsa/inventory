-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema inventory
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema inventory
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `inventory` ;
USE `inventory` ;

-- -----------------------------------------------------
-- Table `inventory`.`buildings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`buildings` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `address` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`cabinets`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`cabinets` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `building_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_cabinets_1_idx` (`building_id` ASC) VISIBLE,
  CONSTRAINT `fk_cabinets_1`
    FOREIGN KEY (`building_id`)
    REFERENCES `inventory`.`buildings` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`types` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`equipment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`equipment` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(85) NOT NULL,
  `number` VARCHAR(45) NOT NULL,
  `count` INT NOT NULL,
  `price` FLOAT NOT NULL,
  `cabinet_id` INT NOT NULL,
  `1c_kod` VARCHAR(45) NULL,
  `type_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_equipment_1_idx` (`cabinet_id` ASC) VISIBLE,
  INDEX `fk_equipment_2_idx` (`type_id` ASC) VISIBLE,
  CONSTRAINT `fk_equipment_1`
    FOREIGN KEY (`cabinet_id`)
    REFERENCES `inventory`.`cabinets` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_equipment_2`
    FOREIGN KEY (`type_id`)
    REFERENCES `inventory`.`types` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`employees`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`employees` (
  `id` INT NOT NULL,
  `surname` VARCHAR(45) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `patronymic` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`responsible`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`responsible` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `employee_id` INT NOT NULL,
  `cabinet_id` INT NOT NULL,
  `datetime` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_responsible_1_idx` (`employee_id` ASC) VISIBLE,
  INDEX `fk_responsible_2_idx` (`cabinet_id` ASC) VISIBLE,
  CONSTRAINT `fk_responsible_1`
    FOREIGN KEY (`employee_id`)
    REFERENCES `inventory`.`employees` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_responsible_2`
    FOREIGN KEY (`cabinet_id`)
    REFERENCES `inventory`.`cabinets` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`statuses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`statuses` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `equipment_id` INT NOT NULL,
  `status` INT NOT NULL,
  `description` VARCHAR(45) NOT NULL,
  `datetime` VARCHAR(45) NOT NULL,
  `user_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_statuses_1_idx` (`equipment_id` ASC) VISIBLE,
  CONSTRAINT `fk_statuses_1`
    FOREIGN KEY (`equipment_id`)
    REFERENCES `inventory`.`equipment` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `inventory`.`ranges`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `inventory`.`ranges` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `datetime_from` DATETIME NOT NULL,
  `datetime_to` DATETIME NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
