CREATE OR REPLACE FUNCTION create_user(
 email varchar(255),
 role int,
 status int
)
RETURNS uuid AS $$
DECLARE inserted_user_id uuid;
BEGIN
    INSERT INTO users.users (email, role, status)
    VALUES (email, role, status)
    RETURNING user_id
    INTO inserted_user_id;
    
    RETURN inserted_user_id;
END 
$$ LANGUAGE plpgsql;