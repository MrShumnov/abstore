CREATE USER abstore_rw WITH PASSWORD '1234';
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO abstore_rw;

CREATE USER abstore_r WITH PASSWORD '1234';
GRANT SELECT ON ALL TABLES IN SCHEMA public TO abstore_r;