using System.Data;
using DotNetAlegrarioAPI.Data;
using DotNetAlegrarioAPI.Dtos;
using DotNetAlegrarioAPI.Helpers;
using DotNetAlegrarioAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace APIFinalTouch.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        private readonly AuthHelper _authHelper;

        private readonly ReusableSql _reusabelSql;

        private readonly IMapper _mapper;
        
        public AuthController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
            _authHelper = new AuthHelper(config);
            _reusabelSql = new ReusableSql(config);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserForRegistrationDto, User>();
            }));
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public  IActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration.Password == userForRegistration.PasswordConfirm)
            {
                string slqCheckUserExists = "SELECT Email FROM AlegrarioAppSchema.Auth WHERE Email = '"+
                userForRegistration.Email+"'";

                IEnumerable<string> userExists =  _dapper.LoadData<string>(slqCheckUserExists);

                if (userExists.Count() == 0)
                {
                    UserForLoginDto useForSetPassword = new UserForLoginDto()
                    {
                        Email = userForRegistration.Email,
                        Password = userForRegistration.Password,

                    };
                    if (_authHelper.SetPassword(useForSetPassword))
                    {
                        User userComplete = _mapper.Map<User>(userForRegistration);
                        userComplete.Active = true;

                        if (_reusabelSql.UpsertUser(userComplete))
                        {
                            return Ok ();
                        }
                        throw new Exception ("Failed to ADD user!");
                    }
                    throw new Exception ("Failed to register user!");
                }

                throw new Exception ("User with this email already exists!");
            }

            throw new Exception ("Passwords don't match!");
        }

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword (UserForLoginDto useForSetPassword)
        {

            string sql = "SELECT [Email] FROM AlegrarioAppSchema.Users WHERE UserId = " + this.User.FindFirst("userId")?.Value;
            string crrtUserEmail = _dapper.LoadDataSingle<string>(sql);
            if (crrtUserEmail == useForSetPassword.Email)
            {
                
            if (_authHelper.SetPassword(useForSetPassword))
            {
                return Ok();
            }
            throw new Exception("Falied to reset password!");

            }
            throw new Exception("You are not allowed to change password of other users!");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public  IActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"EXEC AlegrarioAppSchema.spLoginConfirmation_Get 
            @Email = @EmailParam ";

            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@EmailParam", userForLogin.Email, DbType.String);

            UserForLoginConfirmationDto userForConfirmation = _dapper.LoadDataSingleWithParameters<UserForLoginConfirmationDto>(sqlForHashAndSalt,sqlParameters);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForConfirmation.PasswordSalt);

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForConfirmation.PasswordHash[index])
                {
                    return  StatusCode(401,"Incorrect Password!");
                }
            }

            
            string userIdSql = @"SELECT [UserId]
            FROM AlegrarioAppSchema.Users WHERE Email = '"+userForLogin.Email+"'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok (new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userId)}
            });
        }
        
        [HttpGet("RefreshToken")]
        public string RefreshToken()
        {
            
            string userIdSql = @"SELECT [UserId]
            FROM AlegrarioAppSchema.Users WHERE UserId = '"+User.FindFirst("userId")?.Value+"'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return _authHelper.CreateToken(userId);
        }
        
    }
}