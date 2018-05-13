-- password: jobusDevPasswd
CREATE ROLE jobus_user LOGIN
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;

  CREATE DATABASE jobus_dev WITH OWNER = jobus_user;

  INSERT INTO jobus.ws_clients(client_name, hash) VALUES ('ADMIN', 'hash4admin');
  INSERT INTO jobus.resource(controller, action) VALUES ('invest','ping');
  INSERT INTO jobus.ws_client_resource(id_ws_client, id_resource) VALUES (1, 1);