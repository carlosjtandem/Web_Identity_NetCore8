1. Postgres Docker installation
docker image ls
docker pull postgres 
docker run --name my-postgres -e POSTGRES_PASSWORD=mysecretpassword -d -p 6432:5432 postgres    --Asigned 6432 to connect locally in another port

1. Connect to the DB via DBeaver
1.1 Create DB testdb
1.2 Create table
	
	GRANT ALL PRIVILEGES ON DATABASE test_db TO postgres;

    CREATE TABLE users (
      Id SERIAL PRIMARY KEY,
      Email VARCHAR(255) UNIQUE NOT NULL,
      PasswordHash VARCHAR(255) NOT NULL,
      EmailConfirmed BOOLEAN DEFAULT FALSE
    );

Note. check the permission and be sure that you can create a query with the user.


2. Dependencies an run
- Restore dependencies.
- Update the DB connection in appsetings.json