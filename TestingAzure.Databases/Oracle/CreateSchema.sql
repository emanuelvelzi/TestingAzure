alter session set "_ORACLE_SCRIPT"=true;  
create user emanuelvelzi identified by Claro$701234;
create role basic_user ;
grant basic_user to emanuelvelzi;
GRANT CREATE SESSION to basic_user;
GRANT create session TO basic_user;
GRANT create table TO basic_user;
GRANT create view TO basic_user;
GRANT create any trigger TO basic_user;
GRANT create any procedure TO basic_user;
GRANT create sequence TO basic_user;
GRANT create synonym TO basic_user;


BEGIN
   FOR objects IN
   (
         SELECT 'GRANT ALL ON "'||owner||'"."'||object_name||'" TO emanuelvelzi' grantSQL
           FROM all_objects
          WHERE owner = 'MY_SCHEMA'
            AND object_type NOT IN
                (
                   --Ungrantable objects.  Your schema may have more.
                   'SYNONYM', 'INDEX', 'INDEX PARTITION', 'DATABASE LINK',
                   'LOB', 'TABLE PARTITION', 'TRIGGER'
                )
       ORDER BY object_type, object_name
   ) LOOP
      BEGIN
         EXECUTE IMMEDIATE objects.grantSQL;
      EXCEPTION WHEN OTHERS THEN
         --Ignore ORA-04063: view "X.Y" has errors.
         --(You could potentially workaround this by creating an empty view,
         -- granting access to it, and then recreat the original view.) 
         IF SQLCODE IN (-4063) THEN
            NULL;
         --Raise exception along with the statement that failed.
         ELSE
            raise_application_error(-20000, 'Problem with this statement: ' ||
               objects.grantSQL || CHR(10) || SQLERRM);
         END IF;
      END;
   END LOOP;
END;
/