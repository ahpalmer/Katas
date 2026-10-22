The important part of this kata are these lines of code:

ALTER TABLE orders
ADD CONSTRAINT orders_user_id_fkey
FOREIGN KEY (user_id) REFERENCES users(id);

This alters the orders table so that the foreign key (user_id_) that resides in the orders table,
must have a real reference to the id variable in the users table 'users(id)'. If there is no 
reference, the operation will fail.

PostgreSQL therefore blocks these operations:

┌──────────────────────────────────────────────────────────────┬─────────────────────────────────────────────────────┐
│ Operation                                                    │ Why it fails                                        │
├──────────────────────────────────────────────────────────────┼─────────────────────────────────────────────────────┤
│ Insert an order for user 999999 when that user doesn’t exist │ The order would reference nobody                    │
├──────────────────────────────────────────────────────────────┼─────────────────────────────────────────────────────┤
│ Change an order’s user_id to a nonexistent user              │ Same problem, caused by an update                   │
├──────────────────────────────────────────────────────────────┼─────────────────────────────────────────────────────┤
│ Delete Alice while her orders still exist                    │ Her orders would be left pointing to a missing user │
└──────────────────────────────────────────────────────────────┴─────────────────────────────────────────────────────┘

Here is the test of the code:
(lss2-foreign-key) PS C:...\LSS2ForeignKey> python main.py users delete 1        
PostgreSQL operation failed (SQLSTATE 23503): update or delete on table "users" violates foreign key constraint "orders_user_id_fkey" on table "orders"
DETAIL:  Key (id)=(1) is still referenced from table "orders".
(lss2-foreign-key) PS C:...\LSS2ForeignKey> python main.py orders insert --user-id 999999 --total 12.50
PostgreSQL operation failed (SQLSTATE 23503): insert or update on table "orders" violates foreign key constraint "orders_user_id_fkey"
DETAIL:  Key (user_id)=(999999) is not present in table "users".
(lss2-foreign-key) PS C:...\LSS2ForeignKey> python main.py orders update 1 --user-id 999999 --total 20.00
PostgreSQL operation failed (SQLSTATE 23503): insert or update on table "orders" violates foreign key constraint "orders_user_id_fkey"
DETAIL:  Key (user_id)=(999999) is not present in table "users".

P.S.
The 'cursor' object is a python database object used to execute SQL and retrieve its results
For example:

 with connection.cursor(row_factory=class_row(User)) as cursor:
     cursor.execute(
         "SELECT id, name, email FROM users WHERE id = %s",
         (1,),
     )
     user = cursor.fetchone()