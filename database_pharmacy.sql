-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 20, 2024 at 07:10 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `database_pharmacy`
--
CREATE DATABASE IF NOT EXISTS `database_pharmacy` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `database_pharmacy`;

-- --------------------------------------------------------

--
-- Table structure for table `drugtable`
--

CREATE TABLE `drugtable` (
  `ID` int(11) NOT NULL,
  `DrugName` varchar(250) NOT NULL,
  `Strength` varchar(10) NOT NULL,
  `Unit` varchar(10) NOT NULL,
  `DosageForm` varchar(10) NOT NULL,
  `PrescriberCategory` varchar(10) NOT NULL,
  `DefaultMaxQTY` varchar(10) NOT NULL,
  `Remark` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `drugtable`
--

INSERT INTO `drugtable` (`ID`, `DrugName`, `Strength`, `Unit`, `DosageForm`, `PrescriberCategory`, `DefaultMaxQTY`, `Remark`) VALUES
(1, 'Acarbose 50mg Tablet', '50', 'mg', 'Tablet', 'A/KK', '', 'Kencing Manis. Ambil ubat dengan suap pertama makanan.'),
(5, 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', 'Tablet', 'C', '', 'Cair darah. Ambil ubat selepas makan.'),
(6, 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', 'Tablet', 'B', '', 'Cair darah'),
(7, 'Albendazole 200mg Tablet', '200', 'mg', 'Tablet', 'C', '', 'Buang cacing'),
(8, 'Allopurinol 300 mg Tablet', '300', 'mg', 'Tablet', 'A/KK', '', 'Gout'),
(9, 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', 'Tablet', 'B', '', ''),
(10, 'Amlodipine 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi. Makan pada waktu pagi.'),
(11, 'Amlodipine 10 mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi. Makan pada waktu pagi.'),
(12, 'Amoxicillin (Amoxycillin) 250mg Capsule', '250', 'mg', 'Tablet', 'B', '', 'Habiskan. Jumpa doktor jika ada alahan.'),
(13, 'Ascorbic Acid 100mg Tablet', '100', 'mg', 'Tablet', 'C', '', ''),
(14, 'Atenolol 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(15, 'Atenolol 50 mg Tablet', '50', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(16, 'Atorvastatin 20 mg Tablet', '20', 'mg', 'Tablet', 'B', '', 'Kolesterol'),
(17, 'Atorvastatin 40 mg Tablet', '40', 'mg', 'Tablet', 'B', '', 'Kolesterol'),
(18, 'Baclofen 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Kekejangan Otot'),
(19, 'Benzhexol 2 mg Tablet', '2', 'mg', 'Tablet', 'B', '', ''),
(20, 'Bisoprolol Fumarate 2.5 mg Tablet', '3', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(21, 'Bisoprolol Fumarate 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(22, 'Bromhexine HCl 8mg Tablet', '8', 'mg', 'Tablet', 'C', '', 'Cair Kahak. Ambil bila perlu sahaja.'),
(23, 'Calcitriol 0.25 mcg Capsule', '0.25', 'mcg', 'Tablet', 'A/KK', '', ''),
(24, 'Calcium Carbonate 500 mg Tablet', '500', 'mg', 'Tablet', 'B', '', ''),
(25, 'Calcium Lactate 300 mg Tablet', '300', 'mg', 'Tablet', 'C', '', ''),
(26, 'Captopril 25 mg Tablet', '25', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(27, 'Carbimazole 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', 'Hyperthyroidism'),
(28, 'Cephalexin Monohydrate 250 mg Capsule', '250', 'mg', 'Tablet', 'B', '', 'Habiskan. Jumpa doktor jika ada alahan.'),
(29, 'Charcoal, Activated 250mg Tablet', '250', 'mg', 'Tablet', 'C', '', 'Keracunan Makanan'),
(30, 'Chlorpheniramine Maleate 4mg Tablet', '4', 'mg', 'Tablet', 'C', '', 'Selsema/Tahan Gatal. Ambil bila perlu sahaja. Boleh menyebabkan mengantuk'),
(31, 'Chlorpromazine HCl 100mg Tablet', '100', 'mg', 'Tablet', 'B', '', ''),
(32, 'Clopidogrel 75 mg Tablet', '75', 'mg', 'Tablet', 'A/KK', '', 'Cair darah'),
(33, 'Cloxacillin Sodium 250mg Capsule', '250', 'mg', 'Tablet', 'B', '', 'Habiskan. Jumpa doktor jika ada alahan'),
(34, 'Colchicine 0.5mg Tablet', '0.5', 'mg', 'Tablet', 'B', '', 'Sakit gout'),
(35, 'Diclofenac 50mg Tablet', '50', 'mg', 'Tablet', 'B', '', 'Tahan sakit. Ambil ubat selepas makan'),
(36, 'Digoxin 0.25mg Tablet', '0.25', 'mg', 'Tablet', 'B', '', ''),
(37, 'Diltiazem HCl 30 mg Tablet', '30', 'mg', 'Tablet', 'B', '', ''),
(38, 'Enalapril 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(39, 'Erythromycin Ethylsuccinate 400 mg Tablet', '400', 'mg', 'Tablet', 'B', '', 'Habiskan. Jumpa doktor jika ada alahan.'),
(40, 'Escitalopram 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', ''),
(41, 'Ethambutol HCl 400 mg Tablet', '400', 'mg', 'Tablet', 'B', '', ''),
(42, 'Ezetimibe 10 mg Tablet', '10', 'mg', 'Tablet', 'A*', '', ''),
(43, 'Felodipine 10mg Extended Release Tablet', '10', 'mg', 'Tablet', 'A/KK', '', ''),
(44, 'Ferrous Fumarate 200 mg Tablet', '200', 'mg', 'Tablet', 'C', '', 'Tambah darah'),
(45, 'Finasteride 5mg Tablet', '5', 'mg', 'Tablet', 'A*', '', ''),
(46, 'Fluvoxamine 50 mg Tablet', '50', 'mg', 'Tablet', 'B', '', ''),
(47, 'Folic Acid 5 mg Tablet', '5', 'mg', 'Tablet', 'C', '', ''),
(48, 'Frusemide 40mg Tablet', '40', 'mg', 'Tablet', 'B', '', 'Buang air / Tekanan Darah Tinggi'),
(49, 'Gemfibrozil 300mg Capsule', '300', 'mg', 'Tablet', 'A/KK', '', ''),
(50, 'Gliclazide 30 mg Modified Release Tablet', '30', 'mg', 'Tablet', 'B', '', 'Kencing manis. Ambil ubat 30 minit sebelum sarapan pagi.'),
(51, 'Gliclazide 80 mg Tablet', '80', 'mg', 'Tablet', 'B', '', 'Kencing manis. Ambil ubat 30 minit sebelum sarapan pagi.'),
(52, 'Glyceryl Trinitrate 0.5mg Sublingual Tablet', '0.5', 'mg', 'Tablet', 'C', '', 'Simpan bawah lidah apabila sakit dada (EMERGENCY)'),
(53, 'Haloperidol 1.5 mg Tablet', '2', 'mg', 'Tablet', 'B', '', ''),
(54, 'Haloperidol 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', ''),
(55, 'Hydrochlorothiazide 25mg Tablet', '25', 'mg', 'Tablet', 'B', '', 'Buang air/Tekanan Darah Tinggi'),
(56, 'Hyoscine N-Butylbromide 10mg Tablet', '10', 'mg', 'Tablet', 'C', '', 'Tahan sakit perut'),
(57, 'Ibuprofen 200 mg Tablet', '200', 'mg', 'Tablet', 'B', '', 'Tahan sakit. Ambil ubat selepas makan.'),
(58, 'Isoniazid 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', ''),
(59, 'Isosorbide Dinitrate 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', ''),
(60, 'Isosorbide-5-Mononitrate 60mg SR Tablet', '60', 'mg', 'Tablet', 'A/KK', '', ''),
(61, 'Labetalol HCl 100mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(62, 'Lamotrigine 100mg Tablet', '100', 'mg', 'Tablet', 'A', '', ''),
(63, 'Levetiracetam 500 mg Tablet', '500', 'mg', 'Tablet', 'A*', '', ''),
(64, 'Levodopa 200 mg, Benserazide 50 mg Tablet (MADOPAR)', '250', 'mg', 'Tablet', 'B', '', ''),
(65, 'Levothyroxine Sodium 100 mcg Tablet', '100', 'mcg', 'Tablet', 'B', '', ''),
(66, 'Loratadine 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Selsema / Tahan gatal'),
(67, 'Losartan 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(68, 'Losartan 50mg Tablet', '50', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(69, 'Magnesium Trisilicate Tablet', '1', 'tablet', 'Tablet', 'C', '', 'Gastrik. Kunyah sebelum telan.'),
(70, 'Mefenamic Acid 250mg Capsule', '250', 'mg', 'Tablet', 'C', '', 'Tahan sakit. Ambil ubat selepas makan.'),
(71, 'Metformin HCl 500 mg Extended Release Tablet', '500', 'mg', 'Tablet', 'B', '', 'Kencing manis. Ambil ubat selepas makan malam.'),
(72, 'Metformin HCl 500 mg Tablet', '500', 'mg', 'Tablet', 'B', '', 'Kencing manis. Ambil ubat selepas makan.'),
(73, 'Methyldopa 250 mg Tablet', '250', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(74, 'Metoclopramide HCl 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Tahan loya/muntah. Ambil bila perlu sahaja'),
(75, 'Metoprolol Tartrate 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(76, 'Metoprolol Tartrate 50 mg Tablet', '50', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(77, 'Metronidazole 200mg Tablet', '200', 'mg', 'Tablet', 'B', '', ''),
(78, 'Multivitamin Tablet', '1', 'tablet', 'Tablet', 'B', '', ''),
(79, 'Nifedipine 10 mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(80, 'Olanzapine 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', ''),
(81, 'Olanzapine 10 mg Tablet', '10', 'mg', 'Tablet', 'B', '', ''),
(82, 'Omeprazole 20 mg Capsule', '20', 'mg', 'Tablet', 'A/KK', '', 'Gastrik. Ambil ubat 1 jam sebelum atau 2 jam selepas makan.'),
(83, 'Oseltamivir 75mg capsule', '75', 'mg', 'Tablet', 'A/KK', '', ''),
(84, 'Paracetamol 500 mg Tablet', '500', 'mg', 'Tablet', 'C', '', 'Tahan sakit/Demam. Ambil bila perlu sahaja.'),
(85, 'Pantoprazole 40 mg Tablet', '40', 'mg', 'Tablet', 'B', '', 'Gastrik. Ambil ubat 1 jam sebelum atau 2 jam selepas makan.'),
(86, 'Perindopril 4 mg Tablet', '4', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(87, 'Phenoxymethyl Penicillin 125mg Tablet', '125', 'mg', 'Tablet', 'C', '', ''),
(88, 'Phenytoin Sodium 100mg Capsule', '100', 'mg', 'Tablet', 'B', '', ''),
(89, 'Phenytoin Sodium 30mg Capsule', '30', 'mg', 'Tablet', 'B', '', ''),
(90, 'Potassium Chloride 600 mg SR Tablet', '600', 'mg', 'Tablet', 'B', '', ''),
(91, 'Prazosin HCl 1 mg Tablet', '1', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(92, 'Prazosin HCl 2 mg Tablet', '2', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(93, 'Prazosin HCl 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(94, 'Pre/Post Natal Vitamin & Mineral Capsule (ZINCOFER)', '1', 'tablet', 'Tablet', 'C', '', 'Tambah darah'),
(95, 'Prednisolone 5mg Tablet', '5', 'mg', 'Tablet', 'B', '', ''),
(96, 'Prochlorperazine Maleate 5mg Tablet', '5', 'mg', 'Tablet', 'B', '', 'Tahan pening. Ambil bila perlu sahaja.'),
(97, 'Prolase Tablet', '1', 'tablet', 'Tablet', 'B', '', 'Kurangkan bengkak/keradangan'),
(98, 'Propranolol HCl 40 mg Tablet', '40', 'mg', 'Tablet', 'B', '', ''),
(99, 'Pyrazinamide 500mg Tablet', '500', 'mg', 'Tablet', 'B', '', ''),
(100, 'Pyridoxine HCl 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', ''),
(101, 'Rifampicin 150 mg, Isoniazid 75 mg (AKURIT-2)', '1', 'tablet', 'Tablet', 'B', '', ''),
(102, 'Rifampicin 150 mg, Isoniazid 75 mg, Pyrazinamide 400 mg & Ethambutol HCl 275 mg Tablet (AKURIT-4)', '1', 'tablet', 'Tablet', 'B', '', ''),
(103, 'Rifampicin 150mg Capsule', '150', 'mg', 'Tablet', 'B', '', ''),
(104, 'Rifampicin 300mg Capsule', '300', 'mg', 'Tablet', 'B', '', ''),
(105, 'Risperidone 1 mg Tablet', '1', 'mg', 'Tablet', 'B', '', ''),
(106, 'Risperidone 2 mg Tablet', '2', 'mg', 'Tablet', 'B', '', ''),
(107, 'Sertraline HCI 50mg Tablet', '50', 'mg', 'Tablet', 'B', '', ''),
(108, 'Simvastatin 10 mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Kolesterol. Ambil ubat pada waktu malam.'),
(109, 'Simvastatin 40 mg Tablet', '40', 'mg', 'Tablet', 'B', '', 'Kolesterol. Ambil ubat pada waktu malam.'),
(110, 'Sodium Valproate 200mg Tablet', '200', 'mg', 'Tablet', 'B', '', ''),
(111, 'Spironolactone 25 mg Tablet', '25', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(112, 'Telmisartan 80 mg Tablet', '80', 'mg', 'Tablet', 'A/KK', '', 'Tekanan Darah Tinggi'),
(113, 'Telmisartan 80 mg & Hydrochlorothiazide 12.5 mg Tablet', '1', 'tablet', 'Tablet', 'A/KK', '', ''),
(114, 'Terazosin HCl 2 mg Tablet', '2', 'mg', 'Tablet', 'A/KK', '', ''),
(115, 'Terazosin HCl 5 mg Tablet', '5', 'mg', 'Tablet', 'A/KK', '', ''),
(116, 'Theophylline 250mg Long Acting Tablet', '250', 'mg', 'Tablet', 'B', '', ''),
(117, 'Tranexamic Acid 250 mg Capsule', '250', 'mg', 'Tablet', 'B', '', ''),
(118, 'Trimetazidine 35mg MR Tablet', '35', 'mg', 'Tablet', 'B', '', ''),
(119, 'Vildagliptin 50mg Tablet', '50', 'mg', 'Tablet', 'A/KK', '', 'Kencing manis'),
(120, 'Vitamin B complex tablet', '1', 'tablet', 'Tablet', 'C', '', ''),
(121, 'Vitamin B1, B6, B12 tablet', '1', 'tablet', 'Tablet', 'B', '', 'Vitamin untuk urat'),
(122, 'Warfarin Sodium 1 mg Tablet', '1', 'tablet', 'Tablet', 'B', '', ''),
(123, 'Warfarin Sodium 2 mg Tablet', '2', 'tablet', 'Tablet', 'B', '', ''),
(124, 'Warfarin Sodium 5 mg Tablet', '5', 'tablet', 'Tablet', 'B', '', ''),
(125, 'Insulin regular (Actrapid) 100 IU/mL Penfill', '300', 'unit/pc', 'Fridge Ite', 'B', '', 'Pastikan suntik 30 minit sebelum makan'),
(126, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 'Fridge Ite', 'B', '', ''),
(127, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 'Fridge Ite', 'B', '', 'Pastikan suntik 30 minit sebelum makan'),
(128, 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', 'Fridge Ite', 'B', '', ''),
(129, 'Insulin glargine (Basalog One) 100 IU/mL Prefilled Pen', '300', 'unit/pc', 'Fridge Ite', 'A/KK', '', ''),
(130, 'Oral Rehydration Salt (ORS)', '1', 'sachet', 'Internal', 'C', '', 'Bancuh 1 paket dalam 1 gelas air. Minum selepas cirit/muntah.'),
(131, 'Thymol Compound Gargle', '1', 'ml', 'Gargle', 'C', '60', 'Sakit tekak. Campur 5 ml dengan 15ml air. Kumur. Bukan untuk diminum.'),
(132, 'Albendazole 200mg/5ml Suspension', '40', 'mg/ml', 'Syrup', 'C', '10', 'Buang cacing'),
(133, 'Amoxicillin Trihydrate 125mg/5ml Suspension', '25', 'mg/ml', 'Syrup', 'B', '', 'Jumpa doktor jika ada alahan'),
(134, 'Bromhexine HCL 4mg/5ml Elixir', '0.8', 'mg/ml', 'Syrup', 'B', '60', 'Cair kahak'),
(135, 'Chlorpheniramine Maleate 2mg/5ml', '0.4', 'mg/ml', 'Syrup', 'C', '60', 'Selsema/Tahan gatal. Boleh menyebabkan mengantuk.'),
(136, 'Diphenhydramine HCL 14mg/5ml', '2.8', 'mg/ml', 'Syrup', 'C', '90', 'Batuk. Boleh menyebabkan mengantuk.'),
(137, 'Diphenhydramine HCL 7mg/5ml', '1.4', 'mg/ml', 'Syrup', 'C', '60', 'Batuk. Boleh menyebabkan mengantuk.'),
(138, 'Erythromycin Ethylsuccinate 200mg/5ml', '40', 'mg/ml', 'Syrup', 'B', '', 'Jumpa doktor jika ada alahan'),
(139, 'Lactulose 3.35g/5ml', '1', 'ml', 'Syrup', 'C', '100', 'Sembelit'),
(140, 'Magnesium Trisilicate Mixture', '1', 'ml', 'Syrup', 'C', '120', 'Gastrik. Goncang dulu sebelum minum.'),
(141, 'Multivitamin Drops for Infant/Paediatric', '1', 'ml', 'Syrup', 'B', '30', ''),
(142, 'Multivitamin Syrup', '1', 'ml', 'Syrup', 'C', '120', ''),
(143, 'Sodium Bicarbonate, Citric Acid, Sodium Citrate and Tartaric Acid - 4 g per sachet', '1', 'paket', 'Packet', 'B', '', 'Bancuh 1 paket dalam 1 gelas air dan minum.'),
(144, 'Nystatin 100,000units/ml suspension', '1', 'ml', 'Syrup', 'B', '', ''),
(145, 'Paracetamol 250mg/5ml Syrup', '50', 'mg/ml', 'Syrup', 'C', '60', 'Demam'),
(146, 'Paracetamol 120mg/5ml Syrup', '24', 'mg/ml', 'Syrup', 'C', '60', 'Demam'),
(147, 'MDI Budesonide 200mcg/dose Inhaler', '1', 'sedut', 'Inhaler', 'B', '', ''),
(148, 'MDI Beclomethasone Dipropionate 100mcg/dose Inhalation', '1', 'sedut', 'Inhaler', 'B', '', ''),
(149, 'MDI Fluticasone propionate 125mcg/dose/ Evohaler', '1', 'sedut', 'Inhaler', 'B', '', ''),
(150, 'MDI Ipratropium Br 20mcg Fenoterol 50mcg/dose Inhalation', '1', 'sedut', 'Inhaler', 'B', '', ''),
(151, 'MDI Ipratropium Bromide 20mcg/dose Inhalation', '1', 'sedut', 'Inhaler', 'B', '', ''),
(152, 'MDI Salbutamol 100mcg/dose Inhaler', '1', 'sedut', 'Inhaler', 'B', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `prescribeddrugs`
--

CREATE TABLE `prescribeddrugs` (
  `ID` int(255) NOT NULL,
  `HistoryID` int(255) NOT NULL,
  `Name` varchar(300) NOT NULL,
  `ICNo` varchar(20) NOT NULL,
  `Date` varchar(25) NOT NULL,
  `DateCollection` varchar(25) NOT NULL,
  `DateSeeDoctor` varchar(25) NOT NULL,
  `Timestamp` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `Drug1Name` varchar(100) NOT NULL,
  `Drug1Strength` varchar(11) NOT NULL,
  `Drug1Unit` varchar(10) NOT NULL,
  `Drug1Dose` varchar(11) NOT NULL,
  `Drug1Freq` varchar(11) NOT NULL,
  `Drug1Duration` varchar(11) NOT NULL,
  `Drug1TotalQTY` varchar(11) NOT NULL,
  `Drug2Name` varchar(100) NOT NULL,
  `Drug2Strength` varchar(11) NOT NULL,
  `Drug2Unit` varchar(10) NOT NULL,
  `Drug2Dose` varchar(11) NOT NULL,
  `Drug2Freq` varchar(11) NOT NULL,
  `Drug2Duration` varchar(11) NOT NULL,
  `Drug2TotalQTY` varchar(11) NOT NULL,
  `Drug3Name` varchar(100) NOT NULL,
  `Drug3Strength` varchar(11) NOT NULL,
  `Drug3Unit` varchar(10) NOT NULL,
  `Drug3Dose` varchar(11) NOT NULL,
  `Drug3Freq` varchar(11) NOT NULL,
  `Drug3Duration` varchar(11) NOT NULL,
  `Drug3TotalQTY` varchar(11) NOT NULL,
  `Drug4Name` varchar(100) NOT NULL,
  `Drug4Strength` varchar(11) NOT NULL,
  `Drug4Unit` varchar(10) NOT NULL,
  `Drug4Dose` varchar(11) NOT NULL,
  `Drug4Freq` varchar(11) NOT NULL,
  `Drug4Duration` varchar(11) NOT NULL,
  `Drug4TotalQTY` varchar(11) NOT NULL,
  `Drug5Name` varchar(100) NOT NULL,
  `Drug5Strength` varchar(11) NOT NULL,
  `Drug5Unit` varchar(10) NOT NULL,
  `Drug5Dose` varchar(11) NOT NULL,
  `Drug5Freq` varchar(11) NOT NULL,
  `Drug5Duration` varchar(11) NOT NULL,
  `Drug5TotalQTY` varchar(11) NOT NULL,
  `Drug6Name` varchar(100) NOT NULL,
  `Drug6Strength` varchar(11) NOT NULL,
  `Drug6Unit` varchar(10) NOT NULL,
  `Drug6Dose` varchar(11) NOT NULL,
  `Drug6Freq` varchar(11) NOT NULL,
  `Drug6Duration` varchar(11) NOT NULL,
  `Drug6TotalQTY` varchar(11) NOT NULL,
  `Drug7Name` varchar(100) NOT NULL,
  `Drug7Strength` varchar(11) NOT NULL,
  `Drug7Unit` varchar(10) NOT NULL,
  `Drug7Dose` varchar(11) NOT NULL,
  `Drug7Freq` varchar(11) NOT NULL,
  `Drug7Duration` varchar(11) NOT NULL,
  `Drug7TotalQTY` varchar(11) NOT NULL,
  `Drug8Name` varchar(100) NOT NULL,
  `Drug8Strength` varchar(11) NOT NULL,
  `Drug8Unit` varchar(10) NOT NULL,
  `Drug8Dose` varchar(11) NOT NULL,
  `Drug8Freq` varchar(11) NOT NULL,
  `Drug8Duration` varchar(11) NOT NULL,
  `Drug8TotalQTY` varchar(11) NOT NULL,
  `Drug9Name` varchar(100) NOT NULL,
  `Drug9Strength` varchar(11) NOT NULL,
  `Drug9Unit` varchar(10) NOT NULL,
  `Drug9Dose` varchar(11) NOT NULL,
  `Drug9Freq` varchar(11) NOT NULL,
  `Drug9Duration` varchar(11) NOT NULL,
  `Drug9TotalQTY` varchar(11) NOT NULL,
  `Drug10Name` varchar(100) NOT NULL,
  `Drug10Strength` varchar(11) NOT NULL,
  `Drug10Unit` varchar(10) NOT NULL,
  `Drug10Dose` varchar(11) NOT NULL,
  `Drug10Freq` varchar(11) NOT NULL,
  `Drug10Duration` varchar(11) NOT NULL,
  `Drug10TotalQTY` varchar(11) NOT NULL,
  `Insulin1Name` varchar(100) NOT NULL,
  `Insulin1Strength` varchar(11) NOT NULL,
  `Insulin1Unit` varchar(10) NOT NULL,
  `Insulin1MorDose` varchar(11) NOT NULL,
  `Insulin1NoonDose` varchar(11) NOT NULL,
  `Insulin1AfternoonDose` varchar(11) NOT NULL,
  `Insulin1NightDose` varchar(11) NOT NULL,
  `Insulin1Freq` varchar(11) NOT NULL,
  `Insulin1Duration` varchar(11) NOT NULL,
  `Insulin1TotalDose` varchar(11) NOT NULL,
  `Insulin1POM` varchar(11) NOT NULL,
  `Insulin1CartQTY` varchar(11) NOT NULL,
  `Insulin2Name` varchar(100) NOT NULL,
  `Insulin2Strength` varchar(11) NOT NULL,
  `Insulin2Unit` varchar(10) NOT NULL,
  `Insulin2MorDose` varchar(11) NOT NULL,
  `Insulin2NoonDose` varchar(11) NOT NULL,
  `Insulin2AfternoonDose` varchar(11) NOT NULL,
  `Insulin2NightDose` varchar(11) NOT NULL,
  `Insulin2Freq` varchar(11) NOT NULL,
  `Insulin2Duration` varchar(11) NOT NULL,
  `Insulin2TotalDose` varchar(11) NOT NULL,
  `Insulin2POM` varchar(11) NOT NULL,
  `Insulin2CartQTY` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prescribeddrugs`
--

INSERT INTO `prescribeddrugs` (`ID`, `HistoryID`, `Name`, `ICNo`, `Date`, `DateCollection`, `DateSeeDoctor`, `Timestamp`, `Drug1Name`, `Drug1Strength`, `Drug1Unit`, `Drug1Dose`, `Drug1Freq`, `Drug1Duration`, `Drug1TotalQTY`, `Drug2Name`, `Drug2Strength`, `Drug2Unit`, `Drug2Dose`, `Drug2Freq`, `Drug2Duration`, `Drug2TotalQTY`, `Drug3Name`, `Drug3Strength`, `Drug3Unit`, `Drug3Dose`, `Drug3Freq`, `Drug3Duration`, `Drug3TotalQTY`, `Drug4Name`, `Drug4Strength`, `Drug4Unit`, `Drug4Dose`, `Drug4Freq`, `Drug4Duration`, `Drug4TotalQTY`, `Drug5Name`, `Drug5Strength`, `Drug5Unit`, `Drug5Dose`, `Drug5Freq`, `Drug5Duration`, `Drug5TotalQTY`, `Drug6Name`, `Drug6Strength`, `Drug6Unit`, `Drug6Dose`, `Drug6Freq`, `Drug6Duration`, `Drug6TotalQTY`, `Drug7Name`, `Drug7Strength`, `Drug7Unit`, `Drug7Dose`, `Drug7Freq`, `Drug7Duration`, `Drug7TotalQTY`, `Drug8Name`, `Drug8Strength`, `Drug8Unit`, `Drug8Dose`, `Drug8Freq`, `Drug8Duration`, `Drug8TotalQTY`, `Drug9Name`, `Drug9Strength`, `Drug9Unit`, `Drug9Dose`, `Drug9Freq`, `Drug9Duration`, `Drug9TotalQTY`, `Drug10Name`, `Drug10Strength`, `Drug10Unit`, `Drug10Dose`, `Drug10Freq`, `Drug10Duration`, `Drug10TotalQTY`, `Insulin1Name`, `Insulin1Strength`, `Insulin1Unit`, `Insulin1MorDose`, `Insulin1NoonDose`, `Insulin1AfternoonDose`, `Insulin1NightDose`, `Insulin1Freq`, `Insulin1Duration`, `Insulin1TotalDose`, `Insulin1POM`, `Insulin1CartQTY`, `Insulin2Name`, `Insulin2Strength`, `Insulin2Unit`, `Insulin2MorDose`, `Insulin2NoonDose`, `Insulin2AfternoonDose`, `Insulin2NightDose`, `Insulin2Freq`, `Insulin2Duration`, `Insulin2TotalDose`, `Insulin2POM`, `Insulin2CartQTY`) VALUES
(1, 0, 'TESTING 1', '111111-11-1111', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:04:44', 'Acarbose 50mg Tablet', '50', 'mg', '50', '1', '30', '30', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Metformin HCl 500 mg Tablet', '500', 'mg', '1000', '1', '30', '60', 'Gemfibrozil 300mg Capsule', '300', 'mg', '600', '1', '30', '60', 'Vildagliptin 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular (Actrapid) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '1', '2', '', '', '', '', '', '', '', '1', '', '', '', ''),
(2, 0, 'TESTING 2', '111111-11-1112', '16/5/2024', 'Saturday, 15 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-16 13:44:44', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '1', '30', '60', 'Atenolol 50 mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Hydrochlorothiazide 25mg Tablet', '25', 'mg', '50', '1', '30', '60', 'Chlorpheniramine Maleate 4mg Tablet', '4', 'mg', '8', '1', '30', '60', 'Simvastatin 40 mg Tablet', '40', 'mg', '80', '1', '30', '60', 'Perindopril 4 mg Tablet', '4', 'mg', '8', '1', '30', '60', 'Spironolactone 25 mg Tablet', '25', 'mg', '50', '1', '30', '60', 'Calcium Lactate 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '1', '2', 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', '12', '12', '', '', '1', '30', '840', '', '3'),
(3, 0, 'TESTING 3', '111111-11-1113', '16/5/2024', 'Saturday, 15 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-16 13:47:21', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '2', '30', '120', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '2', '30', '60', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Atenolol 100 mg Tablet', '100', 'mg', '200', '2', '30', '120', 'Gliclazide 80 mg Tablet', '80', 'mg', '160', '2', '30', '120', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '', '', '', '12', '1', '30', '420', '', '2', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', '12', '12', '12', '', '1', '30', '1260', '1', '4'),
(4, 0, 'TESTING 4', '111111-11-1114', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 08:09:12', 'Amlodipine 5 mg Tablet', '5', 'mg', '12', '1', '30', '72', 'Atorvastatin 20 mg Tablet', '20', 'mg', '2', '2', '30', '6', 'Amlodipine 10 mg Tablet', '10', 'mg', '12', '1', '30', '36', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '100', '1', '30', '30', 'Atenolol 100 mg Tablet', '100', 'mg', '500', '2', '30', '300', 'Amoxicillin (Amoxycillin) 250mg Capsule', '250', 'mg', '250', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '25', '2', '30', '60', 'Albendazole 200mg Tablet', '200', 'mg', '400', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular (Actrapid) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '1', '2', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3'),
(5, 0, 'TESTING 5', '111111-11-1115', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 09:04:26', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Acarbose 50mg Tablet', '50', 'mg', '100', '2', '30', '120', 'Amlodipine 10 mg Tablet', '10', 'mg', '10', '2', '30', '60', 'Acarbose 50mg Tablet', '50', 'mg', '50', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin glargine (Basalog One) 100 IU/mL Prefilled Pen', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '1', '2', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3'),
(36, 0, 'TESTING 6', '111111-11-1116', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 19:18:12', 'Albendazole 200mg Tablet', '200', 'mg', '400', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '2', '30', '120', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '2', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '1', '2', 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3'),
(37, 0, 'TESTING 7', '111111-11-1117', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:11:32', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Magnesium Trisilicate Tablet', '1', 'tablet', '1', '2', '30', '60', 'Albendazole 200mg Tablet', '200', 'mg', '200', '1', '30', '30', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3', 'Insulin regular (Actrapid) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3'),
(38, 0, 'TESTING 8', '111111-11-1118', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 00:55:37', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3'),
(39, 0, 'TESTING 9', '111111-11-1119', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 22:55:58', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Albendazole 200mg Tablet', '200', 'mg', '200', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '25', '1', '30', '30', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3', '', '', '', '', '', '', '', '1', '', '', '', ''),
(63, 0, 'TESTING 10', '101010-10-1010', '19/5/2024', 'Thursday, 20 June, 2024', 'Tuesday, 20 August, 2024', '2024-05-19 23:57:25', 'Losartan 100 mg Tablet', '100', 'mg', '100', '1', '32', '32', 'Lactulose 3.35g/5ml', '1', 'ml', '10', '1', '32', '320', 'Potassium Chloride 600 mg SR Tablet', '600', 'mg', '1200', '1', '32', '64', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '1', '', '', '', '', '', '', '', '', '', '', '', '1', '', '', '', ''),
(66, 0, 'TEST 1', '111111-11-1100', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:05:40', 'Mefenamic Acid 250mg Capsule', '250', 'mg', '250', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '1', '', '', '', '', '', '', '', '', '', '', '', '1', '', '', '', ''),
(68, 0, 'TEST 2', '111111-11-1101', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:06:33', 'Metoprolol Tartrate 100 mg Tablet', '100', 'mg', '100', '1', '30', '30', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'MDI Fluticasone propionate 125mcg/dose/ Evohaler', '1', 'sedut', '1', '1', '30', '30', 'Metformin HCl 500 mg Tablet', '500', 'mg', '500', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3', '', '', '', '', '', '', '', '1', '', '', '', ''),
(70, 0, 'TEST 3', '111111-11-1103', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:33:14', 'Haloperidol 1.5 mg Tablet', '2', 'mg', '4', '1', '30', '60', 'Albendazole 200mg/5ml Suspension', '40', 'mg/ml', '40', '1', '30', '30', 'Cephalexin Monohydrate 250 mg Capsule', '250', 'mg', '500', '1', '30', '60', 'Omeprazole 20 mg Capsule', '20', 'mg', '40', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3', '', '', '', '', '', '', '', '1', '', '', '', ''),
(71, 0, 'TEST 4', '111111-11-1104', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:44:33', 'Isoniazid 100 mg Tablet', '100', 'mg', '200', '1', '30', '60', 'Erythromycin Ethylsuccinate 400 mg Tablet', '400', 'mg', '800', '1', '30', '60', 'Diclofenac 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '', '3', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', '', '12', '', '12', '1', '30', '840', '', '3'),
(75, 0, 'TEST 5', '111111-11-1105', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 03:12:43', 'Ferrous Fumarate 200 mg Tablet', '200', 'mg', '200', '1', '30', '30', 'Digoxin 0.25mg Tablet', '0.25', 'mg', '0.25', '1', '30', '30', 'Isosorbide-5-Mononitrate 60mg SR Tablet', '60', 'mg', '60', '1', '30', '30', 'Olanzapine 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', '12', '', '12', '', '1', '30', '840', '1', '2', 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', '', '12', '', '12', '1', '30', '840', '', '3');

-- --------------------------------------------------------

--
-- Table structure for table `prescribeddrugshistory`
--

CREATE TABLE `prescribeddrugshistory` (
  `PrevID` int(255) NOT NULL,
  `ID` int(255) NOT NULL,
  `Name` varchar(300) NOT NULL,
  `ICNo` varchar(20) NOT NULL,
  `Date` varchar(25) NOT NULL,
  `DateCollection` varchar(25) NOT NULL,
  `DateSeeDoctor` varchar(25) NOT NULL,
  `Timestamp` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `Drug1Name` varchar(100) NOT NULL,
  `Drug1Strength` varchar(11) NOT NULL,
  `Drug1Unit` varchar(10) NOT NULL,
  `Drug1Dose` varchar(11) NOT NULL,
  `Drug1Freq` varchar(11) NOT NULL,
  `Drug1Duration` varchar(11) NOT NULL,
  `Drug1TotalQTY` varchar(11) NOT NULL,
  `Drug2Name` varchar(100) NOT NULL,
  `Drug2Strength` varchar(11) NOT NULL,
  `Drug2Unit` varchar(10) NOT NULL,
  `Drug2Dose` varchar(11) NOT NULL,
  `Drug2Freq` varchar(11) NOT NULL,
  `Drug2Duration` varchar(11) NOT NULL,
  `Drug2TotalQTY` varchar(11) NOT NULL,
  `Drug3Name` varchar(100) NOT NULL,
  `Drug3Strength` varchar(11) NOT NULL,
  `Drug3Unit` varchar(10) NOT NULL,
  `Drug3Dose` varchar(11) NOT NULL,
  `Drug3Freq` varchar(11) NOT NULL,
  `Drug3Duration` varchar(11) NOT NULL,
  `Drug3TotalQTY` varchar(11) NOT NULL,
  `Drug4Name` varchar(100) NOT NULL,
  `Drug4Strength` varchar(11) NOT NULL,
  `Drug4Unit` varchar(10) NOT NULL,
  `Drug4Dose` varchar(11) NOT NULL,
  `Drug4Freq` varchar(11) NOT NULL,
  `Drug4Duration` varchar(11) NOT NULL,
  `Drug4TotalQTY` varchar(11) NOT NULL,
  `Drug5Name` varchar(100) NOT NULL,
  `Drug5Strength` varchar(11) NOT NULL,
  `Drug5Unit` varchar(10) NOT NULL,
  `Drug5Dose` varchar(11) NOT NULL,
  `Drug5Freq` varchar(11) NOT NULL,
  `Drug5Duration` varchar(11) NOT NULL,
  `Drug5TotalQTY` varchar(11) NOT NULL,
  `Drug6Name` varchar(100) NOT NULL,
  `Drug6Strength` varchar(11) NOT NULL,
  `Drug6Unit` varchar(10) NOT NULL,
  `Drug6Dose` varchar(11) NOT NULL,
  `Drug6Freq` varchar(11) NOT NULL,
  `Drug6Duration` varchar(11) NOT NULL,
  `Drug6TotalQTY` varchar(11) NOT NULL,
  `Drug7Name` varchar(100) NOT NULL,
  `Drug7Strength` varchar(11) NOT NULL,
  `Drug7Unit` varchar(10) NOT NULL,
  `Drug7Dose` varchar(11) NOT NULL,
  `Drug7Freq` varchar(11) NOT NULL,
  `Drug7Duration` varchar(11) NOT NULL,
  `Drug7TotalQTY` varchar(11) NOT NULL,
  `Drug8Name` varchar(100) NOT NULL,
  `Drug8Strength` varchar(11) NOT NULL,
  `Drug8Unit` varchar(10) NOT NULL,
  `Drug8Dose` varchar(11) NOT NULL,
  `Drug8Freq` varchar(11) NOT NULL,
  `Drug8Duration` varchar(11) NOT NULL,
  `Drug8TotalQTY` varchar(11) NOT NULL,
  `Drug9Name` varchar(100) NOT NULL,
  `Drug9Strength` varchar(11) NOT NULL,
  `Drug9Unit` varchar(10) NOT NULL,
  `Drug9Dose` varchar(11) NOT NULL,
  `Drug9Freq` varchar(11) NOT NULL,
  `Drug9Duration` varchar(11) NOT NULL,
  `Drug9TotalQTY` varchar(11) NOT NULL,
  `Drug10Name` varchar(100) NOT NULL,
  `Drug10Strength` varchar(11) NOT NULL,
  `Drug10Unit` varchar(10) NOT NULL,
  `Drug10Dose` varchar(11) NOT NULL,
  `Drug10Freq` varchar(11) NOT NULL,
  `Drug10Duration` varchar(11) NOT NULL,
  `Drug10TotalQTY` varchar(11) NOT NULL,
  `Insulin1Name` varchar(100) NOT NULL,
  `Insulin1Strength` varchar(11) NOT NULL,
  `Insulin1Unit` varchar(10) NOT NULL,
  `Insulin1MorDose` int(11) NOT NULL,
  `Insulin1NoonDose` int(11) NOT NULL,
  `Insulin1AfternoonDose` int(11) NOT NULL,
  `Insulin1NightDose` int(11) NOT NULL,
  `Insulin1Freq` int(11) NOT NULL,
  `Insulin1Duration` varchar(11) NOT NULL,
  `Insulin1TotalDose` int(11) NOT NULL,
  `Insulin1POM` int(11) NOT NULL,
  `Insulin1CartQTY` int(11) NOT NULL,
  `Insulin2Name` varchar(100) NOT NULL,
  `Insulin2Strength` varchar(11) NOT NULL,
  `Insulin2Unit` varchar(10) NOT NULL,
  `Insulin2MorDose` int(11) NOT NULL,
  `Insulin2NoonDose` int(11) NOT NULL,
  `Insulin2AfternoonDose` int(11) NOT NULL,
  `Insulin2NightDose` int(11) NOT NULL,
  `Insulin2Freq` int(11) NOT NULL,
  `Insulin2Duration` varchar(11) NOT NULL,
  `Insulin2TotalDose` int(11) NOT NULL,
  `Insulin2POM` int(11) NOT NULL,
  `Insulin2CartQTY` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `prescribeddrugshistory`
--

INSERT INTO `prescribeddrugshistory` (`PrevID`, `ID`, `Name`, `ICNo`, `Date`, `DateCollection`, `DateSeeDoctor`, `Timestamp`, `Drug1Name`, `Drug1Strength`, `Drug1Unit`, `Drug1Dose`, `Drug1Freq`, `Drug1Duration`, `Drug1TotalQTY`, `Drug2Name`, `Drug2Strength`, `Drug2Unit`, `Drug2Dose`, `Drug2Freq`, `Drug2Duration`, `Drug2TotalQTY`, `Drug3Name`, `Drug3Strength`, `Drug3Unit`, `Drug3Dose`, `Drug3Freq`, `Drug3Duration`, `Drug3TotalQTY`, `Drug4Name`, `Drug4Strength`, `Drug4Unit`, `Drug4Dose`, `Drug4Freq`, `Drug4Duration`, `Drug4TotalQTY`, `Drug5Name`, `Drug5Strength`, `Drug5Unit`, `Drug5Dose`, `Drug5Freq`, `Drug5Duration`, `Drug5TotalQTY`, `Drug6Name`, `Drug6Strength`, `Drug6Unit`, `Drug6Dose`, `Drug6Freq`, `Drug6Duration`, `Drug6TotalQTY`, `Drug7Name`, `Drug7Strength`, `Drug7Unit`, `Drug7Dose`, `Drug7Freq`, `Drug7Duration`, `Drug7TotalQTY`, `Drug8Name`, `Drug8Strength`, `Drug8Unit`, `Drug8Dose`, `Drug8Freq`, `Drug8Duration`, `Drug8TotalQTY`, `Drug9Name`, `Drug9Strength`, `Drug9Unit`, `Drug9Dose`, `Drug9Freq`, `Drug9Duration`, `Drug9TotalQTY`, `Drug10Name`, `Drug10Strength`, `Drug10Unit`, `Drug10Dose`, `Drug10Freq`, `Drug10Duration`, `Drug10TotalQTY`, `Insulin1Name`, `Insulin1Strength`, `Insulin1Unit`, `Insulin1MorDose`, `Insulin1NoonDose`, `Insulin1AfternoonDose`, `Insulin1NightDose`, `Insulin1Freq`, `Insulin1Duration`, `Insulin1TotalDose`, `Insulin1POM`, `Insulin1CartQTY`, `Insulin2Name`, `Insulin2Strength`, `Insulin2Unit`, `Insulin2MorDose`, `Insulin2NoonDose`, `Insulin2AfternoonDose`, `Insulin2NightDose`, `Insulin2Freq`, `Insulin2Duration`, `Insulin2TotalDose`, `Insulin2POM`, `Insulin2CartQTY`) VALUES
(5, 59, 'TESTING 5', '111111-11-1115', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 08:06:23', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin glargine (Basalog One) 100 IU/mL Prefilled Pen', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 1, 2, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(5, 60, 'TESTING 5', '111111-11-1115', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 08:58:07', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin glargine (Basalog One) 100 IU/mL Prefilled Pen', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 1, 2, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(5, 61, 'TESTING 5', '111111-11-1115', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 08:58:07', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin glargine (Basalog One) 100 IU/mL Prefilled Pen', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 1, 2, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(5, 62, 'TESTING 5', '111111-11-1115', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 09:03:58', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '600', '1', '30', '60', 'Acarbose 50mg Tablet', '50', 'mg', '100', '2', '30', '120', 'Amlodipine 10 mg Tablet', '10', 'mg', '10', '2', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin glargine (Basalog One) 100 IU/mL Prefilled Pen', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 1, 2, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 63, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 22:48:27', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 64, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 22:48:27', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(1, 65, 'TESTING 1', '111111-11-1111', '16/5/2024', 'Saturday, 15 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-16 13:39:40', 'Acarbose 50mg Tablet', '50', 'mg', '50', '1', '30', '30', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Metformin HCl 500 mg Tablet', '500', 'mg', '1000', '1', '30', '60', 'Gemfibrozil 300mg Capsule', '300', 'mg', '600', '1', '30', '60', 'Vildagliptin 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular (Actrapid) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 1, 2, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 66, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 22:48:27', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(37, 67, 'TESTING 7', '111111-11-1117', '18/5/2024', 'Monday, 17 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-18 20:33:38', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Magnesium Trisilicate Tablet', '1', 'tablet', '1', '2', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(37, 68, 'TESTING 7', '111111-11-1117', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:10:26', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Magnesium Trisilicate Tablet', '1', 'tablet', '1', '2', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(37, 69, 'TESTING 7', '111111-11-1117', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:10:44', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Magnesium Trisilicate Tablet', '1', 'tablet', '1', '2', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin regular (Actrapid) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 70, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 22:48:27', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 71, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:13:08', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 72, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:13:08', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 73, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:13:08', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 74, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:13:08', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 75, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:21:07', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 76, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:21:31', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 77, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:21:31', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '300', '1', '30', '30', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 78, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:27:45', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 79, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:27:45', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(38, 80, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 81, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 82, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 83, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 84, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 85, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(38, 86, 'TESTING 8', '111111-11-1118', '19/5/2024', 'Tuesday, 18 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-19 23:35:09', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Allopurinol 300 mg Tablet', '300', 'mg', '300', '1', '30', '30', 'Amitriptyline HCl 25 mg Tablet', '25', 'mg', '50', '2', '30', '120', 'Ascorbic Acid 100mg Tablet', '100', 'mg', '200', '3', '30', '180', 'Terazosin HCl 5 mg Tablet', '5', 'mg', '10', '1', '30', '60', 'Acetylsalicylic Acid 300 mg Soluble Tablet', '300', 'mg', '600', '1', '30', '60', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'Acarbose 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3),
(66, 87, 'TEST 1', '111111-11-1100', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:05:17', 'Mefenamic Acid 250mg Capsule', '250', 'mg', '250', '1', '30', '30', 'Multivitamin Syrup', '1', 'ml', '1', '1', '30', '30', 'Prazosin HCl 5 mg Tablet', '5', 'mg', '5', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(68, 88, 'TEST 2', '111111-11-1101', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:06:33', 'Metoprolol Tartrate 100 mg Tablet', '100', 'mg', '100', '1', '30', '30', 'Acetylsalicylic Acid 100 mg & Glycine 45 mg Tablet', '1', 'Tablet', '1', '1', '30', '30', 'MDI Fluticasone propionate 125mcg/dose/ Evohaler', '1', 'sedut', '1', '1', '30', '30', 'Metformin HCl 500 mg Tablet', '500', 'mg', '500', '1', '30', '30', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin isophane (Insulatard) 100 IU/mL Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, '', '', '', 0, 0, 0, 0, 1, '', 0, 0, 0),
(71, 89, 'TEST 4', '111111-11-1104', '20/5/2024', 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:44:33', 'Isoniazid 100 mg Tablet', '100', 'mg', '200', '1', '30', '60', 'Erythromycin Ethylsuccinate 400 mg Tablet', '400', 'mg', '800', '1', '30', '60', 'Diclofenac 50mg Tablet', '50', 'mg', '100', '1', '30', '60', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 'Insulin Recombinant Synthetic Human, Intermediate-Acting (Insugen-N) 100IU/ml Penfill', '300', 'unit/pc', 12, 0, 12, 0, 1, '30', 840, 0, 3, 'Insulin regular/isophane (Mixtard-30) 100 IU/mL Penfill', '300', 'unit/pc', 0, 12, 0, 12, 1, '30', 840, 0, 3);

-- --------------------------------------------------------

--
-- Table structure for table `records`
--

CREATE TABLE `records` (
  `ID` int(11) NOT NULL,
  `Name` varchar(25) NOT NULL,
  `ICNo` varchar(25) NOT NULL,
  `NewPatient` int(2) NOT NULL,
  `IOU` int(2) NOT NULL,
  `NoOfItems` int(2) NOT NULL,
  `DateCollection` varchar(25) NOT NULL,
  `DateSeeDoctor` varchar(25) NOT NULL,
  `Timestamp` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `records`
--

INSERT INTO `records` (`ID`, `Name`, `ICNo`, `NewPatient`, `IOU`, `NoOfItems`, `DateCollection`, `DateSeeDoctor`, `Timestamp`) VALUES
(26, 'TESTING 10', '101010-10-1010', 1, 0, 3, 'Thursday, 20 June, 2024', 'Tuesday, 20 August, 2024', '2024-05-19 23:57:39'),
(27, 'TESTING 8', '111111-11-1118', 1, 0, 8, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 00:55:41'),
(28, 'TEST 1', '111111-11-1100', 1, 0, 3, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:05:27'),
(29, 'TEST 1', '111111-11-1100', 1, 0, 1, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:05:49'),
(30, 'TEST 2', '111111-11-1101', 1, 0, 5, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:07:01'),
(31, 'TEST 2', '111111-11-1101', 0, 1, 5, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:07:43'),
(32, 'TEST 3', '111111-11-1103', 1, 0, 5, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:34:53'),
(33, 'TEST 4', '111111-11-1104', 1, 0, 5, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:45:52'),
(34, 'TEST 4', '111111-11-1104', 1, 0, 5, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 01:46:15'),
(35, 'TEST 5', '111111-11-1105', 1, 0, 6, 'Wednesday, 19 June, 2024', 'Wednesday, 19 June, 2024', '2024-05-20 03:12:55');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `drugtable`
--
ALTER TABLE `drugtable`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `prescribeddrugs`
--
ALTER TABLE `prescribeddrugs`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ICNo` (`ICNo`);

--
-- Indexes for table `prescribeddrugshistory`
--
ALTER TABLE `prescribeddrugshistory`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `records`
--
ALTER TABLE `records`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `drugtable`
--
ALTER TABLE `drugtable`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=153;

--
-- AUTO_INCREMENT for table `prescribeddrugs`
--
ALTER TABLE `prescribeddrugs`
  MODIFY `ID` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=76;

--
-- AUTO_INCREMENT for table `prescribeddrugshistory`
--
ALTER TABLE `prescribeddrugshistory`
  MODIFY `ID` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=90;

--
-- AUTO_INCREMENT for table `records`
--
ALTER TABLE `records`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;
--
-- Database: `phpmyadmin`
--
CREATE DATABASE IF NOT EXISTS `phpmyadmin` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin;
USE `phpmyadmin`;

-- --------------------------------------------------------

--
-- Table structure for table `pma__bookmark`
--

CREATE TABLE `pma__bookmark` (
  `id` int(10) UNSIGNED NOT NULL,
  `dbase` varchar(255) NOT NULL DEFAULT '',
  `user` varchar(255) NOT NULL DEFAULT '',
  `label` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `query` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Bookmarks';

-- --------------------------------------------------------

--
-- Table structure for table `pma__central_columns`
--

CREATE TABLE `pma__central_columns` (
  `db_name` varchar(64) NOT NULL,
  `col_name` varchar(64) NOT NULL,
  `col_type` varchar(64) NOT NULL,
  `col_length` text DEFAULT NULL,
  `col_collation` varchar(64) NOT NULL,
  `col_isNull` tinyint(1) NOT NULL,
  `col_extra` varchar(255) DEFAULT '',
  `col_default` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Central list of columns';

-- --------------------------------------------------------

--
-- Table structure for table `pma__column_info`
--

CREATE TABLE `pma__column_info` (
  `id` int(5) UNSIGNED NOT NULL,
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `column_name` varchar(64) NOT NULL DEFAULT '',
  `comment` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `mimetype` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT '',
  `transformation` varchar(255) NOT NULL DEFAULT '',
  `transformation_options` varchar(255) NOT NULL DEFAULT '',
  `input_transformation` varchar(255) NOT NULL DEFAULT '',
  `input_transformation_options` varchar(255) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Column information for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__designer_settings`
--

CREATE TABLE `pma__designer_settings` (
  `username` varchar(64) NOT NULL,
  `settings_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Settings related to Designer';

-- --------------------------------------------------------

--
-- Table structure for table `pma__export_templates`
--

CREATE TABLE `pma__export_templates` (
  `id` int(5) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL,
  `export_type` varchar(10) NOT NULL,
  `template_name` varchar(64) NOT NULL,
  `template_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Saved export templates';

-- --------------------------------------------------------

--
-- Table structure for table `pma__favorite`
--

CREATE TABLE `pma__favorite` (
  `username` varchar(64) NOT NULL,
  `tables` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Favorite tables';

-- --------------------------------------------------------

--
-- Table structure for table `pma__history`
--

CREATE TABLE `pma__history` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL DEFAULT '',
  `db` varchar(64) NOT NULL DEFAULT '',
  `table` varchar(64) NOT NULL DEFAULT '',
  `timevalue` timestamp NOT NULL DEFAULT current_timestamp(),
  `sqlquery` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='SQL history for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__navigationhiding`
--

CREATE TABLE `pma__navigationhiding` (
  `username` varchar(64) NOT NULL,
  `item_name` varchar(64) NOT NULL,
  `item_type` varchar(64) NOT NULL,
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Hidden items of navigation tree';

-- --------------------------------------------------------

--
-- Table structure for table `pma__pdf_pages`
--

CREATE TABLE `pma__pdf_pages` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `page_nr` int(10) UNSIGNED NOT NULL,
  `page_descr` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='PDF relation pages for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__recent`
--

CREATE TABLE `pma__recent` (
  `username` varchar(64) NOT NULL,
  `tables` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Recently accessed tables';

--
-- Dumping data for table `pma__recent`
--

INSERT INTO `pma__recent` (`username`, `tables`) VALUES
('root', '[{\"db\":\"database_pharmacy\",\"table\":\"drugtable\"},{\"db\":\"database_pharmacy\",\"table\":\"prescribeddrugs\"},{\"db\":\"database_pharmacy\",\"table\":\"records\"},{\"db\":\"database_pharmacy\",\"table\":\"prescribeddrugshistory\"}]');

-- --------------------------------------------------------

--
-- Table structure for table `pma__relation`
--

CREATE TABLE `pma__relation` (
  `master_db` varchar(64) NOT NULL DEFAULT '',
  `master_table` varchar(64) NOT NULL DEFAULT '',
  `master_field` varchar(64) NOT NULL DEFAULT '',
  `foreign_db` varchar(64) NOT NULL DEFAULT '',
  `foreign_table` varchar(64) NOT NULL DEFAULT '',
  `foreign_field` varchar(64) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Relation table';

-- --------------------------------------------------------

--
-- Table structure for table `pma__savedsearches`
--

CREATE TABLE `pma__savedsearches` (
  `id` int(5) UNSIGNED NOT NULL,
  `username` varchar(64) NOT NULL DEFAULT '',
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `search_name` varchar(64) NOT NULL DEFAULT '',
  `search_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Saved searches';

-- --------------------------------------------------------

--
-- Table structure for table `pma__table_coords`
--

CREATE TABLE `pma__table_coords` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `pdf_page_number` int(11) NOT NULL DEFAULT 0,
  `x` float UNSIGNED NOT NULL DEFAULT 0,
  `y` float UNSIGNED NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Table coordinates for phpMyAdmin PDF output';

-- --------------------------------------------------------

--
-- Table structure for table `pma__table_info`
--

CREATE TABLE `pma__table_info` (
  `db_name` varchar(64) NOT NULL DEFAULT '',
  `table_name` varchar(64) NOT NULL DEFAULT '',
  `display_field` varchar(64) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Table information for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__table_uiprefs`
--

CREATE TABLE `pma__table_uiprefs` (
  `username` varchar(64) NOT NULL,
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL,
  `prefs` text NOT NULL,
  `last_update` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Tables'' UI preferences';

-- --------------------------------------------------------

--
-- Table structure for table `pma__tracking`
--

CREATE TABLE `pma__tracking` (
  `db_name` varchar(64) NOT NULL,
  `table_name` varchar(64) NOT NULL,
  `version` int(10) UNSIGNED NOT NULL,
  `date_created` datetime NOT NULL,
  `date_updated` datetime NOT NULL,
  `schema_snapshot` text NOT NULL,
  `schema_sql` text DEFAULT NULL,
  `data_sql` longtext DEFAULT NULL,
  `tracking` set('UPDATE','REPLACE','INSERT','DELETE','TRUNCATE','CREATE DATABASE','ALTER DATABASE','DROP DATABASE','CREATE TABLE','ALTER TABLE','RENAME TABLE','DROP TABLE','CREATE INDEX','DROP INDEX','CREATE VIEW','ALTER VIEW','DROP VIEW') DEFAULT NULL,
  `tracking_active` int(1) UNSIGNED NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Database changes tracking for phpMyAdmin';

-- --------------------------------------------------------

--
-- Table structure for table `pma__userconfig`
--

CREATE TABLE `pma__userconfig` (
  `username` varchar(64) NOT NULL,
  `timevalue` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `config_data` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='User preferences storage for phpMyAdmin';

--
-- Dumping data for table `pma__userconfig`
--

INSERT INTO `pma__userconfig` (`username`, `timevalue`, `config_data`) VALUES
('root', '2024-05-20 05:07:42', '{\"Console\\/Mode\":\"collapse\"}');

-- --------------------------------------------------------

--
-- Table structure for table `pma__usergroups`
--

CREATE TABLE `pma__usergroups` (
  `usergroup` varchar(64) NOT NULL,
  `tab` varchar(64) NOT NULL,
  `allowed` enum('Y','N') NOT NULL DEFAULT 'N'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='User groups with configured menu items';

-- --------------------------------------------------------

--
-- Table structure for table `pma__users`
--

CREATE TABLE `pma__users` (
  `username` varchar(64) NOT NULL,
  `usergroup` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin COMMENT='Users and their assignments to user groups';

--
-- Indexes for dumped tables
--

--
-- Indexes for table `pma__bookmark`
--
ALTER TABLE `pma__bookmark`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pma__central_columns`
--
ALTER TABLE `pma__central_columns`
  ADD PRIMARY KEY (`db_name`,`col_name`);

--
-- Indexes for table `pma__column_info`
--
ALTER TABLE `pma__column_info`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `db_name` (`db_name`,`table_name`,`column_name`);

--
-- Indexes for table `pma__designer_settings`
--
ALTER TABLE `pma__designer_settings`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__export_templates`
--
ALTER TABLE `pma__export_templates`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `u_user_type_template` (`username`,`export_type`,`template_name`);

--
-- Indexes for table `pma__favorite`
--
ALTER TABLE `pma__favorite`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__history`
--
ALTER TABLE `pma__history`
  ADD PRIMARY KEY (`id`),
  ADD KEY `username` (`username`,`db`,`table`,`timevalue`);

--
-- Indexes for table `pma__navigationhiding`
--
ALTER TABLE `pma__navigationhiding`
  ADD PRIMARY KEY (`username`,`item_name`,`item_type`,`db_name`,`table_name`);

--
-- Indexes for table `pma__pdf_pages`
--
ALTER TABLE `pma__pdf_pages`
  ADD PRIMARY KEY (`page_nr`),
  ADD KEY `db_name` (`db_name`);

--
-- Indexes for table `pma__recent`
--
ALTER TABLE `pma__recent`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__relation`
--
ALTER TABLE `pma__relation`
  ADD PRIMARY KEY (`master_db`,`master_table`,`master_field`),
  ADD KEY `foreign_field` (`foreign_db`,`foreign_table`);

--
-- Indexes for table `pma__savedsearches`
--
ALTER TABLE `pma__savedsearches`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `u_savedsearches_username_dbname` (`username`,`db_name`,`search_name`);

--
-- Indexes for table `pma__table_coords`
--
ALTER TABLE `pma__table_coords`
  ADD PRIMARY KEY (`db_name`,`table_name`,`pdf_page_number`);

--
-- Indexes for table `pma__table_info`
--
ALTER TABLE `pma__table_info`
  ADD PRIMARY KEY (`db_name`,`table_name`);

--
-- Indexes for table `pma__table_uiprefs`
--
ALTER TABLE `pma__table_uiprefs`
  ADD PRIMARY KEY (`username`,`db_name`,`table_name`);

--
-- Indexes for table `pma__tracking`
--
ALTER TABLE `pma__tracking`
  ADD PRIMARY KEY (`db_name`,`table_name`,`version`);

--
-- Indexes for table `pma__userconfig`
--
ALTER TABLE `pma__userconfig`
  ADD PRIMARY KEY (`username`);

--
-- Indexes for table `pma__usergroups`
--
ALTER TABLE `pma__usergroups`
  ADD PRIMARY KEY (`usergroup`,`tab`,`allowed`);

--
-- Indexes for table `pma__users`
--
ALTER TABLE `pma__users`
  ADD PRIMARY KEY (`username`,`usergroup`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `pma__bookmark`
--
ALTER TABLE `pma__bookmark`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__column_info`
--
ALTER TABLE `pma__column_info`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__export_templates`
--
ALTER TABLE `pma__export_templates`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__history`
--
ALTER TABLE `pma__history`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__pdf_pages`
--
ALTER TABLE `pma__pdf_pages`
  MODIFY `page_nr` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pma__savedsearches`
--
ALTER TABLE `pma__savedsearches`
  MODIFY `id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- Database: `test`
--
CREATE DATABASE IF NOT EXISTS `test` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `test`;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
