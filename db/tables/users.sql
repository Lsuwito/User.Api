CREATE TABLE IF NOT EXISTS users (
  user_id uuid NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
  email varchar(255) NOT NULL CONSTRAINT users_email_key UNIQUE,
  role int NOT NULL,
  status int NOT NULL,
  created timestamp NOT NULL DEFAULT (now() AT TIME ZONE 'UTC')
)