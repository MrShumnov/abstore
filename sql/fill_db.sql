\c abstore;

\copy users(username, login, password, role, created, last_changed) FROM 'C:\Users\mrshu\reps\abstore\src\sql\data\users.csv' WITH (FORMAT csv);
\copy orders FROM 'C:\Users\mrshu\reps\abstore\src\sql\data\orders.csv' WITH (FORMAT csv);
\copy products(symbol, price, sale, description, created, last_changed) FROM 'C:\Users\mrshu\reps\abstore\src\sql\data\products.csv' WITH (FORMAT csv);
\copy orders_products FROM 'C:\Users\mrshu\reps\abstore\src\sql\data\orders_products.csv' WITH (FORMAT csv);