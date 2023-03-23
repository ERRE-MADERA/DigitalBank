using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class storedProcedures4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER PROCEDURE dbo.CRUD_USER(
                     @PROCESS CHAR(1),
                     @NAME VARCHAR(100)='',
                     @BIRTHDATE DATETIME=NULL,
                     @SEX BIT=FALSE,
                     @ID INT =0 OUTPUT
                )AS BEGIN 
                SET NOCOUNT ON
                    IF @PROCESS = 'C'
                    BEGIN
                        INSERT INTO USERINFO ( NAME, BIRTHDATE, SEX  )
                        VALUES (@NAME,@BIRTHDATE,@SEX  )

                        SELECT @ID = SCOPE_IDENTITY()
                    END
                    ELSE
                    BEGIN
                        IF @PROCESS = 'R'
                        BEGIN
                           SELECT ID, NAME, BIRTHDATE, SEX FROM USERINFO ORDER BY NAME
                        END
                        ELSE
                        BEGIN
                            IF @PROCESS = 'U'
                            BEGIN
                               UPDATE USERINFO SET NAME = @NAME, 
                                               BIRTHDATE = @BIRTHDATE, 
                                               SEX = @SEX 
                               WHERE ID = @ID                               
                            END
                            ELSE
                            BEGIN
                                IF @PROCESS = 'D'
                                BEGIN
                                  DELETE FROM USERINFO WHERE ID = @ID                               
                                END        
                            END
                        END
                    END
                SET NOCOUNT OFF
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.CRUD_USER");
        }
    }
}
