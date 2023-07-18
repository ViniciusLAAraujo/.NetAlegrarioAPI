using System.Data;
using DotNetAlegrarioAPI.Data;
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

    //public IEnumerable<DayCell> GetDays(int userId = 0")
    public IEnumerable<DayCell> GetWeekDays()
    {
        string sql = @"EXEC AlegrarioAppSchema.spDayCellWeek_Get @UserId = @UserIdParam";

        DynamicParameters  sqlParameters = new DynamicParameters();
        //sqlParameters.Add("@UserIdParam",userId, DbType.Int32);
        sqlParameters.Add("@UserIdParam",this.User.FindFirst("userId")?.Value, DbType.Int32);

        IEnumerable<DayCell> dayCells = _dapper.LoadDataWithParameters<DayCell>(sql,sqlParameters);
        return dayCells;
    }
    
    [HttpPost("InsertDayCell")]
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
        throw new Exception("Failed to Insert DayCell!");
    }


    //[HttpGet("GetEmotions/{userId}/{Month}/{HourId}/{EmotionValue}/{DateSerach}")]
    [HttpGet("GetEmotions/{Month}/{HourId}/{EmotionValue}/{DateSerach}")]

    //public IEnumerable<Emotion> GetEmotions(int userId = 0,  int Month = 0, int HourId = 24, int EmotionValue = 0 , string DateSerach = "1900-01-01")
    public IEnumerable<Emotion> GetEmotions( int Month = 0, int HourId = 24, int EmotionValue = 0 ,string DateSerach = "1900-01-01")
    {
        string sql = @"EXEC AlegrarioAppSchema.spEmotion_Get @UserId = @UserIdParam";

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
        if (EmotionValue > 0 && EmotionValue < 5)
        {
            stringParameters += ", @EmotionValue = @EmotionValueParam ";
            sqlParameters.Add("@EmotionValueParam",EmotionValue, DbType.Int32);
        }
        if (HourId >= 0 && HourId < 24)
        {
            stringParameters += ", @HourId = @HourIdParam ";
            sqlParameters.Add("@HourIdParam",HourId, DbType.Int32);
        }
        if (stringParameters.Length > 0)
        {
            sql+= stringParameters;
        }

        IEnumerable<Emotion> emotions = _dapper.LoadDataWithParameters<Emotion>(sql,sqlParameters);
        return emotions;
    }

    [HttpPut("UpsertEmotion")]
    public IActionResult UpsertEmotion(EmotionToAddDto emotion)
    {
        string sql = @"
        EXEC AlegrarioAppSchema.spEmotion_Upsert 
            @UserId = @UserIdParam " +
            ",@CellDay = @CellDayParam " +
            ", @HourId = @HourIdParam " +
            ", @EmotionValue = @EmotionValueParam " +
            ", @Comment = @CommentParam "+
            ", @Score = @ScoreParam ";
            DynamicParameters  sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParam", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@CellDayParam",emotion.CellDay, DbType.Date);
            sqlParameters.Add("@HourIdParam",(emotion.HourId < 0 || emotion.HourId > 23) ? 0 : emotion.HourId, DbType.Int32);
            sqlParameters.Add("@EmotionValueParam ",(emotion.EmotionValue <= 0 || emotion.EmotionValue > 4) ? 2 : emotion.EmotionValue, DbType.Int32);
            sqlParameters.Add("@CommentParam",emotion.Comment, DbType.String);
            sqlParameters.Add("@ScoreParam",(emotion.Score < 1 || emotion.Score > 10) ? 5 : emotion.Score, DbType.Int32);

        
        if (_dapper.ExecuteSqlWithParameters(sql,sqlParameters))
        {
            return Ok();
        }
        throw new Exception("Failed to Upsert Emotion!");
    }

    //[HttpGet("GetEmotionsStats/{userId}/{DateSerach}/{EmotionValue}/{IncludeAllEmotions}")]
    [HttpGet("GetEmotionsStats/{DateSerach}/{EmotionValue}/{IncludeAllEmotions}")]

    //public IEnumerable<Emotion> GetEmotionsStats(int userId = 0,  string DateSerach = "1900-01-01", int EmotionValue = 0 , bool IncludeAllEmotions = false)
    public IEnumerable<EmotionStatToReceiveDto> GetEmotionsStats(  string DateSerach = "1900-01-01", int EmotionValue = 0 ,bool IncludeAllEmotions = false)
    {
        string sql = @"EXEC AlegrarioAppSchema.spDayCell_GetEmotionStats @UserId = @UserIdParam";

        DynamicParameters  sqlParameters = new DynamicParameters();
        string stringParameters = "";
        //sqlParameters.Add("@UserIdParam",userId, DbType.Int32);
        sqlParameters.Add("@UserIdParam",this.User.FindFirst("userId")?.Value, DbType.Int32);

        if (IncludeAllEmotions)
        {
            stringParameters += ", @IncludeAllEmotions = 1";
        }
        else
        {
            if (EmotionValue > 0 && EmotionValue < 5)
            {
                stringParameters += ", @EmotionValue = @EmotionValueParam ";
                sqlParameters.Add("@EmotionValueParam",EmotionValue, DbType.Int32);
            }
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

        IEnumerable<EmotionStatToReceiveDto> emotionsStats = _dapper.LoadDataWithParameters<EmotionStatToReceiveDto>(sql,sqlParameters);
        return emotionsStats;
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

        throw new Exception("Failed to Delete DayCell");
    }

    [HttpDelete("DeleteEmotion")]
    public IActionResult DeleteEmotion(EmotionToDeleteDto emotion)
    {
        string sql = @"
            AlegrarioAppSchema.spEmotion_Delete
                @UserId = @UserIdParam ,
                @CellDay = @CellDayParam,
                @HourId = @HourIdParam";
        DynamicParameters  sqlParameters = new DynamicParameters();
        sqlParameters.Add("@UserIdParam",this.User.FindFirst("userId")?.Value, DbType.Int32);
        sqlParameters.Add("@CellDayParam",emotion.CellDay, DbType.Date);
        sqlParameters.Add("@HourIdParam",emotion.HourId, DbType.Int32);

        if (_dapper.ExecuteSqlWithParameters(sql,sqlParameters))
        {
            return Ok();
        } 

        throw new Exception("Failed to Delete Emotion");
    }
}