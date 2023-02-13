COPY users(username, login, password, role, created, last_changed) FROM '/var/db/data/users.csv' WITH (FORMAT csv);
COPY orders FROM '/var/db/data/orders.csv' WITH (FORMAT csv);
COPY products(symbol, price, sale, description, created, last_changed) FROM '/var/db/data/products.csv' WITH (FORMAT csv);
COPY orders_products FROM '/var/db/data/orders_products.csv' WITH (FORMAT csv);

