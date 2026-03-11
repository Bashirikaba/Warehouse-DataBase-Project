CREATE TYPE route_type AS ENUM ('Приход', 'Расход');

CREATE TABLE positions (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE warehouses (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    manager_id INT,
    name VARCHAR(50) UNIQUE NOT NULL,
    
    CONSTRAINT fk_warehouses_manager 
        FOREIGN KEY (manager_id) 
        REFERENCES staff(id) 
        ON DELETE SET NULL
);

CREATE TABLE staff (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    warehouse_id INT NOT NULL REFERENCES warehouses(id) ON DELETE CASCADE,
    position_id INT NOT NULL REFERENCES positions(id) ON DELETE RESTRICT,
    full_name VARCHAR(50) NOT NULL,
    tin VARCHAR(12) UNIQUE NOT NULL,
    UNIQUE (warehouse_id, tin)
);

CREATE TABLE goods (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    code VARCHAR(15) UNIQUE NOT NULL,
    nomenclature_number VARCHAR(5) NOT NULL,
    name VARCHAR(200) NOT NULL,
    price NUMERIC(9, 2) NOT NULL
);

CREATE TABLE balances (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    warehouse_id INT NOT NULL,
    goods_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 0 CHECK (quantity >= 0),
    
    CONSTRAINT fk_balances_warehouse 
        FOREIGN KEY (warehouse_id) 
        REFERENCES warehouses(id) 
        ON DELETE cascade, 
    
    CONSTRAINT fk_balances_goods 
        FOREIGN KEY (goods_id) 
        REFERENCES goods(id) 
        ON DELETE restrict,
    
    CONSTRAINT unique_warehouse_goods UNIQUE (warehouse_id, goods_id)
);

CREATE TABLE invoice (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    warehouse_id INT NOT NULL,
    goods_id INT NOT NULL,
    invoice_number VARCHAR(5) UNIQUE NOT NULL,
    date DATE NOT NULL DEFAULT CURRENT_DATE,
    route route_type NOT NULL,
    quantity INT NOT NULL CHECK (quantity > 0),
    cost NUMERIC(11, 2) NOT NULL CHECK (cost >= 0),
    
    CONSTRAINT fk_invoice_warehouse 
        FOREIGN KEY (warehouse_id) 
        REFERENCES warehouses(id) 
        ON DELETE restrict,
    
    CONSTRAINT fk_invoice_goods 
        FOREIGN KEY (goods_id) 
        REFERENCES goods(id) 
        ON DELETE RESTRICT
);

/*
 * пока не заполнил данными
CREATE INDEX idx_staff_warehouse ON staff(warehouse_id);
CREATE INDEX idx_staff_position ON staff(position_id);
CREATE INDEX idx_balances_warehouse ON balances(warehouse_id);
CREATE INDEX idx_balances_goods ON balances(goods_id);
CREATE INDEX idx_invoice_warehouse ON invoice(warehouse_id);
CREATE INDEX idx_invoice_goods ON invoice(goods_id);
CREATE INDEX idx_invoice_date ON invoice(date);
*/

CREATE VIEW staff_info AS
SELECT 
    id,
    full_name,
    tin,
    warehouse_id
FROM staff
ORDER BY full_name;

CREATE VIEW balances_details AS
SELECT 
    b.id,
    w.name AS warehouse_name,
    g.name AS goods_name,
    g.code AS goods_code,
    b.quantity
FROM balances b
JOIN warehouses w ON b.warehouse_id = w.id
JOIN goods g ON b.goods_id = g.id;

CREATE VIEW invoice_details AS
SELECT 
    i.id,
    i.invoice_number,
    i.date,
    i.route,
    w.name AS warehouse_name,
    g.name AS goods_name,
    g.code AS goods_code,
    i.quantity,
    i.cost,
    (i.quantity * i.cost) AS total_amount
FROM invoice i
JOIN warehouses w ON i.warehouse_id = w.id
JOIN goods g ON i.goods_id = g.id
ORDER BY i.date DESC;

CREATE VIEW warehouse_totals AS
SELECT 
    w.name AS warehouse_name,
    COUNT(b.goods_id) AS unique_goods_count,
    SUM(b.quantity) AS total_items
FROM warehouses w
LEFT JOIN balances b ON w.id = b.warehouse_id
GROUP BY w.name
HAVING SUM(b.quantity) > 0 OR COUNT(b.goods_id) > 0;

CREATE VIEW goods_turnover AS
SELECT 
    g.id,
    g.name AS goods_name,
    g.code,
    COUNT(i.id) AS documents_count,
    SUM(CASE WHEN i.route = 'Приход' THEN i.quantity ELSE 0 END) AS total_received,
    SUM(CASE WHEN i.route = 'Расход' THEN i.quantity ELSE 0 END) AS total_sold,
    SUM(CASE WHEN i.route = 'Приход' THEN i.cost * i.quantity ELSE 0 END) AS received_sum,
    SUM(CASE WHEN i.route = 'Расход' THEN i.cost * i.quantity ELSE 0 END) AS sold_sum
FROM goods g
LEFT JOIN invoice i ON g.id = i.goods_id
GROUP BY g.id, g.name, g.code
HAVING COUNT(i.id) > 0;

-- отчет по складу за период
CREATE VIEW warehouse_report AS
SELECT 
    w.name AS warehouse,
    i.route,
    COUNT(DISTINCT i.id) AS invoices,
    COUNT(DISTINCT i.goods_id) AS unique_goods,
    SUM(i.quantity) AS total_quantity,
    SUM(i.cost * i.quantity) AS total_sum,
    MIN(i.date) AS first_date,
    MAX(i.date) AS last_date
FROM warehouses w
JOIN invoice i ON w.id = i.warehouse_id
GROUP BY w.name, i.route;

-- товары, которые скоро закончатся 
CREATE VIEW reorder_goods AS
SELECT 
    g.name,
    g.code,
    b.warehouse_id,
    b.quantity,
    COALESCE(SUM(CASE WHEN i.route = 'Расход' THEN i.quantity END), 0) AS sold_last_30_days
FROM goods g
JOIN balances b ON g.id = b.goods_id
LEFT JOIN invoice i ON g.id = i.goods_id 
    AND i.date > CURRENT_DATE - INTERVAL '30 days'
GROUP BY g.id, g.name, g.code, b.warehouse_id, b.quantity
HAVING b.quantity < 10
ORDER BY b.quantity;

-- эффективность сотрудников
CREATE VIEW staff_performance AS
SELECT 
    s.id,
    s.full_name,
    p.name AS position,
    w.name AS warehouse,
    COUNT(DISTINCT i.id) AS documents_processed,
    COUNT(DISTINCT i.goods_id) AS unique_goods_handled,
    SUM(i.quantity) AS total_items_processed,
    SUM(i.cost * i.quantity) AS total_value_processed,
    MAX(i.date) AS last_work_date
FROM staff s
JOIN warehouses w ON s.warehouse_id = w.id
JOIN positions p ON s.position_id = p.id
LEFT JOIN invoice i ON w.id = i.warehouse_id
GROUP BY s.id, s.full_name, p.name, w.name
ORDER BY total_items_processed DESC;

CREATE OR REPLACE FUNCTION update_balances()
RETURNS TRIGGER AS $$
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
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_invoice_balances
    AFTER INSERT ON invoice
    FOR EACH ROW
    EXECUTE FUNCTION update_balances();

CREATE OR REPLACE FUNCTION create_balance()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO balances (warehouse_id, goods_id, quantity)
    VALUES (NEW.warehouse_id, NEW.goods_id, 0)
    ON CONFLICT (warehouse_id, goods_id) DO NOTHING;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_ensure_balance
    BEFORE INSERT ON invoice
    FOR EACH ROW
    EXECUTE FUNCTION create_balance();