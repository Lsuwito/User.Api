CREATE OR REPLACE FUNCTION create_user(
 email_in varchar(255),
 role_in int,
 status_in int
)
RETURNS uuid AS $$
DECLARE inserted_user_id uuid;
BEGIN
    INSERT INTO users.users (email, role, status)
    VALUES (email_in, role_in, status_in)
    RETURNING user_id
    INTO inserted_user_id;
    
    RETURN inserted_user_id;
END 
$$ LANGUAGE plpgsql;