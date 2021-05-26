CREATE OR REPLACE FUNCTION delete_user(
 user_id_in uuid
)
RETURNS int AS $$
DECLARE count int;
BEGIN
    DELETE FROM users.users 
    WHERE user_id = user_id_in;
    
    GET DIAGNOSTICS count = ROW_COUNT;
    
    RETURN count;
END 
$$ LANGUAGE plpgsql;