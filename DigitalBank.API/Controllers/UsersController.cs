

namespace DigitalBank.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Infraestructure.Models;
    using Microsoft.Data.SqlClient;
    using System.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;
    using System.Reflection.Metadata;

    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>>Post(UserInfo userInfo)
        {
            var parameterId = new SqlParameter("@ID", SqlDbType.Int);
            parameterId.Direction = ParameterDirection.Output;
            string process = "C";
            await context.Database
                .ExecuteSqlInterpolatedAsync($@"EXEC CRUD_USER @PROCESS={process},
                    @Name = {userInfo.Name}, @Birthdate = {userInfo.Birthdate},
                     @sex = {userInfo.Sex}, @ID = {parameterId} OUTPUT");
            return Ok((int)parameterId.Value);
        }

        [HttpGet]
        public async Task<IAsyncEnumerable<UserInfo>> Get()
        {
            string process = "R";
            var users =  context.UserInfo.FromSqlInterpolated($@"EXEC CRUD_USER  @PROCESS={process}").AsAsyncEnumerable();

            return  users;

        }

        [HttpGet("{id:int}")]
        public IAsyncEnumerable<UserInfo> GetById(int? id)
        {
            string process = "I";
            var users = context.UserInfo.FromSqlInterpolated($@"EXEC CRUD_USER  @PROCESS={process}, @ID = {id}").AsAsyncEnumerable();

            return users;

        }

        [HttpPut]
        public async Task<ActionResult> Put(UserInfo userInfo)
        {
            string process = "U";
            await context.Database
                .ExecuteSqlInterpolatedAsync($@"EXEC CRUD_USER @PROCESS={process},
                    @Name = {userInfo.Name}, @Birthdate = {userInfo.Birthdate},
                     @sex = {userInfo.Sex}, @ID = {userInfo.Id} ");
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            
            string process = "D";
            await context.Database
                .ExecuteSqlInterpolatedAsync($@"EXEC CRUD_USER @PROCESS={process},
                    @ID = {id} ");
            return Ok();
        }

    }
}
