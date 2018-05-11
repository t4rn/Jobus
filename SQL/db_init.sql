-- password: jobusDevPasswd
CREATE ROLE jobus_user LOGIN
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;

  CREATE DATABASE jobus_dev WITH OWNER = jobus_user;

  INSERT INTO jobus.ws_clients(client_name, hash) VALUES ('ADMIN', 'hash4admin');