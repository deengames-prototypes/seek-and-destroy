using SeekAndDestroy.Core.DataAccess;
using Dapper;
using Npgsql;

namespace SeekAndDestroy.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public int CreateUser(string oauthId, string emailAddress)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Execute("INSERT INTO users (oauth_id, email_address) VALUES (@oauthId, @emailAddress);", new { oauthId, emailAddress });
            }

            return this.GetUserId(emailAddress);
        }

        public int GetUserId(string emailAddress)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT user_id FROM users WHERE email_address = @emailAddress;", new { emailAddress });
            }
        }
    }
}