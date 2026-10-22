\set ON_ERROR_STOP on

BEGIN;

INSERT INTO users (name, email)
SELECT 'User ' || n, 'user' || n || '@example.com'
FROM generate_series(1, 100) AS g(n);

INSERT INTO orders (user_id, total)
SELECT u.id, (10 + (n % 200))::numeric
FROM users AS u
CROSS JOIN generate_series(1, 10) AS g(n)
ORDER BY u.id, n;

COMMIT;

SELECT count(*) AS user_count FROM users;
SELECT count(*) AS order_count FROM orders;
