--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.3

-- Started on 2017-08-12 18:16:41

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2257 (class 1262 OID 16520)
-- Name: tbpfoi; Type: DATABASE; Schema: -; Owner: marko
--

--CREATE DATABASE tbpfoi WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Croatian_Croatia.1250' LC_CTYPE = 'Croatian_Croatia.1250';

--connect tbpfoi

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 1 (class 3079 OID 12387)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2260 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- TOC entry 222 (class 1255 OID 33075)
-- Name: brisanje_stavki_stanje(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION brisanje_stavki_stanje() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
 
DECLARE
vd_id integer;
vd_naziv varchar(30);
kol integer;

BEGIN

SELECT "VrstaID" into vd_id FROM "Dokument" WHERE "ID" = OLD."DokumentID";
SELECT "Naziv" into vd_naziv FROM "Vrsta_Dokumenta" WHERE "ID" = vd_id;

IF (vd_naziv = 'Izdatnica') then kol = OLD."Kolicina";
ELSIF (vd_naziv = 'Primka') then kol = -1 * OLD."Kolicina";
ELSE kol = 0;
END IF;

UPDATE "Stanje_Proizvoda" SET "Stanje" = "Stanje" + kol WHERE "ProizvodID" = OLD."ProizvodID";

RETURN OLD;
END;

 $$;


ALTER FUNCTION public.brisanje_stavki_stanje() OWNER TO postgres;

--
-- TOC entry 221 (class 1255 OID 33072)
-- Name: dodavanje_stavki_stanje(); Type: FUNCTION; Schema: public; Owner: marko
--

CREATE FUNCTION dodavanje_stavki_stanje() RETURNS trigger
    LANGUAGE plpgsql
    AS $$DECLARE
vd_id integer;
vd_naziv varchar(30);
kol integer;

BEGIN

SELECT "VrstaID" into vd_id FROM "Dokument" WHERE "ID" = NEW."DokumentID";
SELECT "Naziv" into vd_naziv FROM "Vrsta_Dokumenta" WHERE "ID" = vd_id;

IF (vd_naziv = 'Primka') then kol = NEW."Kolicina";
ELSIF (vd_naziv = 'Izdatnica') then kol = -1 * NEW."Kolicina";
ELSE kol = 0;
END IF;

UPDATE "Stanje_Proizvoda" SET "Stanje" = "Stanje" + kol WHERE "ProizvodID" = NEW."ProizvodID";

RETURN NEW;
END;
$$;

--
-- TOC entry 208 (class 1255 OID 33062)
-- Name: izdvoji_godinu(); Type: FUNCTION; Schema: public; Owner: marko
--

CREATE FUNCTION izdvoji_godinu() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
    DECLARE 
    BEGIN 
        NEW."Godina" := EXTRACT(YEAR FROM NEW."DatumAzuriranja");
        RETURN NEW;
    END; 
    $$;


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 198 (class 1259 OID 16628)
-- Name: Dokument; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Dokument" (
    "ID" integer NOT NULL,
    "DatumKreiranja" timestamp without time zone DEFAULT now() NOT NULL,
    "DatumAzuriranja" timestamp without time zone DEFAULT now() NOT NULL,
    "ZaposlenikID" integer,
    "StatusID" integer,
    "VrstaID" integer,
    "Godina" smallint DEFAULT 2017 NOT NULL
);


ALTER TABLE "Dokument" OWNER TO postgres;

--
-- TOC entry 197 (class 1259 OID 16626)
-- Name: Dokument_ID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Dokument_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Dokument_ID_seq" OWNER TO postgres;

--
-- TOC entry 2261 (class 0 OID 0)
-- Dependencies: 197
-- Name: Dokument_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "Dokument_ID_seq" OWNED BY "Dokument"."ID";


--
-- TOC entry 194 (class 1259 OID 16582)
-- Name: Jedinica_Mjere; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Jedinica_Mjere" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL,
    "Opis" character varying(128) NOT NULL
);


ALTER TABLE "Jedinica_Mjere" OWNER TO postgres;

--
-- TOC entry 193 (class 1259 OID 16580)
-- Name: JedinicaMjere_ID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "JedinicaMjere_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "JedinicaMjere_ID_seq" OWNER TO postgres;

--
-- TOC entry 2262 (class 0 OID 0)
-- Dependencies: 193
-- Name: JedinicaMjere_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "JedinicaMjere_ID_seq" OWNED BY "Jedinica_Mjere"."ID";


--
-- TOC entry 206 (class 1259 OID 24905)
-- Name: Kontakt; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Kontakt" (
    "ID" integer NOT NULL,
    "Email" character varying(64),
    "Adresa" character varying(128),
    "Naziv" character varying(32) NOT NULL,
    "Telefon" character varying(15)
);


--
-- TOC entry 204 (class 1259 OID 24891)
-- Name: Partner; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Partner" (
    "ID" integer NOT NULL,
    "Naziv" character varying(64) NOT NULL,
    "DatumKreiranja" time without time zone NOT NULL,
    "VrstaID" integer
);



--
-- TOC entry 207 (class 1259 OID 24914)
-- Name: Partner_Kontakt; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Partner_Kontakt" (
    "PartnerID" integer NOT NULL,
    "KontaktID" integer NOT NULL
);


--
-- TOC entry 192 (class 1259 OID 16573)
-- Name: Proizvod; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Proizvod" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL,
    "Opis" character varying(128) NOT NULL,
    "Vrijednost" real NOT NULL,
    "RokTrajanja" smallint NOT NULL
);


--
-- TOC entry 191 (class 1259 OID 16571)
-- Name: Proizvod_ID_seq; Type: SEQUENCE; Schema: public; Owner: marko
--

CREATE SEQUENCE "Proizvod_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2263 (class 0 OID 0)
-- Dependencies: 191
-- Name: Proizvod_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: marko
--

ALTER SEQUENCE "Proizvod_ID_seq" OWNED BY "Proizvod"."ID";


--
-- TOC entry 196 (class 1259 OID 16601)
-- Name: Stanje_Proizvoda; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Stanje_Proizvoda" (
    "ID" integer NOT NULL,
    "MaksimalnaKolicina" double precision NOT NULL,
    "MinimalnaKolicina" double precision NOT NULL,
    "Stanje" double precision DEFAULT 0 NOT NULL,
    "JedinicaMjereID" integer,
    "ProizvodID" integer,
    CONSTRAINT "StanjeProizvoda_MaksimalnaKolicina_check" CHECK (("MaksimalnaKolicina" > (0)::double precision)),
    CONSTRAINT "StanjeProizvoda_MinimalnaKolicina_check" CHECK (("MinimalnaKolicina" >= (0)::double precision)),
    CONSTRAINT "StanjeProizvoda_Stanje_check" CHECK (("Stanje" >= (0)::double precision)),
    CONSTRAINT "StanjeProizvoda_check" CHECK (("MaksimalnaKolicina" > "MinimalnaKolicina")),
    CONSTRAINT "StanjeProizvoda_check1" CHECK (("MaksimalnaKolicina" >= "Stanje")),
    CONSTRAINT "StanjeProizvoda_check2" CHECK (("Stanje" >= "MinimalnaKolicina"))
);


--
-- TOC entry 195 (class 1259 OID 16599)
-- Name: StanjeProizvoda_ID_seq; Type: SEQUENCE; Schema: public; Owner: marko
--

CREATE SEQUENCE "StanjeProizvoda_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2264 (class 0 OID 0)
-- Dependencies: 195
-- Name: StanjeProizvoda_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: marko
--

ALTER SEQUENCE "StanjeProizvoda_ID_seq" OWNED BY "Stanje_Proizvoda"."ID";


--
-- TOC entry 188 (class 1259 OID 16533)
-- Name: Status_Dokumenta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Status_Dokumenta" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL
);


--
-- TOC entry 187 (class 1259 OID 16531)
-- Name: StatusDokumenta_ID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "StatusDokumenta_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2265 (class 0 OID 0)
-- Dependencies: 187
-- Name: StatusDokumenta_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "StatusDokumenta_ID_seq" OWNED BY "Status_Dokumenta"."ID";


--
-- TOC entry 200 (class 1259 OID 16658)
-- Name: Stavka_Dokumenta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Stavka_Dokumenta" (
    "ID" integer NOT NULL,
    "DokumentID" integer,
    "ProizvodID" integer,
    "Kolicina" double precision NOT NULL,
    CONSTRAINT "StavkaDokumenta_Kolicina_check" CHECK (("Kolicina" > (0)::double precision))
);


--
-- TOC entry 199 (class 1259 OID 16656)
-- Name: StavkaDokumenta_ID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "StavkaDokumenta_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2266 (class 0 OID 0)
-- Dependencies: 199
-- Name: StavkaDokumenta_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "StavkaDokumenta_ID_seq" OWNED BY "Stavka_Dokumenta"."ID";


--
-- TOC entry 190 (class 1259 OID 16564)
-- Name: Vrsta_Dokumenta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Vrsta_Dokumenta" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL
);


--
-- TOC entry 189 (class 1259 OID 16562)
-- Name: VrstaDokumenta_ID_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "VrstaDokumenta_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2267 (class 0 OID 0)
-- Dependencies: 189
-- Name: VrstaDokumenta_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "VrstaDokumenta_ID_seq" OWNED BY "Vrsta_Dokumenta"."ID";


--
-- TOC entry 202 (class 1259 OID 24883)
-- Name: Vrsta_Partnera; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Vrsta_Partnera" (
    "ID" integer NOT NULL,
    "Naziv" character varying(64) NOT NULL
);


--
-- TOC entry 186 (class 1259 OID 16523)
-- Name: Zaposlenik; Type: TABLE; Schema: public; Owner: marko
--

CREATE TABLE "Zaposlenik" (
    "ID" integer NOT NULL,
    "Prezime" character varying(32) NOT NULL,
    "Ime" character varying(32) NOT NULL,
    "DatumRodjenja" date,
    "KontaktID" integer,
    CONSTRAINT "CK_Rodjenje" CHECK (("DatumRodjenja" < now()))
);


--
-- TOC entry 185 (class 1259 OID 16521)
-- Name: Zaposlenik_ID_seq; Type: SEQUENCE; Schema: public; Owner: marko
--

CREATE SEQUENCE "Zaposlenik_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2268 (class 0 OID 0)
-- Dependencies: 185
-- Name: Zaposlenik_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: marko
--

ALTER SEQUENCE "Zaposlenik_ID_seq" OWNED BY "Zaposlenik"."ID";


--
-- TOC entry 203 (class 1259 OID 24889)
-- Name: dobavljac_ID_seq; Type: SEQUENCE; Schema: public; Owner: marko
--

CREATE SEQUENCE "dobavljac_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2269 (class 0 OID 0)
-- Dependencies: 203
-- Name: dobavljac_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: marko
--

ALTER SEQUENCE "dobavljac_ID_seq" OWNED BY "Partner"."ID";


--
-- TOC entry 205 (class 1259 OID 24903)
-- Name: kontakt_ID_seq; Type: SEQUENCE; Schema: public; Owner: marko
--

CREATE SEQUENCE "kontakt_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2270 (class 0 OID 0)
-- Dependencies: 205
-- Name: kontakt_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: marko
--

ALTER SEQUENCE "kontakt_ID_seq" OWNED BY "Kontakt"."ID";


--
-- TOC entry 201 (class 1259 OID 24881)
-- Name: vrsta_dobavljaca_ID_seq; Type: SEQUENCE; Schema: public; Owner: marko
--

CREATE SEQUENCE "vrsta_dobavljaca_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 2271 (class 0 OID 0)
-- Dependencies: 201
-- Name: vrsta_dobavljaca_ID_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: marko
--

ALTER SEQUENCE "vrsta_dobavljaca_ID_seq" OWNED BY "Vrsta_Partnera"."ID";


--
-- TOC entry 2082 (class 2604 OID 16631)
-- Name: Dokument ID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Dokument" ALTER COLUMN "ID" SET DEFAULT nextval('"Dokument_ID_seq"'::regclass);


--
-- TOC entry 2073 (class 2604 OID 16585)
-- Name: Jedinica_Mjere ID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Jedinica_Mjere" ALTER COLUMN "ID" SET DEFAULT nextval('"JedinicaMjere_ID_seq"'::regclass);


--
-- TOC entry 2090 (class 2604 OID 24908)
-- Name: Kontakt ID; Type: DEFAULT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Kontakt" ALTER COLUMN "ID" SET DEFAULT nextval('"kontakt_ID_seq"'::regclass);


--
-- TOC entry 2089 (class 2604 OID 24894)
-- Name: Partner ID; Type: DEFAULT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Partner" ALTER COLUMN "ID" SET DEFAULT nextval('"dobavljac_ID_seq"'::regclass);


--
-- TOC entry 2072 (class 2604 OID 16576)
-- Name: Proizvod ID; Type: DEFAULT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Proizvod" ALTER COLUMN "ID" SET DEFAULT nextval('"Proizvod_ID_seq"'::regclass);


--
-- TOC entry 2074 (class 2604 OID 16604)
-- Name: Stanje_Proizvoda ID; Type: DEFAULT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Stanje_Proizvoda" ALTER COLUMN "ID" SET DEFAULT nextval('"StanjeProizvoda_ID_seq"'::regclass);


--
-- TOC entry 2070 (class 2604 OID 16536)
-- Name: Status_Dokumenta ID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Status_Dokumenta" ALTER COLUMN "ID" SET DEFAULT nextval('"StatusDokumenta_ID_seq"'::regclass);


--
-- TOC entry 2086 (class 2604 OID 16661)
-- Name: Stavka_Dokumenta ID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Stavka_Dokumenta" ALTER COLUMN "ID" SET DEFAULT nextval('"StavkaDokumenta_ID_seq"'::regclass);


--
-- TOC entry 2071 (class 2604 OID 16567)
-- Name: Vrsta_Dokumenta ID; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Vrsta_Dokumenta" ALTER COLUMN "ID" SET DEFAULT nextval('"VrstaDokumenta_ID_seq"'::regclass);


--
-- TOC entry 2088 (class 2604 OID 24886)
-- Name: Vrsta_Partnera ID; Type: DEFAULT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Vrsta_Partnera" ALTER COLUMN "ID" SET DEFAULT nextval('"vrsta_dobavljaca_ID_seq"'::regclass);


--
-- TOC entry 2068 (class 2604 OID 16526)
-- Name: Zaposlenik ID; Type: DEFAULT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Zaposlenik" ALTER COLUMN "ID" SET DEFAULT nextval('"Zaposlenik_ID_seq"'::regclass);


--
-- TOC entry 2111 (class 2606 OID 16635)
-- Name: Dokument PK_Dokument; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "PK_Dokument" PRIMARY KEY ("ID");


--
-- TOC entry 2105 (class 2606 OID 16587)
-- Name: Jedinica_Mjere PK_JedinicaMjere; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Jedinica_Mjere"
    ADD CONSTRAINT "PK_JedinicaMjere" PRIMARY KEY ("ID");


--
-- TOC entry 2101 (class 2606 OID 16578)
-- Name: Proizvod PK_Proizvod; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Proizvod"
    ADD CONSTRAINT "PK_Proizvod" PRIMARY KEY ("ID");


--
-- TOC entry 2107 (class 2606 OID 16613)
-- Name: Stanje_Proizvoda PK_StanjeProizvoda; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "PK_StanjeProizvoda" PRIMARY KEY ("ID");


--
-- TOC entry 2095 (class 2606 OID 16538)
-- Name: Status_Dokumenta PK_StatusDokumenta; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Status_Dokumenta"
    ADD CONSTRAINT "PK_StatusDokumenta" PRIMARY KEY ("ID");


--
-- TOC entry 2113 (class 2606 OID 16664)
-- Name: Stavka_Dokumenta PK_StavkaDokumenta; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Stavka_Dokumenta"
    ADD CONSTRAINT "PK_StavkaDokumenta" PRIMARY KEY ("ID");


--
-- TOC entry 2098 (class 2606 OID 16569)
-- Name: Vrsta_Dokumenta PK_VrstaDokumenta; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Vrsta_Dokumenta"
    ADD CONSTRAINT "PK_VrstaDokumenta" PRIMARY KEY ("ID");


--
-- TOC entry 2092 (class 2606 OID 16529)
-- Name: Zaposlenik PK_Zaposlenik; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Zaposlenik"
    ADD CONSTRAINT "PK_Zaposlenik" PRIMARY KEY ("ID");


--
-- TOC entry 2121 (class 2606 OID 24918)
-- Name: Partner_Kontakt Partner_Kontakt_pkey; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Partner_Kontakt"
    ADD CONSTRAINT "Partner_Kontakt_pkey" PRIMARY KEY ("PartnerID", "KontaktID");


--
-- TOC entry 2109 (class 2606 OID 16615)
-- Name: Stanje_Proizvoda StanjeProizvoda_ProizvodID_key; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "StanjeProizvoda_ProizvodID_key" UNIQUE ("ProizvodID");


--
-- TOC entry 2117 (class 2606 OID 24896)
-- Name: Partner dobavljac_pkey; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Partner"
    ADD CONSTRAINT dobavljac_pkey PRIMARY KEY ("ID");


--
-- TOC entry 2119 (class 2606 OID 24910)
-- Name: Kontakt kontakt_pkey; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Kontakt"
    ADD CONSTRAINT kontakt_pkey PRIMARY KEY ("ID");


--
-- TOC entry 2115 (class 2606 OID 24888)
-- Name: Vrsta_Partnera vrsta_dobavljaca_pkey; Type: CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Vrsta_Partnera"
    ADD CONSTRAINT vrsta_dobavljaca_pkey PRIMARY KEY ("ID");


--
-- TOC entry 2103 (class 1259 OID 16588)
-- Name: JedinicaMjere_Naziv; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "JedinicaMjere_Naziv" ON "Jedinica_Mjere" USING btree ("Naziv");


--
-- TOC entry 2102 (class 1259 OID 16579)
-- Name: Proizvod_Naziv; Type: INDEX; Schema: public; Owner: marko
--

CREATE INDEX "Proizvod_Naziv" ON "Proizvod" USING btree ("Naziv");


--
-- TOC entry 2096 (class 1259 OID 16539)
-- Name: StatusDokumenta_Naziv; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "StatusDokumenta_Naziv" ON "Status_Dokumenta" USING btree ("Naziv");


--
-- TOC entry 2099 (class 1259 OID 16570)
-- Name: VrstaDokumenta_Naziv; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "VrstaDokumenta_Naziv" ON "Vrsta_Dokumenta" USING btree ("Naziv");


--
-- TOC entry 2093 (class 1259 OID 24902)
-- Name: Zaposlenik_Prezime; Type: INDEX; Schema: public; Owner: marko
--

CREATE INDEX "Zaposlenik_Prezime" ON "Zaposlenik" USING btree ("Prezime");


--
-- TOC entry 2135 (class 2620 OID 33077)
-- Name: Stavka_Dokumenta brisanje_stavki; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER brisanje_stavki AFTER DELETE ON "Stavka_Dokumenta" FOR EACH ROW EXECUTE PROCEDURE brisanje_stavki_stanje();


--
-- TOC entry 2134 (class 2620 OID 33073)
-- Name: Stavka_Dokumenta dodavanje_stavki; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER dodavanje_stavki AFTER INSERT OR UPDATE ON "Stavka_Dokumenta" FOR EACH ROW EXECUTE PROCEDURE dodavanje_stavki_stanje();


--
-- TOC entry 2133 (class 2620 OID 33064)
-- Name: Dokument umetni_godinu; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER umetni_godinu BEFORE INSERT OR UPDATE ON "Dokument" FOR EACH ROW EXECUTE PROCEDURE izdvoji_godinu();


--
-- TOC entry 2130 (class 2606 OID 24897)
-- Name: Partner Dobavljac_Vrsta; Type: FK CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Partner"
    ADD CONSTRAINT "Dobavljac_Vrsta" FOREIGN KEY ("VrstaID") REFERENCES "Vrsta_Partnera"("ID");


--
-- TOC entry 2126 (class 2606 OID 16641)
-- Name: Dokument Dokument_StatusID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "Dokument_StatusID_fkey" FOREIGN KEY ("StatusID") REFERENCES "Status_Dokumenta"("ID");


--
-- TOC entry 2127 (class 2606 OID 16646)
-- Name: Dokument Dokument_VrstaID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "Dokument_VrstaID_fkey" FOREIGN KEY ("VrstaID") REFERENCES "Vrsta_Dokumenta"("ID");


--
-- TOC entry 2125 (class 2606 OID 16636)
-- Name: Dokument Dokument_ZaposlenikID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "Dokument_ZaposlenikID_fkey" FOREIGN KEY ("ZaposlenikID") REFERENCES "Zaposlenik"("ID");


--
-- TOC entry 2132 (class 2606 OID 24924)
-- Name: Partner_Kontakt Kontakt; Type: FK CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Partner_Kontakt"
    ADD CONSTRAINT "Kontakt" FOREIGN KEY ("KontaktID") REFERENCES "Kontakt"("ID");


--
-- TOC entry 2131 (class 2606 OID 24919)
-- Name: Partner_Kontakt Partner; Type: FK CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Partner_Kontakt"
    ADD CONSTRAINT "Partner" FOREIGN KEY ("PartnerID") REFERENCES "Partner"("ID");


--
-- TOC entry 2123 (class 2606 OID 16616)
-- Name: Stanje_Proizvoda StanjeProizvoda_JedinicaMjereID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "StanjeProizvoda_JedinicaMjereID_fkey" FOREIGN KEY ("JedinicaMjereID") REFERENCES "Jedinica_Mjere"("ID");


--
-- TOC entry 2124 (class 2606 OID 16621)
-- Name: Stanje_Proizvoda StanjeProizvoda_ProizvodID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "StanjeProizvoda_ProizvodID_fkey" FOREIGN KEY ("ProizvodID") REFERENCES "Proizvod"("ID");


--
-- TOC entry 2128 (class 2606 OID 16665)
-- Name: Stavka_Dokumenta StavkaDokumenta_DokumentID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Stavka_Dokumenta"
    ADD CONSTRAINT "StavkaDokumenta_DokumentID_fkey" FOREIGN KEY ("DokumentID") REFERENCES "Dokument"("ID");


--
-- TOC entry 2129 (class 2606 OID 16670)
-- Name: Stavka_Dokumenta StavkaDokumenta_ProizvodID_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Stavka_Dokumenta"
    ADD CONSTRAINT "StavkaDokumenta_ProizvodID_fkey" FOREIGN KEY ("ProizvodID") REFERENCES "Proizvod"("ID");


--
-- TOC entry 2122 (class 2606 OID 24929)
-- Name: Zaposlenik Zaposlenik_Kontakt; Type: FK CONSTRAINT; Schema: public; Owner: marko
--

ALTER TABLE ONLY "Zaposlenik"
    ADD CONSTRAINT "Zaposlenik_Kontakt" FOREIGN KEY ("KontaktID") REFERENCES "Kontakt"("ID");


--
-- TOC entry 2259 (class 0 OID 0)
-- Dependencies: 3
-- Name: public; Type: ACL; Schema: -; Owner: marko
--

--REVOKE ALL ON SCHEMA public FROM PUBLIC;
--GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2017-08-12 18:16:42

--
-- PostgreSQL database dump complete
--

