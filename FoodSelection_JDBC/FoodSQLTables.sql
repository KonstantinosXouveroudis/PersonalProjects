CREATE DATABASE foodselection;

USE foodselection;

CREATE TABLE Food_Regular(
ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL
);

CREATE TABLE Food_Wed_Fri(
ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL
);

CREATE TABLE Food_Weekend(
ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL
);

CREATE TABLE Food_Winter(
ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL
);

CREATE TABLE Food_Summer(
ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL
);

CREATE TABLE Meat_Side_Dish(
ID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL
);

INSERT INTO Food_Regular(Food_Name) VALUES('Μακαρόνια με Κυμά');
INSERT INTO Food_Regular(Food_Name) VALUES('Μακαρόνια με Σάλτσα Τόνου');
INSERT INTO Food_Regular(Food_Name) VALUES('Μακαρόνια με Σάλτσα Ντομάτα');
INSERT INTO Food_Regular(Food_Name) VALUES('Μακαρόνια με Κρέμα Γάλακτος και Μανιτάρια');
INSERT INTO Food_Regular(Food_Name) VALUES('Μακαρόνια Φούρνου');
INSERT INTO Food_Regular(Food_Name) VALUES('Τορτίγιες με Τσίλι και Ρίζι');
INSERT INTO Food_Regular(Food_Name) VALUES('Αρακάς');
INSERT INTO Food_Regular(Food_Name) VALUES('Αρακάς με Μοσχαράκι');
INSERT INTO Food_Regular(Food_Name) VALUES('Ψαροκροκέτες με Ρύζι');
INSERT INTO Food_Regular(Food_Name) VALUES('Ψαροκροκέτες με Ρύζι και Καλαμπόκι');
INSERT INTO Food_Regular(Food_Name) VALUES('Ψαροκροκέτες με Τηγανιτές Πατάτες');
INSERT INTO Food_Regular(Food_Name) VALUES('Φιλέτα Μπακαλιάρου');
INSERT INTO Food_Regular(Food_Name) VALUES('Κοτομπουκιές με Ρύζι');
INSERT INTO Food_Regular(Food_Name) VALUES('Κοτομπουκιές με Ρύζι και Καλαμπόκι');
INSERT INTO Food_Regular(Food_Name) VALUES('Κοτομπουκιές με Τηγανιτές Πατάτες');

INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Αρακάς');
INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Σπανακόρυζο με Τυρί');
INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Μακαρόνια με Κρέμα Γάλακτος και Μανιτάρια');
INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Ψαροκροκέτες με Ρύζι');
INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Ψαροκροκέτες με Ρύζι και Καλαμπόκι');
INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Ψαροκροκέτες με Τηγανιτές Πατάτες');
INSERT INTO Food_Wed_Fri(Food_Name) VALUES('Φιλέτα Μπακαλιάρου');

INSERT INTO Food_Winter(Food_Name) VALUES('Φακές');
INSERT INTO Food_Winter(Food_Name) VALUES('Φασολάδα');

INSERT INTO Food_Summer(Food_Name) VALUES('Στραπατσάδα (Αυγά με ντομάτα και πιπερίες)');
INSERT INTO Food_Summer(Food_Name) VALUES('Τουρλού');

#Πήνακας με συνοδευτικά: Τηγανιτές Πατάτες, Πατάτες στον φούρνο, πουρές, ρύζι, ρύζι και μανιτάρια.
#Για: Κοτόπουλο, ψαρονέφρι, μπιροζόλες, σουβλάκια, πανσέτες, τυγανιά, μπιφτέκια, σνίτσελ, φιλέτα μπακαλιάρου
#Θα το κάνω έτσι μόνο με τα κρέατα, για να μειώσω τον αριθμό γραμμών από 9*5=45 σε 9+5=14.
INSERT INTO Food_Weekend(Food_Name) VALUES('Κοτόπουλο στον φούρνο');
INSERT INTO Food_Weekend(Food_Name) VALUES('Ψαρονέφρι');
INSERT INTO Food_Weekend(Food_Name) VALUES('Κριθαράκι με Μοσχαράκι');
INSERT INTO Food_Weekend(Food_Name) VALUES('Μπριζόλες');
INSERT INTO Food_Weekend(Food_Name) VALUES('Σουβλάκια');
INSERT INTO Food_Weekend(Food_Name) VALUES('Πανσέτες');
INSERT INTO Food_Weekend(Food_Name) VALUES('Τυγανιά');
INSERT INTO Food_Weekend(Food_Name) VALUES('Μπιφτέκια');
INSERT INTO Food_Weekend(Food_Name) VALUES('Σνίτσελ');
INSERT INTO Food_Weekend(Food_Name) VALUES('Hamburger');
INSERT INTO Food_Weekend(Food_Name) VALUES('Hot Dogs');

INSERT INTO Meat_Side_Dish(Food_Name) VALUES('Τηγανιές Πατάτες');
INSERT INTO Meat_Side_Dish(Food_Name) VALUES('Πατάτες στον φούρνο');
INSERT INTO Meat_Side_Dish(Food_Name) VALUES('Πουρές');
INSERT INTO Meat_Side_Dish(Food_Name) VALUES('Ρύζι');
INSERT INTO Meat_Side_Dish(Food_Name) VALUES('Ρύζι και Μανιτάρια');