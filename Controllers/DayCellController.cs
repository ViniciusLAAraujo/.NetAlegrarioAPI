using System.Data;
using DotNetAlegrarioAPI.Data;
using DotNetAlegrarioAPI.Helpers;
using DotNetAlegrarioAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotNetAlegrarioAPI.Dtos;

namespace DotNetAlegrarioAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DayCellController : ControllerBase
{
    private readonly DataContextDapper _dapper;

    public DayCellController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }
    
    //[HttpGet("GetDays/{userId}/{Month}/{DateSerach}")]
    [HttpGet("GetDays/{Month}/{DateSerach}")]

    //public IEnumerable<DayCell> GetDays(int userId = 0,  int Month = 0, string DateSerach = "1900-01-01")
    public IEnumerable<DayCell> GetDays( int Month = 0, string DateSerach = "1900-01-01")
    {
        string sql = @"EXEC AlegrarioAppSchema.spDayCell_Get @UserId = @UserIdParam";

        DynamicParameters  sqlParameters = new DynamicParameters();
        string stringParameters = "";
        //sqlParameters.Add("@UserIdParam",userId, DbType.Int32);
        sqlParameters.Add("@UserIdParam",this.User.FindFirst("userId")?.Value, DbType.Int32);

        if (Month > 0 && Month < 13)
        {
            stringParameters += ", @MonthCells = @MonthParam";
            sqlParameters.Add("@MonthParam",Month, DbType.Int32);
        }
        if (DateSerach != "1900-01-01")
        {
            stringParameters += ", @CellDay = @CellDayParam ";
            sqlParameters.Add("@CellDayParam",DateSerach, DbType.Date);
        }
        if (stringParameters.Length > 0)
        {
            sql+= stringParameters;
        }

        IEnumerable<DayCell> dayCells = _dapper.LoadDataWithParameters<DayCell>(sql,sqlParameters);
        return dayCells;
    }

    //[HttpGet("GetWeekDays/{userId}")]
    [HttpGet("GetWeekDays")]

    //public IEnumerable<DayCell> GetDays(int userId = 0")y
    public IEnumerable<DayCell> GetWeekDays()
    {
        string sql = @"EXEC AlegrarioAppSchema.spDayCellWeek_Get @UserId = @UserIdParam";

        DynamicParameters  sqlParameters = new DynamicParameters();
        //sqlParameters.Add("@UserIdParam",userId, DbType.Int32);
        sqlParameters.Add("@UserIdParam",this.User.FindFirst("userId")?.Value, DbType.Int32);

        IEnumerable<DayCell> dayCells = _dapper.LoadDataWithParameters<DayCell>(sql,sqlParameters);
        return dayCells;
    }
    
    [HttpPut("InsertDayCell")]
    public IActionResult InsertCellDay(DayCell dayCell)
    {
        string sql = @"
        EXEC AlegrarioAppSchema.spDayCell_Insert
            @UserId = @UserIdParam " +
            ", @CellDay = @CellDayParam ";
            DynamicParameters  sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParam", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@CellDayParam",dayCell.CellDay, DbType.Date);

        
        if (_dapper.ExecuteSqlWithParameters(sql,sqlParameters))
        {
            return Ok();
        }
        throw new Exception("Failed to Upsert Post!");
    }



    [HttpDelete("DeleteDay")]
    public IActionResult DeleteDay(DayToDeleteDto day)
    {
        string sql = @"
            AlegrarioAppSchema.spDayCell_Delete
                @UserId = @UserIdParam ,
                @CellDay = @CellDayParam";
        DynamicParameters  sqlParameters = new DynamicParameters();
        sqlParameters.Add("@UserIdParam",this.User.FindFirst("userId")?.Value, DbType.Int32);
        sqlParameters.Add("@CellDayParam",day.CellDay, DbType.Date);

        if (_dapper.ExecuteSqlWithParameters(sql,sqlParameters))
        {
            return Ok();
        } 

        throw new Exception("Failed to Delete User");
    }
}