using System.Data;
using DotNetAlegrarioAPI.Data;
using DotNetAlegrarioAPI.Helpers;
using DotNetAlegrarioAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAlegrarioAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContextDapper _dapper;

    private readonly ReusableSql _reusabelSql;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
        _reusabelSql = new ReusableSql(config);
    }
    

    [HttpGet("GetUsers/{userId}/{isActive}")]

    public IEnumerable<User> GetUsers(int userId = 0,  bool isActive = true)
    {
        string sql = @"EXEC AlegrarioAppSchema.spUser_Get ";

        DynamicParameters  sqlParameters = new DynamicParameters();
        string stringParameters = "";

        if (userId > 0)
        {
            stringParameters += ", @UserId = @UserIdParam";
            sqlParameters.Add("@UserIdParam",userId, DbType.Int32);
        }
        if (isActive)
        {
            stringParameters += ", @Active = @ActiveParam";
            sqlParameters.Add("@ActiveParam",isActive, DbType.Boolean);
        }
        if (stringParameters.Length > 0)
        {
            sql+= stringParameters.Substring(1);
        }
        IEnumerable<User> users = _dapper.LoadDataWithParameters<User>(sql,sqlParameters);
        return users;
    }

    
    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(User user)
    {

        if (_reusabelSql.UpsertUser(user))
        {
            return Ok();
        } 

        throw new Exception("Failed to Upsert User");
    }


    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            AlegrarioAppSchema.spUser_Delete
                @UserId = @UserIdParam";
        DynamicParameters  sqlParameters = new DynamicParameters();
        sqlParameters.Add("@UserIdParam",userId, DbType.Int32);

        if (_dapper.ExecuteSqlWithParameters(sql,sqlParameters))
        {
            return Ok();
        } 

        throw new Exception("Failed to Delete User");
    }
}