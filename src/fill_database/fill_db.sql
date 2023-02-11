\c abstore;

\copy users(username, login, password, role, created, last_changed) FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\users.csv' WITH (FORMAT csv);
\copy orders FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\orders.csv' WITH (FORMAT csv);
\copy products(symbol, price, sale, description, created, last_changed) FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\products.csv' WITH (FORMAT csv);
\copy orders_products FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\orders_products.csv' WITH (FORMAT csv);