using System.Data;
using DotNetAlegrarioAPI.Data;
using DotNetAlegrarioAPI.Helpers;
using DotNetAlegrarioAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAlegrarioAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly DataContextDapper _dapper;

    public TestController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("Test")]
    public string Test()
    {
        return "Application running...";
    }


}