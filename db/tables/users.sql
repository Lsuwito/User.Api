CREATE TABLE IF NOT EXISTS users (
  user_id uuid NOT NULL PRIMARY KEY DEFAULT gen_random_uuid(),
  email varchar(255) NOT NULL CONSTRAINT users_email_key UNIQUE,
  role role NOT NULL,
  status status NOT NULL
)