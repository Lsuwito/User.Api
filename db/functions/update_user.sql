CREATE OR REPLACE FUNCTION update_user(
 user_id_in uuid,
 role_in int,
 status_in int
)
RETURNS int AS $$
DECLARE count int;
BEGIN
    UPDATE users.users 
    SET role = role_in, status = status_in
    WHERE user_id = user_id_in;
    
    GET DIAGNOSTICS count = ROW_COUNT;
    
    RETURN count;
END 
$$ LANGUAGE plpgsql;