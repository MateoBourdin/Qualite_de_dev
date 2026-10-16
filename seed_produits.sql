-- =========================================================
-- Seed : t_e_marque_pro / t_e_typeproduit_pro / t_e_produit_pro
-- PostgreSQL
-- =========================================================

START TRANSACTION;

-- Nettoyage (optionnel)
TRUNCATE TABLE t_e_produit_pro, t_e_marque_pro, t_e_typeproduit_pro RESTART IDENTITY CASCADE;

-- ---------------------------------------------------------
-- Marques
-- ---------------------------------------------------------
INSERT INTO t_e_marque_pro ("idMarque", "nomMarque") VALUES
(1, 'Apple'),
(2, 'Samsung'),
(3, 'Sony'),
(4, 'Logitech'),
(5, 'Dell'),
(6, 'Asus'),
(7, 'Bose'),
(8, 'Nintendo');

-- ---------------------------------------------------------
-- Types de produit
-- ---------------------------------------------------------
INSERT INTO t_e_typeproduit_pro ("idTypeProduit", "nomTypeProduit") VALUES
(1, 'Smartphone'),
(2, 'Ordinateur portable'),
(3, 'Casque audio'),
(4, 'Souris'),
(5, 'Console'),
(6, 'Écran');

-- ---------------------------------------------------------
-- Produits
-- Certains stocks sont volontairement sous StockMin / au-dessus de StockMax / NULL
-- ---------------------------------------------------------
INSERT INTO t_e_produit_pro
("idProduit", "nomProduit", description, "nomPhoto", "uriPhoto",
 "idMarque", "idTypeProduit", "StockReel", "StockMin", "StockMax")
VALUES
( 1, 'iPhone 15',            'Smartphone 6,1 pouces, puce A16',        'iphone15.jpg',       'https://img.exemple.fr/p/iphone15.jpg',       1, 1,  25, 10,  50),
( 2, 'Galaxy S24',           'Smartphone 6,2 pouces, 128 Go',          'galaxys24.jpg',      'https://img.exemple.fr/p/galaxys24.jpg',      2, 1,  40, 10,  60),
( 3, 'Xperia 1 V',           'Smartphone 4K OLED 6,5 pouces',          'xperia1v.jpg',       'https://img.exemple.fr/p/xperia1v.jpg',       3, 1,   4,  5,  30),
( 4, 'MacBook Air M3',       'Portable 13 pouces, 8 Go, 256 Go SSD',   'macbookairm3.jpg',   'https://img.exemple.fr/p/macbookairm3.jpg',   1, 2,  12,  5,  25),
( 5, 'XPS 13',               'Ultrabook 13,4 pouces Intel Core Ultra', 'xps13.jpg',          'https://img.exemple.fr/p/xps13.jpg',          5, 2,   8,  5,  20),
( 6, 'ZenBook 14',           'Portable OLED 14 pouces, 16 Go RAM',     'zenbook14.jpg',      'https://img.exemple.fr/p/zenbook14.jpg',      6, 2,   2,  4,  20),
( 7, 'ROG Strix G16',        'Portable gamer RTX 4070, 16 pouces',     'rogstrixg16.jpg',    'https://img.exemple.fr/p/rogstrixg16.jpg',    6, 2,  15,  3,  15),
( 8, 'WH-1000XM5',           'Casque sans fil a reduction de bruit',   'wh1000xm5.jpg',      'https://img.exemple.fr/p/wh1000xm5.jpg',      3, 3,  30, 10,  40),
( 9, 'QuietComfort Ultra',   'Casque Bluetooth audio spatial',         'qcultra.jpg',        'https://img.exemple.fr/p/qcultra.jpg',        7, 3,  18,  8,  35),
(10, 'AirPods Max',          'Casque circum-aural, puce H1',           'airpodsmax.jpg',     'https://img.exemple.fr/p/airpodsmax.jpg',     1, 3,   0,  5,  20),
(11, 'MX Master 3S',         'Souris sans fil ergonomique 8000 DPI',   'mxmaster3s.jpg',     'https://img.exemple.fr/p/mxmaster3s.jpg',     4, 4,  60, 20, 100),
(12, 'G502 X',               'Souris gaming filaire, 13 boutons',      'g502x.jpg',          'https://img.exemple.fr/p/g502x.jpg',          4, 4,  45, 15,  80),
(13, 'Magic Mouse',          'Souris multi-touch rechargeable',        'magicmouse.jpg',     'https://img.exemple.fr/p/magicmouse.jpg',     1, 4, 120, 10,  50),
(14, 'PlayStation 5',        'Console de salon, lecteur Blu-ray',      'ps5.jpg',            'https://img.exemple.fr/p/ps5.jpg',            3, 5,  22, 10,  40),
(15, 'Switch OLED',          'Console hybride, ecran OLED 7 pouces',   'switcholed.jpg',     'https://img.exemple.fr/p/switcholed.jpg',     8, 5,  35, 10,  50),
(16, 'Odyssey G7',           'Ecran incurve 27 pouces 240 Hz',         'odysseyg7.jpg',      'https://img.exemple.fr/p/odysseyg7.jpg',      2, 6,   9,  5,  20),
(17, 'UltraSharp U2723QE',   'Ecran 4K IPS 27 pouces, USB-C',          'u2723qe.jpg',        'https://img.exemple.fr/p/u2723qe.jpg',        5, 6,  14,  5,  25),
(18, 'ProArt PA278QV',       'Ecran graphiste 27 pouces WQHD',         'pa278qv.jpg',        'https://img.exemple.fr/p/pa278qv.jpg',        6, 6,  NULL, NULL, NULL),
(19, 'Galaxy Buds2 Pro',     'Ecouteurs sans fil ANC',                 'buds2pro.jpg',       'https://img.exemple.fr/p/buds2pro.jpg',       2, 3,  50, 15,  70),
(20, 'G Pro X Superlight',   'Souris gaming sans fil 63 g',            'gproxsuperlight.jpg','https://img.exemple.fr/p/gproxsuper.jpg',     4, 4,   7, 10,  40);

-- ---------------------------------------------------------
-- Recalage des séquences IDENTITY (sinon conflit au prochain INSERT via EF)
-- ---------------------------------------------------------
SELECT setval(pg_get_serial_sequence('t_e_marque_pro', 'idMarque'),
              (SELECT MAX("idMarque") FROM t_e_marque_pro));
SELECT setval(pg_get_serial_sequence('t_e_typeproduit_pro', 'idTypeProduit'),
              (SELECT MAX("idTypeProduit") FROM t_e_typeproduit_pro));
SELECT setval(pg_get_serial_sequence('t_e_produit_pro', 'idProduit'),
              (SELECT MAX("idProduit") FROM t_e_produit_pro));

COMMIT;
