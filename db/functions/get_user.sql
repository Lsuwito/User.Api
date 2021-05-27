CREATE OR REPLACE FUNCTION get_user(
 user_id_in uuid
)
RETURNS SETOF users.users AS $$
BEGIN
    RETURN QUERY
    SELECT user_id, email, role, status, created
    FROM users.users
    WHERE user_id = user_id_in;
END 
$$ LANGUAGE plpgsql;