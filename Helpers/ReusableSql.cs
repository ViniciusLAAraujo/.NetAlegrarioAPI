using System.Data;
using DotNetAlegrarioAPI.Data;
using DotNetAlegrarioAPI.Models;
using Dapper;

namespace DotNetAlegrarioAPI.Helpers
{
    public class ReusableSql
    {
        private readonly DataContextDapper _dapper;
        public ReusableSql(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        public bool UpsertUser(User user)
        {

            string sql = @"
            EXEC AlegrarioAppSchema.spUser_Upsert
                @FirstName = @FirstNameParam " + 
                ", @LastName = @LastNameParam " + 
                ", @Email = @EmailParam " + 
                ", @Gender= @GenderParam " + 
                ", @Active  = @ActiveParam " + 
                ", @UserId  = @UserIdParam " ;

            DynamicParameters  sqlParameters = new DynamicParameters();
            sqlParameters.Add("@FirstNameParam",user.FirstName, DbType.String);
            sqlParameters.Add("@LastNameParam",user.LastName, DbType.String);
            sqlParameters.Add("@EmailParam",user.Email, DbType.String);
            sqlParameters.Add("@GenderParam",user.Gender, DbType.String);
            sqlParameters.Add("@ActiveParam",user.Active, DbType.Boolean);
            sqlParameters.Add("@UserIdParam",user.UserId, DbType.Int32);
            //user.Salary.ToString("0.00", CultureInfo.InvariantCulture)
            

            return _dapper.ExecuteSqlWithParameters(sql,sqlParameters);
        }   
    }
}