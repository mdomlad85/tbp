CREATE TABLE "Dokument" (
    "ID" integer NOT NULL,
    "DatumKreiranja" timestamp without time zone DEFAULT now() NOT NULL,
    "DatumAzuriranja" timestamp without time zone DEFAULT now() NOT NULL,
    "ZaposlenikID" integer,
    "StatusID" integer,
    "VrstaID" integer,
    "Godina" smallint DEFAULT 2017 NOT NULL
);

CREATE SEQUENCE "Dokument_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Dokument_ID_seq" OWNED BY "Dokument"."ID";

CREATE TABLE "Jedinica_Mjere" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL,
    "Opis" character varying(128) NOT NULL
);


ALTER TABLE "Jedinica_Mjere" OWNER TO postgres;

CREATE SEQUENCE "JedinicaMjere_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "JedinicaMjere_ID_seq" OWNED BY "Jedinica_Mjere"."ID";

CREATE TABLE "Kontakt" (
    "ID" integer NOT NULL,
    "Email" character varying(64),
    "Adresa" character varying(128),
    "Naziv" character varying(32) NOT NULL,
    "Telefon" character varying(15)
);

CREATE TABLE "Partner" (
    "ID" integer NOT NULL,
    "Naziv" character varying(64) NOT NULL,
    "DatumKreiranja" time without time zone NOT NULL,
    "VrstaID" integer
);

CREATE TABLE "Partner_Kontakt" (
    "PartnerID" integer NOT NULL,
    "KontaktID" integer NOT NULL
);

CREATE TABLE "Proizvod" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL,
    "Opis" character varying(128) NOT NULL,
    "Vrijednost" real NOT NULL,
    "RokTrajanja" smallint NOT NULL
);

CREATE SEQUENCE "Proizvod_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Proizvod_ID_seq" OWNED BY "Proizvod"."ID";

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

CREATE SEQUENCE "StanjeProizvoda_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "StanjeProizvoda_ID_seq" OWNED BY "Stanje_Proizvoda"."ID";

CREATE TABLE "Status_Dokumenta" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL
);

CREATE SEQUENCE "StatusDokumenta_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "StatusDokumenta_ID_seq" OWNED BY "Status_Dokumenta"."ID";

CREATE TABLE "Stavka_Dokumenta" (
    "ID" integer NOT NULL,
    "DokumentID" integer,
    "ProizvodID" integer,
    "Kolicina" double precision NOT NULL,
    CONSTRAINT "StavkaDokumenta_Kolicina_check" CHECK (("Kolicina" > (0)::double precision))
);

CREATE SEQUENCE "StavkaDokumenta_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "StavkaDokumenta_ID_seq" OWNED BY "Stavka_Dokumenta"."ID";

CREATE TABLE "Vrsta_Dokumenta" (
    "ID" integer NOT NULL,
    "Naziv" character varying(20) NOT NULL
);

CREATE SEQUENCE "VrstaDokumenta_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "VrstaDokumenta_ID_seq" OWNED BY "Vrsta_Dokumenta"."ID";

CREATE TABLE "Vrsta_Partnera" (
    "ID" integer NOT NULL,
    "Naziv" character varying(64) NOT NULL
);

CREATE TABLE "Zaposlenik" (
    "ID" integer NOT NULL,
    "Prezime" character varying(32) NOT NULL,
    "Ime" character varying(32) NOT NULL,
    "DatumRodjenja" date,
    "KontaktID" integer,
    CONSTRAINT "CK_Rodjenje" CHECK (("DatumRodjenja" < now()))
);

CREATE SEQUENCE "Zaposlenik_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "Zaposlenik_ID_seq" OWNED BY "Zaposlenik"."ID";

CREATE SEQUENCE "dobavljac_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "dobavljac_ID_seq" OWNED BY "Partner"."ID";

CREATE SEQUENCE "kontakt_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "kontakt_ID_seq" OWNED BY "Kontakt"."ID";

CREATE SEQUENCE "vrsta_dobavljaca_ID_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE "vrsta_dobavljaca_ID_seq" OWNED BY "Vrsta_Partnera"."ID";

ALTER TABLE ONLY "Dokument" ALTER COLUMN "ID" SET DEFAULT nextval('"Dokument_ID_seq"'::regclass);

ALTER TABLE ONLY "Jedinica_Mjere" ALTER COLUMN "ID" SET DEFAULT nextval('"JedinicaMjere_ID_seq"'::regclass);

ALTER TABLE ONLY "Kontakt" ALTER COLUMN "ID" SET DEFAULT nextval('"kontakt_ID_seq"'::regclass);

ALTER TABLE ONLY "Partner" ALTER COLUMN "ID" SET DEFAULT nextval('"dobavljac_ID_seq"'::regclass);

ALTER TABLE ONLY "Proizvod" ALTER COLUMN "ID" SET DEFAULT nextval('"Proizvod_ID_seq"'::regclass);

ALTER TABLE ONLY "Stanje_Proizvoda" ALTER COLUMN "ID" SET DEFAULT nextval('"StanjeProizvoda_ID_seq"'::regclass);

ALTER TABLE ONLY "Status_Dokumenta" ALTER COLUMN "ID" SET DEFAULT nextval('"StatusDokumenta_ID_seq"'::regclass);

ALTER TABLE ONLY "Stavka_Dokumenta" ALTER COLUMN "ID" SET DEFAULT nextval('"StavkaDokumenta_ID_seq"'::regclass);

ALTER TABLE ONLY "Vrsta_Dokumenta" ALTER COLUMN "ID" SET DEFAULT nextval('"VrstaDokumenta_ID_seq"'::regclass);

ALTER TABLE ONLY "Vrsta_Partnera" ALTER COLUMN "ID" SET DEFAULT nextval('"vrsta_dobavljaca_ID_seq"'::regclass);

ALTER TABLE ONLY "Zaposlenik" ALTER COLUMN "ID" SET DEFAULT nextval('"Zaposlenik_ID_seq"'::regclass);

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "PK_Dokument" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Jedinica_Mjere"
    ADD CONSTRAINT "PK_JedinicaMjere" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Proizvod"
    ADD CONSTRAINT "PK_Proizvod" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "PK_StanjeProizvoda" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Status_Dokumenta"
    ADD CONSTRAINT "PK_StatusDokumenta" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Stavka_Dokumenta"
    ADD CONSTRAINT "PK_StavkaDokumenta" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Vrsta_Dokumenta"
    ADD CONSTRAINT "PK_VrstaDokumenta" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Zaposlenik"
    ADD CONSTRAINT "PK_Zaposlenik" PRIMARY KEY ("ID");

ALTER TABLE ONLY "Partner_Kontakt"
    ADD CONSTRAINT "Partner_Kontakt_pkey" PRIMARY KEY ("PartnerID", "KontaktID");

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "StanjeProizvoda_ProizvodID_key" UNIQUE ("ProizvodID");

ALTER TABLE ONLY "Partner"
    ADD CONSTRAINT dobavljac_pkey PRIMARY KEY ("ID");

ALTER TABLE ONLY "Kontakt"
    ADD CONSTRAINT kontakt_pkey PRIMARY KEY ("ID");

ALTER TABLE ONLY "Vrsta_Partnera"
    ADD CONSTRAINT vrsta_dobavljaca_pkey PRIMARY KEY ("ID");

CREATE INDEX "JedinicaMjere_Naziv" ON "Jedinica_Mjere" USING btree ("Naziv");

CREATE INDEX "Proizvod_Naziv" ON "Proizvod" USING btree ("Naziv");

CREATE INDEX "StatusDokumenta_Naziv" ON "Status_Dokumenta" USING btree ("Naziv");

CREATE INDEX "VrstaDokumenta_Naziv" ON "Vrsta_Dokumenta" USING btree ("Naziv");

CREATE INDEX "Zaposlenik_Prezime" ON "Zaposlenik" USING btree ("Prezime");

ALTER TABLE ONLY "Partner"
    ADD CONSTRAINT "Dobavljac_Vrsta" FOREIGN KEY ("VrstaID") REFERENCES "Vrsta_Partnera"("ID");

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "Dokument_StatusID_fkey" FOREIGN KEY ("StatusID") REFERENCES "Status_Dokumenta"("ID");

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "Dokument_VrstaID_fkey" FOREIGN KEY ("VrstaID") REFERENCES "Vrsta_Dokumenta"("ID");

ALTER TABLE ONLY "Dokument"
    ADD CONSTRAINT "Dokument_ZaposlenikID_fkey" FOREIGN KEY ("ZaposlenikID") REFERENCES "Zaposlenik"("ID");

ALTER TABLE ONLY "Partner_Kontakt"
    ADD CONSTRAINT "Kontakt" FOREIGN KEY ("KontaktID") REFERENCES "Kontakt"("ID");

ALTER TABLE ONLY "Partner_Kontakt"
    ADD CONSTRAINT "Partner" FOREIGN KEY ("PartnerID") REFERENCES "Partner"("ID");

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "StanjeProizvoda_JedinicaMjereID_fkey" FOREIGN KEY ("JedinicaMjereID") REFERENCES "Jedinica_Mjere"("ID");

ALTER TABLE ONLY "Stanje_Proizvoda"
    ADD CONSTRAINT "StanjeProizvoda_ProizvodID_fkey" FOREIGN KEY ("ProizvodID") REFERENCES "Proizvod"("ID");

ALTER TABLE ONLY "Stavka_Dokumenta"
    ADD CONSTRAINT "StavkaDokumenta_DokumentID_fkey" FOREIGN KEY ("DokumentID") REFERENCES "Dokument"("ID");

ALTER TABLE ONLY "Stavka_Dokumenta"
    ADD CONSTRAINT "StavkaDokumenta_ProizvodID_fkey" FOREIGN KEY ("ProizvodID") REFERENCES "Proizvod"("ID");

ALTER TABLE ONLY "Zaposlenik"
    ADD CONSTRAINT "Zaposlenik_Kontakt" FOREIGN KEY ("KontaktID") REFERENCES "Kontakt"("ID");

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

CREATE FUNCTION izdvoji_godinu() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
    DECLARE 
    BEGIN 
        NEW."Godina" := EXTRACT(YEAR FROM NEW."DatumAzuriranja");
        RETURN NEW;
    END; 
    $$;

CREATE TRIGGER brisanje_stavki AFTER DELETE ON "Stavka_Dokumenta" FOR EACH ROW EXECUTE PROCEDURE brisanje_stavki_stanje();

CREATE TRIGGER dodavanje_stavki AFTER INSERT OR UPDATE ON "Stavka_Dokumenta" FOR EACH ROW EXECUTE PROCEDURE dodavanje_stavki_stanje();

CREATE TRIGGER umetni_godinu BEFORE INSERT OR UPDATE ON "Dokument" FOR EACH ROW EXECUTE PROCEDURE izdvoji_godinu();

