\c abstore;

\copy users FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\users.csv' WITH (FORMAT csv);
\copy orders FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\orders.csv' WITH (FORMAT csv);
\copy products FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\products.csv' WITH (FORMAT csv);
\copy orders_products FROM 'C:\Users\mrshu\reps\abstore\src\fill_database\data\orders_products.csv' WITH (FORMAT csv);