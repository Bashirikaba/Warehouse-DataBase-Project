--
-- PostgreSQL database cluster dump
--

-- Started on 2026-03-11 20:41:54

\restrict V4b4zBKelovDvf0dqzM9BrLyAf03Ai8THfRESxcZKWrqd1mEheGjeafrpkg72JA

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;

--
-- User Configurations
--








\unrestrict V4b4zBKelovDvf0dqzM9BrLyAf03Ai8THfRESxcZKWrqd1mEheGjeafrpkg72JA

--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

\restrict lQpzjXtz1VbP4gX0K0P5utEIrKlQV9z6zjIb5GxCoVy74lMpzXuaVxfpnPxlWMI

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.1

-- Started on 2026-03-11 20:41:54

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Completed on 2026-03-11 20:41:54

--
-- PostgreSQL database dump complete
--

\unrestrict lQpzjXtz1VbP4gX0K0P5utEIrKlQV9z6zjIb5GxCoVy74lMpzXuaVxfpnPxlWMI

--
-- Database "warehouse" dump
--

--
-- PostgreSQL database dump
--

\restrict 81UNTZ1Ma4bTcRPpQXW4V7ajNZfqW6ClbXRd9xteLlx07GQ7LAMjIeU0mGmUXFl

-- Dumped from database version 18.1
-- Dumped by pg_dump version 18.1

-- Started on 2026-03-11 20:41:54

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5083 (class 1262 OID 73903)
-- Name: warehouse; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE warehouse WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE warehouse OWNER TO postgres;

\unrestrict 81UNTZ1Ma4bTcRPpQXW4V7ajNZfqW6ClbXRd9xteLlx07GQ7LAMjIeU0mGmUXFl
\connect warehouse
\restrict 81UNTZ1Ma4bTcRPpQXW4V7ajNZfqW6ClbXRd9xteLlx07GQ7LAMjIeU0mGmUXFl

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 873 (class 1247 OID 73974)
-- Name: route_type; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.route_type AS ENUM (
    'Приход',
    'Расход'
);


ALTER TYPE public.route_type OWNER TO postgres;

--
-- TOC entry 240 (class 1255 OID 74098)
-- Name: create_balance(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.create_balance() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO balances (warehouse_id, goods_id, quantity)
    VALUES (NEW.warehouse_id, NEW.goods_id, 0)
    ON CONFLICT (warehouse_id, goods_id) DO NOTHING;
    
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.create_balance() OWNER TO postgres;

--
-- TOC entry 239 (class 1255 OID 74096)
-- Name: update_balances(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.update_balances() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF NEW.route = 'Приход' THEN
        INSERT INTO balances (warehouse_id, goods_id, quantity)
        VALUES (NEW.warehouse_id, NEW.goods_id, NEW.quantity)
        ON CONFLICT (warehouse_id, goods_id) 
        DO UPDATE SET quantity = balances.quantity + NEW.quantity;
    
    ELSE
        IF NOT EXISTS (
            SELECT 1 FROM balances 
            WHERE warehouse_id = NEW.warehouse_id 
            AND goods_id = NEW.goods_id 
            AND quantity >= NEW.quantity
        ) THEN
            RAISE EXCEPTION 'Не хватает товара на складе';
        END IF;
        
        UPDATE balances 
        SET quantity = quantity - NEW.quantity
        WHERE warehouse_id = NEW.warehouse_id AND goods_id = NEW.goods_id;
    END IF;
    
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.update_balances() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 228 (class 1259 OID 74044)
-- Name: balances; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.balances (
    id integer NOT NULL,
    warehouse_id integer NOT NULL,
    goods_id integer NOT NULL,
    quantity integer DEFAULT 0 NOT NULL,
    CONSTRAINT balances_quantity_check CHECK ((quantity >= 0))
);


ALTER TABLE public.balances OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 74031)
-- Name: goods; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.goods (
    id integer NOT NULL,
    code character varying(15) NOT NULL,
    nomenclature_number character varying(5) NOT NULL,
    name character varying(200) NOT NULL,
    price numeric(9,2) NOT NULL
);


ALTER TABLE public.goods OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 74010)
-- Name: warehouses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.warehouses (
    id integer NOT NULL,
    manager_id integer,
    name character varying(50) NOT NULL
);


ALTER TABLE public.warehouses OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 74104)
-- Name: balances_details; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.balances_details AS
 SELECT b.id,
    w.name AS warehouse_name,
    g.name AS goods_name,
    g.code AS goods_code,
    b.quantity
   FROM ((public.balances b
     JOIN public.warehouses w ON ((b.warehouse_id = w.id)))
     JOIN public.goods g ON ((b.goods_id = g.id)));


ALTER VIEW public.balances_details OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 74043)
-- Name: balances_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.balances ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.balances_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 225 (class 1259 OID 74030)
-- Name: goods_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.goods ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.goods_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 230 (class 1259 OID 74068)
-- Name: invoice; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.invoice (
    id integer NOT NULL,
    warehouse_id integer NOT NULL,
    goods_id integer NOT NULL,
    invoice_number character varying(5) NOT NULL,
    date date DEFAULT CURRENT_DATE NOT NULL,
    route public.route_type NOT NULL,
    quantity integer NOT NULL,
    cost numeric(11,2) NOT NULL,
    CONSTRAINT invoice_cost_check CHECK ((cost >= (0)::numeric)),
    CONSTRAINT invoice_quantity_check CHECK ((quantity > 0))
);


ALTER TABLE public.invoice OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 74117)
-- Name: goods_turnover; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.goods_turnover AS
 SELECT g.id,
    g.name AS goods_name,
    g.code,
    count(i.id) AS documents_count,
    sum(
        CASE
            WHEN (i.route = 'Приход'::public.route_type) THEN i.quantity
            ELSE 0
        END) AS total_received,
    sum(
        CASE
            WHEN (i.route = 'Расход'::public.route_type) THEN i.quantity
            ELSE 0
        END) AS total_sold,
    sum(
        CASE
            WHEN (i.route = 'Приход'::public.route_type) THEN (i.cost * (i.quantity)::numeric)
            ELSE (0)::numeric
        END) AS received_sum,
    sum(
        CASE
            WHEN (i.route = 'Расход'::public.route_type) THEN (i.cost * (i.quantity)::numeric)
            ELSE (0)::numeric
        END) AS sold_sum
   FROM (public.goods g
     LEFT JOIN public.invoice i ON ((g.id = i.goods_id)))
  GROUP BY g.id, g.name, g.code
 HAVING (count(i.id) > 0);


ALTER VIEW public.goods_turnover OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 74108)
-- Name: invoice_details; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.invoice_details AS
 SELECT i.id,
    i.invoice_number,
    i.date,
    i.route,
    w.name AS warehouse_name,
    g.name AS goods_name,
    g.code AS goods_code,
    i.quantity,
    i.cost,
    ((i.quantity)::numeric * i.cost) AS total_amount
   FROM ((public.invoice i
     JOIN public.warehouses w ON ((i.warehouse_id = w.id)))
     JOIN public.goods g ON ((i.goods_id = g.id)))
  ORDER BY i.date DESC;


ALTER VIEW public.invoice_details OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 74067)
-- Name: invoice_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.invoice ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.invoice_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 220 (class 1259 OID 73980)
-- Name: positions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.positions (
    id integer NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.positions OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 73979)
-- Name: positions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.positions ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.positions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 237 (class 1259 OID 74127)
-- Name: reorder_goods; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.reorder_goods AS
 SELECT g.name,
    g.code,
    b.warehouse_id,
    b.quantity,
    COALESCE(sum(
        CASE
            WHEN (i.route = 'Расход'::public.route_type) THEN i.quantity
            ELSE NULL::integer
        END), (0)::bigint) AS sold_last_30_days
   FROM ((public.goods g
     JOIN public.balances b ON ((g.id = b.goods_id)))
     LEFT JOIN public.invoice i ON (((g.id = i.goods_id) AND (i.date > (CURRENT_DATE - '30 days'::interval)))))
  GROUP BY g.id, g.name, g.code, b.warehouse_id, b.quantity
 HAVING (b.quantity < 10)
  ORDER BY b.quantity;


ALTER VIEW public.reorder_goods OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 73990)
-- Name: staff; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.staff (
    id integer NOT NULL,
    warehouse_id integer NOT NULL,
    position_id integer NOT NULL,
    full_name character varying(50) NOT NULL,
    tin character varying(12) NOT NULL
);


ALTER TABLE public.staff OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 73989)
-- Name: staff_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.staff ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.staff_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 231 (class 1259 OID 74100)
-- Name: staff_info; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.staff_info AS
 SELECT id,
    full_name,
    tin,
    warehouse_id
   FROM public.staff
  ORDER BY full_name;


ALTER VIEW public.staff_info OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 74132)
-- Name: staff_performance; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.staff_performance AS
 SELECT s.id,
    s.full_name,
    p.name AS "position",
    w.name AS warehouse,
    count(DISTINCT i.id) AS documents_processed,
    count(DISTINCT i.goods_id) AS unique_goods_handled,
    sum(i.quantity) AS total_items_processed,
    sum((i.cost * (i.quantity)::numeric)) AS total_value_processed,
    max(i.date) AS last_work_date
   FROM (((public.staff s
     JOIN public.warehouses w ON ((s.warehouse_id = w.id)))
     JOIN public.positions p ON ((s.position_id = p.id)))
     LEFT JOIN public.invoice i ON ((w.id = i.warehouse_id)))
  GROUP BY s.id, s.full_name, p.name, w.name
  ORDER BY (sum(i.quantity)) DESC;


ALTER VIEW public.staff_performance OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 74122)
-- Name: warehouse_report; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.warehouse_report AS
 SELECT w.name AS warehouse,
    i.route,
    count(DISTINCT i.id) AS invoices,
    count(DISTINCT i.goods_id) AS unique_goods,
    sum(i.quantity) AS total_quantity,
    sum((i.cost * (i.quantity)::numeric)) AS total_sum,
    min(i.date) AS first_date,
    max(i.date) AS last_date
   FROM (public.warehouses w
     JOIN public.invoice i ON ((w.id = i.warehouse_id)))
  GROUP BY w.name, i.route;


ALTER VIEW public.warehouse_report OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 74113)
-- Name: warehouse_totals; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.warehouse_totals AS
 SELECT w.name AS warehouse_name,
    count(b.goods_id) AS unique_goods_count,
    sum(b.quantity) AS total_items
   FROM (public.warehouses w
     LEFT JOIN public.balances b ON ((w.id = b.warehouse_id)))
  GROUP BY w.name
 HAVING ((sum(b.quantity) > 0) OR (count(b.goods_id) > 0));


ALTER VIEW public.warehouse_totals OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 74009)
-- Name: warehouses_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.warehouses ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.warehouses_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 5075 (class 0 OID 74044)
-- Dependencies: 228
-- Data for Name: balances; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.balances (id, warehouse_id, goods_id, quantity) FROM stdin;
\.


--
-- TOC entry 5073 (class 0 OID 74031)
-- Dependencies: 226
-- Data for Name: goods; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.goods (id, code, nomenclature_number, name, price) FROM stdin;
\.


--
-- TOC entry 5077 (class 0 OID 74068)
-- Dependencies: 230
-- Data for Name: invoice; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.invoice (id, warehouse_id, goods_id, invoice_number, date, route, quantity, cost) FROM stdin;
\.


--
-- TOC entry 5067 (class 0 OID 73980)
-- Dependencies: 220
-- Data for Name: positions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.positions (id, name) FROM stdin;
\.


--
-- TOC entry 5069 (class 0 OID 73990)
-- Dependencies: 222
-- Data for Name: staff; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.staff (id, warehouse_id, position_id, full_name, tin) FROM stdin;
\.


--
-- TOC entry 5071 (class 0 OID 74010)
-- Dependencies: 224
-- Data for Name: warehouses; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.warehouses (id, manager_id, name) FROM stdin;
\.


--
-- TOC entry 5084 (class 0 OID 0)
-- Dependencies: 227
-- Name: balances_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.balances_id_seq', 1, false);


--
-- TOC entry 5085 (class 0 OID 0)
-- Dependencies: 225
-- Name: goods_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.goods_id_seq', 1, false);


--
-- TOC entry 5086 (class 0 OID 0)
-- Dependencies: 229
-- Name: invoice_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.invoice_id_seq', 1, false);


--
-- TOC entry 5087 (class 0 OID 0)
-- Dependencies: 219
-- Name: positions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.positions_id_seq', 5, true);


--
-- TOC entry 5088 (class 0 OID 0)
-- Dependencies: 221
-- Name: staff_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.staff_id_seq', 51, true);


--
-- TOC entry 5089 (class 0 OID 0)
-- Dependencies: 223
-- Name: warehouses_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.warehouses_id_seq', 146, true);


--
-- TOC entry 4895 (class 2606 OID 74054)
-- Name: balances balances_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.balances
    ADD CONSTRAINT balances_pkey PRIMARY KEY (id);


--
-- TOC entry 4891 (class 2606 OID 74042)
-- Name: goods goods_code_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.goods
    ADD CONSTRAINT goods_code_key UNIQUE (code);


--
-- TOC entry 4893 (class 2606 OID 74040)
-- Name: goods goods_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.goods
    ADD CONSTRAINT goods_pkey PRIMARY KEY (id);


--
-- TOC entry 4899 (class 2606 OID 74085)
-- Name: invoice invoice_invoice_number_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoice
    ADD CONSTRAINT invoice_invoice_number_key UNIQUE (invoice_number);


--
-- TOC entry 4901 (class 2606 OID 74083)
-- Name: invoice invoice_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoice
    ADD CONSTRAINT invoice_pkey PRIMARY KEY (id);


--
-- TOC entry 4877 (class 2606 OID 73988)
-- Name: positions positions_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.positions
    ADD CONSTRAINT positions_name_key UNIQUE (name);


--
-- TOC entry 4879 (class 2606 OID 73986)
-- Name: positions positions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.positions
    ADD CONSTRAINT positions_pkey PRIMARY KEY (id);


--
-- TOC entry 4881 (class 2606 OID 73999)
-- Name: staff staff_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT staff_pkey PRIMARY KEY (id);


--
-- TOC entry 4883 (class 2606 OID 74001)
-- Name: staff staff_tin_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT staff_tin_key UNIQUE (tin);


--
-- TOC entry 4885 (class 2606 OID 74003)
-- Name: staff staff_warehouse_id_tin_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT staff_warehouse_id_tin_key UNIQUE (warehouse_id, tin);


--
-- TOC entry 4897 (class 2606 OID 74056)
-- Name: balances unique_warehouse_goods; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.balances
    ADD CONSTRAINT unique_warehouse_goods UNIQUE (warehouse_id, goods_id);


--
-- TOC entry 4887 (class 2606 OID 74019)
-- Name: warehouses warehouses_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.warehouses
    ADD CONSTRAINT warehouses_name_key UNIQUE (name);


--
-- TOC entry 4889 (class 2606 OID 74017)
-- Name: warehouses warehouses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.warehouses
    ADD CONSTRAINT warehouses_pkey PRIMARY KEY (id);


--
-- TOC entry 4909 (class 2620 OID 74099)
-- Name: invoice trg_ensure_balance; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_ensure_balance BEFORE INSERT ON public.invoice FOR EACH ROW EXECUTE FUNCTION public.create_balance();


--
-- TOC entry 4910 (class 2620 OID 74097)
-- Name: invoice trg_invoice_balances; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_invoice_balances AFTER INSERT ON public.invoice FOR EACH ROW EXECUTE FUNCTION public.update_balances();


--
-- TOC entry 4905 (class 2606 OID 74062)
-- Name: balances fk_balances_goods; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.balances
    ADD CONSTRAINT fk_balances_goods FOREIGN KEY (goods_id) REFERENCES public.goods(id) ON DELETE RESTRICT;


--
-- TOC entry 4906 (class 2606 OID 74057)
-- Name: balances fk_balances_warehouse; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.balances
    ADD CONSTRAINT fk_balances_warehouse FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) ON DELETE CASCADE;


--
-- TOC entry 4907 (class 2606 OID 74091)
-- Name: invoice fk_invoice_goods; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoice
    ADD CONSTRAINT fk_invoice_goods FOREIGN KEY (goods_id) REFERENCES public.goods(id) ON DELETE RESTRICT;


--
-- TOC entry 4908 (class 2606 OID 74086)
-- Name: invoice fk_invoice_warehouse; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.invoice
    ADD CONSTRAINT fk_invoice_warehouse FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) ON DELETE RESTRICT;


--
-- TOC entry 4902 (class 2606 OID 74025)
-- Name: staff fk_staff_warehouse; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT fk_staff_warehouse FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) ON DELETE CASCADE;


--
-- TOC entry 4904 (class 2606 OID 82095)
-- Name: warehouses fk_warehouses_manager; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.warehouses
    ADD CONSTRAINT fk_warehouses_manager FOREIGN KEY (manager_id) REFERENCES public.staff(id) ON UPDATE SET NULL ON DELETE RESTRICT;


--
-- TOC entry 4903 (class 2606 OID 74004)
-- Name: staff staff_position_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.staff
    ADD CONSTRAINT staff_position_id_fkey FOREIGN KEY (position_id) REFERENCES public.positions(id) ON DELETE RESTRICT;


-- Completed on 2026-03-11 20:41:55

--
-- PostgreSQL database dump complete
--

\unrestrict 81UNTZ1Ma4bTcRPpQXW4V7ajNZfqW6ClbXRd9xteLlx07GQ7LAMjIeU0mGmUXFl

-- Completed on 2026-03-11 20:41:55

--
-- PostgreSQL database cluster dump complete
--

