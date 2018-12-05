-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.3.11-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for kururudb
CREATE DATABASE IF NOT EXISTS `kururudb` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `kururudb`;

-- Dumping structure for table kururudb.guilds
CREATE TABLE IF NOT EXISTS `guilds` (
  `GuildID` bigint(20) NOT NULL,
  `Prefix` tinytext NOT NULL,
  `WelcomeMessageActive` tinyint(4) DEFAULT 0,
  `WelcomeMessage` text DEFAULT NULL,
  `WelcomeMessageChannel` bigint(20) DEFAULT NULL,
  `DefaultRole` text DEFAULT NULL,
  `GoodbyeMessageActive` tinyint(4) DEFAULT 0,
  `GoodbyeMessage` text DEFAULT NULL,
  `GoodbyeMessageChannel` bigint(20) DEFAULT NULL,
  `AddDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Data exporting was unselected.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
