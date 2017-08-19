--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.3

-- Started on 2017-08-12 18:10:00

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

SET search_path = public, pg_catalog;

--
-- TOC entry 2270 (class 0 OID 24883)
-- Dependencies: 202
-- Data for Name: Vrsta_Partnera; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Vrsta_Partnera" VALUES (1, 'Dobavljač');
INSERT INTO "Vrsta_Partnera" VALUES (2, 'Pacijent');

--
-- TOC entry 2299 (class 0 OID 0)
-- Dependencies: 187
-- Name: StatusDokumenta_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"StatusDokumenta_ID_seq"', 4, true);

--
-- TOC entry 2256 (class 0 OID 16533)
-- Dependencies: 188
-- Data for Name: Status_Dokumenta; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Status_Dokumenta" VALUES (1, 'Zaprimljen');
INSERT INTO "Status_Dokumenta" VALUES (2, 'U obradi');
INSERT INTO "Status_Dokumenta" VALUES (3, 'Zatvoren');
INSERT INTO "Status_Dokumenta" VALUES (4, 'Otkazan');

--
-- TOC entry 2296 (class 0 OID 0)
-- Dependencies: 193
-- Name: JedinicaMjere_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"JedinicaMjere_ID_seq"', 3, true);


--
-- TOC entry 2262 (class 0 OID 16582)
-- Dependencies: 194
-- Data for Name: Jedinica_Mjere; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Jedinica_Mjere" VALUES (1, 'kom', 'komad');
INSERT INTO "Jedinica_Mjere" VALUES (2, 'kg', 'kilogram');
INSERT INTO "Jedinica_Mjere" VALUES (3, 'l', 'litra');

--
-- TOC entry 2301 (class 0 OID 0)
-- Dependencies: 189
-- Name: VrstaDokumenta_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"VrstaDokumenta_ID_seq"', 2, true);


--
-- TOC entry 2258 (class 0 OID 16564)
-- Dependencies: 190
-- Data for Name: Vrsta_Dokumenta; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Vrsta_Dokumenta" VALUES (2, 'Primka');
INSERT INTO "Vrsta_Dokumenta" VALUES (1, 'Narudžbenica');
INSERT INTO "Vrsta_Dokumenta" VALUES (3, 'Izdatnica');


--
-- TOC entry 2295 (class 0 OID 0)
-- Dependencies: 197
-- Name: Dokument_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"Dokument_ID_seq"', 18, true);

--
-- TOC entry 2274 (class 0 OID 24905)
-- Dependencies: 206
-- Data for Name: Kontakt; Type: TABLE DATA; Schema: public; Owner: marko
--

INSERT INTO "Kontakt" VALUES (1, 'm@m.com', 'Čazma', 'Domladovac, Marko', '09328432');
INSERT INTO "Kontakt" VALUES (2, 'mka@g.com', 'Čazma', 'Kapustić, Marko', '09328432');
INSERT INTO "Kontakt" VALUES (5, 'mka@g.com', 'Čazma', 'Test1', '09328432');
INSERT INTO "Kontakt" VALUES (7, 'mka@g.com', 'Čazma', 'Test 2', '09328432');
INSERT INTO "Kontakt" VALUES (8, 'mka@g.com', 'Čazma', 'Test 3', NULL);


--
-- TOC entry 2272 (class 0 OID 24891)
-- Dependencies: 204
-- Data for Name: Partner; Type: TABLE DATA; Schema: public; Owner: marko
--

INSERT INTO "Partner" VALUES (1, 'Test 1', '00:00:00', 1);
INSERT INTO "Partner" VALUES (2, 'Test 2', '00:00:00', 2);


--
-- TOC entry 2260 (class 0 OID 16573)
-- Dependencies: 192
-- Data for Name: Proizvod; Type: TABLE DATA; Schema: public; Owner: marko
--

INSERT INTO "Proizvod" VALUES (2, 'Proizvod 2', 'Opis proizvoda 2', 12.1099997, 12);
INSERT INTO "Proizvod" VALUES (1, 'Proizvod 1', 'Opis proizvoda 1', 2.33999991, 24);
INSERT INTO "Proizvod" VALUES (4, 'Proizvod 4', 'Opis proizvoda 4', 27.1700001, 36);
INSERT INTO "Proizvod" VALUES (3, 'Proizvod 3', 'Opis proizvoda 3', 6.94999981, 12);


--
-- TOC entry 2297 (class 0 OID 0)
-- Dependencies: 191
-- Name: Proizvod_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: marko
--

SELECT pg_catalog.setval('"Proizvod_ID_seq"', 4, true);


--
-- TOC entry 2298 (class 0 OID 0)
-- Dependencies: 195
-- Name: StanjeProizvoda_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: marko
--

SELECT pg_catalog.setval('"StanjeProizvoda_ID_seq"', 7, true);


--
-- TOC entry 2254 (class 0 OID 16523)
-- Dependencies: 186
-- Data for Name: Zaposlenik; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Zaposlenik" VALUES (1, 'Domladovac', 'Marko', '1989-07-23', 1);
INSERT INTO "Zaposlenik" VALUES (2, 'Kapustić', 'Mirko', '1991-11-13', 2);


--
-- TOC entry 2302 (class 0 OID 0)
-- Dependencies: 185
-- Name: Zaposlenik_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"Zaposlenik_ID_seq"', 2, true);


--
-- TOC entry 2303 (class 0 OID 0)
-- Dependencies: 203
-- Name: dobavljac_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"dobavljac_ID_seq"', 2, true);


--
-- TOC entry 2304 (class 0 OID 0)
-- Dependencies: 205
-- Name: kontakt_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"kontakt_ID_seq"', 8, true);


--
-- TOC entry 2305 (class 0 OID 0)
-- Dependencies: 201
-- Name: vrsta_dobavljaca_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"vrsta_dobavljaca_ID_seq"', 2, true);

--
-- TOC entry 2275 (class 0 OID 24914)
-- Dependencies: 207
-- Data for Name: Partner_Kontakt; Type: TABLE DATA; Schema: public; Owner: marko
--

INSERT INTO "Partner_Kontakt" VALUES (1, 5);
INSERT INTO "Partner_Kontakt" VALUES (1, 7);
INSERT INTO "Partner_Kontakt" VALUES (1, 8);



--
-- TOC entry 2266 (class 0 OID 16628)
-- Dependencies: 198
-- Data for Name: Dokument; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Dokument" VALUES (1, '2015-07-23 10:08:22', '2017-07-23 10:08:22', 1, 3, 2, 2017);
INSERT INTO "Dokument" VALUES (2, '2017-07-23 10:16:19.46', '2018-07-23 10:16:19.46', 1, 2, 3, 2018);
INSERT INTO "Dokument" VALUES (3, '2017-08-12 09:13:27.55631', '2017-08-12 09:13:27.55631', 2, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (6, '0001-01-01 00:00:00', '2017-08-12 10:28:54.691051', 1, 3, 2, 2017);
INSERT INTO "Dokument" VALUES (7, '2017-08-12 13:22:25.495179', '2017-08-12 13:22:25.495179', 1, 1, 1, 2017);
INSERT INTO "Dokument" VALUES (8, '0001-01-01 00:00:00', '2017-08-12 14:05:56.762264', 1, 3, 1, 2017);
INSERT INTO "Dokument" VALUES (9, '2017-08-12 14:06:01.014267', '2017-08-12 14:06:01.014267', 1, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (10, '0001-01-01 00:00:00', '2017-08-12 14:11:19.715093', 1, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (11, '0001-01-01 00:00:00', '2017-08-12 17:41:54.577245', 1, 3, 1, 2017);
INSERT INTO "Dokument" VALUES (12, '0001-01-01 00:00:00', '2017-08-12 17:42:02.991727', 1, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (13, '0001-01-01 00:00:00', '2017-08-12 17:43:27.660513', 1, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (14, '2017-08-12 17:53:55.264918', '2017-08-12 17:53:55.268418', 1, 1, 1, 2017);
INSERT INTO "Dokument" VALUES (15, '2017-08-12 17:55:40.532508', '2017-08-12 17:55:40.532508', 1, 1, 1, 2017);
INSERT INTO "Dokument" VALUES (16, '2017-08-12 17:56:06.482292', '2017-08-12 17:56:06.482292', 1, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (17, '2017-08-12 17:56:38.88679', '2017-08-12 17:56:38.88679', 1, 1, 2, 2017);
INSERT INTO "Dokument" VALUES (18, '0001-01-01 00:00:00', '2017-08-12 17:57:20.281862', 1, 1, 3, 2017);


--
-- TOC entry 2300 (class 0 OID 0)
-- Dependencies: 199
-- Name: StavkaDokumenta_ID_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"StavkaDokumenta_ID_seq"', 40, true);


--
-- TOC entry 2268 (class 0 OID 16658)
-- Dependencies: 200
-- Data for Name: Stavka_Dokumenta; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO "Stavka_Dokumenta" VALUES (2, 2, 1, 500);
INSERT INTO "Stavka_Dokumenta" VALUES (3, 1, 4, 100);
INSERT INTO "Stavka_Dokumenta" VALUES (4, 1, 2, 100);
INSERT INTO "Stavka_Dokumenta" VALUES (6, 3, 2, 1000);
INSERT INTO "Stavka_Dokumenta" VALUES (7, 3, 3, 1000);
INSERT INTO "Stavka_Dokumenta" VALUES (8, 3, 4, 1000);
INSERT INTO "Stavka_Dokumenta" VALUES (9, 3, 1, 1000);
INSERT INTO "Stavka_Dokumenta" VALUES (22, 8, 2, 98900);
INSERT INTO "Stavka_Dokumenta" VALUES (23, 8, 1, 8500);
INSERT INTO "Stavka_Dokumenta" VALUES (24, 8, 4, 23900);
INSERT INTO "Stavka_Dokumenta" VALUES (25, 8, 3, 54000);
INSERT INTO "Stavka_Dokumenta" VALUES (27, 10, 2, 98900);
INSERT INTO "Stavka_Dokumenta" VALUES (28, 10, 1, 8500);
INSERT INTO "Stavka_Dokumenta" VALUES (29, 10, 4, 23900);
INSERT INTO "Stavka_Dokumenta" VALUES (31, 11, 3, 54000);
INSERT INTO "Stavka_Dokumenta" VALUES (35, 13, 3, 50000);
INSERT INTO "Stavka_Dokumenta" VALUES (36, 14, 3, 4000);
INSERT INTO "Stavka_Dokumenta" VALUES (37, 15, 3, 4000);
INSERT INTO "Stavka_Dokumenta" VALUES (38, 16, 3, 4000);
INSERT INTO "Stavka_Dokumenta" VALUES (40, 18, 3, 10000);


--
-- TOC entry 2264 (class 0 OID 16601)
-- Dependencies: 196
-- Data for Name: Stanje_Proizvoda; Type: TABLE DATA; Schema: public; Owner: marko
--

INSERT INTO "Stanje_Proizvoda" VALUES (7, 55000, 1000, 45000, 3, 3);
INSERT INTO "Stanje_Proizvoda" VALUES (4, 100000, 1000, 100000, 2, 2);
INSERT INTO "Stanje_Proizvoda" VALUES (5, 10000, 1000, 10000, 1, 1);
INSERT INTO "Stanje_Proizvoda" VALUES (6, 25000, 1000, 25000, 2, 4);


-- Completed on 2017-08-12 18:10:00

--
-- PostgreSQL database dump complete
--

