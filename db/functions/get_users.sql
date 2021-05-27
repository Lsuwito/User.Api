CREATE OR REPLACE FUNCTION get_users(
 sort_by_in TEXT,
 sort_ascending_in BOOLEAN,
 limit_in INT,
 last_sort_value_in TEXT DEFAULT NULL,
 last_secondary_sort_value_in TEXT DEFAULT NULL
)
RETURNS SETOF users.users AS $$
DECLARE 
    sql TEXT;
    sortPart TEXT;
    wherePart TEXT = '';
BEGIN
    IF last_sort_value_in IS NOT NULL THEN
        -- where condition for the cursor starting point
        wherePart = 'WHERE ' 
                    || quote_ident(sort_by_in) 
                    || ' > ' 
                    || quote_literal(last_sort_value_in)
                    || ' OR ('
                    || quote_ident(sort_by_in)
                    || ' = '
                    || quote_literal(last_sort_value_in)
                    || 'AND user_id > '
                    || quote_literal(last_secondary_sort_value_in)
                    || ')';    
    END IF;
    
    sortPart = 'ORDER BY ' 
               || quote_ident(sort_by_in) 
               || ' ' 
               || CASE WHEN sort_ascending_in THEN 'ASC' ELSE 'DESC' END 
               || ', user_id ASC';
    
    RETURN QUERY EXECUTE 'SELECT user_id, email, role, status, created FROM users.users ' 
                         || wherePart 
                         || ' '
                         || sortPart 
                         || ' LIMIT $1'
                 USING limit_in;
    
END 
$$ LANGUAGE plpgsql;