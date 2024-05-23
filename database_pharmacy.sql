-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 23, 2024 at 02:10 AM
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
(22, 'Bromhexine HCl 8mg Tablet', '8', 'mg', 'Tablet', 'C', '10', 'Cair Kahak. Ambil bila perlu sahaja.'),
(23, 'Calcitriol 0.25 mcg Capsule', '0.25', 'mcg', 'Tablet', 'A/KK', '', ''),
(24, 'Calcium Carbonate 500 mg Tablet', '500', 'mg', 'Tablet', 'B', '', ''),
(25, 'Calcium Lactate 300 mg Tablet', '300', 'mg', 'Tablet', 'C', '', ''),
(26, 'Captopril 25 mg Tablet', '25', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(27, 'Carbimazole 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', 'Hyperthyroidism'),
(28, 'Cephalexin Monohydrate 250 mg Capsule', '250', 'mg', 'Tablet', 'B', '', 'Habiskan. Jumpa doktor jika ada alahan.'),
(29, 'Charcoal, Activated 250mg Tablet', '250', 'mg', 'Tablet', 'C', '', 'Keracunan Makanan'),
(30, 'Chlorpheniramine Maleate 4mg Tablet', '4', 'mg', 'Tablet', 'C', '10', 'Selsema/Tahan Gatal. Ambil bila perlu sahaja. Boleh menyebabkan mengantuk'),
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
(56, 'Hyoscine N-Butylbromide 10mg Tablet', '10', 'mg', 'Tablet', 'C', '10', 'Tahan sakit perut'),
(57, 'Ibuprofen 200 mg Tablet', '200', 'mg', 'Tablet', 'B', '10', 'Tahan sakit. Ambil ubat selepas makan.'),
(58, 'Isoniazid 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', ''),
(59, 'Isosorbide Dinitrate 10mg Tablet', '10', 'mg', 'Tablet', 'B', '', ''),
(60, 'Isosorbide-5-Mononitrate 60mg SR Tablet', '60', 'mg', 'Tablet', 'A/KK', '', ''),
(61, 'Labetalol HCl 100mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(62, 'Lamotrigine 100mg Tablet', '100', 'mg', 'Tablet', 'A', '', ''),
(63, 'Levetiracetam 500 mg Tablet', '500', 'mg', 'Tablet', 'A*', '', ''),
(64, 'Levodopa 200 mg, Benserazide 50 mg Tablet (MADOPAR)', '250', 'mg', 'Tablet', 'B', '', ''),
(65, 'Levothyroxine Sodium 100 mcg Tablet', '100', 'mcg', 'Tablet', 'B', '', ''),
(66, 'Loratadine 10mg Tablet', '10', 'mg', 'Tablet', 'B', '10', 'Selsema / Tahan gatal'),
(67, 'Losartan 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(68, 'Losartan 50mg Tablet', '50', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(69, 'Magnesium Trisilicate Tablet', '1', 'tablet', 'Tablet', 'C', '10', 'Gastrik. Kunyah sebelum telan.'),
(70, 'Mefenamic Acid 250mg Capsule', '250', 'mg', 'Tablet', 'C', '10', 'Tahan sakit. Ambil ubat selepas makan.'),
(71, 'Metformin HCl 500 mg Extended Release Tablet', '500', 'mg', 'Tablet', 'B', '', 'Kencing manis. Ambil ubat selepas makan malam.'),
(72, 'Metformin HCl 500 mg Tablet', '500', 'mg', 'Tablet', 'B', '', 'Kencing manis. Ambil ubat selepas makan.'),
(73, 'Methyldopa 250 mg Tablet', '250', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(74, 'Metoclopramide HCl 10mg Tablet', '10', 'mg', 'Tablet', 'B', '10', 'Tahan loya/muntah. Ambil bila perlu sahaja'),
(75, 'Metoprolol Tartrate 100 mg Tablet', '100', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(76, 'Metoprolol Tartrate 50 mg Tablet', '50', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(77, 'Metronidazole 200mg Tablet', '200', 'mg', 'Tablet', 'B', '', ''),
(78, 'Multivitamin Tablet', '1', 'tablet', 'Tablet', 'B', '', ''),
(79, 'Nifedipine 10 mg Tablet', '10', 'mg', 'Tablet', 'B', '', 'Tekanan Darah Tinggi'),
(80, 'Olanzapine 5 mg Tablet', '5', 'mg', 'Tablet', 'B', '', ''),
(81, 'Olanzapine 10 mg Tablet', '10', 'mg', 'Tablet', 'B', '', ''),
(82, 'Omeprazole 20 mg Capsule', '20', 'mg', 'Tablet', 'A/KK', '', 'Gastrik. Ambil ubat 1 jam sebelum atau 2 jam selepas makan.'),
(83, 'Oseltamivir 75mg capsule', '75', 'mg', 'Tablet', 'A/KK', '', ''),
(84, 'Paracetamol 500 mg Tablet', '500', 'mg', 'Tablet', 'C', '10', 'Tahan sakit/Demam. Ambil bila perlu sahaja.'),
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
(96, 'Prochlorperazine Maleate 5mg Tablet', '5', 'mg', 'Tablet', 'B', '10', 'Tahan pening. Ambil bila perlu sahaja.'),
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
(130, 'Oral Rehydration Salt (ORS)', '1', 'sachet', 'Internal', 'C', '3', 'Bancuh 1 paket dalam 1 gelas air. Minum selepas cirit/muntah.'),
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
  MODIFY `ID` int(255) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `prescribeddrugshistory`
--
ALTER TABLE `prescribeddrugshistory`
  MODIFY `ID` int(255) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `records`
--
ALTER TABLE `records`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
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
('root', '[{\"db\":\"database_pharmacy\",\"table\":\"prescribeddrugs\"},{\"db\":\"database_pharmacy\",\"table\":\"records\"},{\"db\":\"database_pharmacy\",\"table\":\"drugtable\"},{\"db\":\"database_pharmacy\",\"table\":\"prescribeddrugshistory\"}]');

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
('root', '2024-05-23 00:10:06', '{\"Console\\/Mode\":\"collapse\"}');

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
