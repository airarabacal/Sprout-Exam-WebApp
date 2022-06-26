using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.DataAccessLayer
{
    public class Transfer
    {
        private string _connectionString;

        public Transfer(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string procedure)
                => (await GetAsync<T>(procedure, 0));
        public async Task<IEnumerable<T>> GetAsync<T>(string procedure, int id)
        {
            List<T> models = new List<T>();
            var instance = DALFactory.GetDALIntance<T>();
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(procedure, sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (id > 0) cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            models.Add(instance.Init(reader));
                        }
                    }
                }
            }
            return (IEnumerable<T>)models;
        }

        public async Task<T> PostAsync<T>(string procedure, T model)
        {
            var instance = DALFactory.GetDALIntance<T>();
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(procedure, sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    return await instance.SaveAsync(cmd, model);
                }
            }
        }

        public async Task DeleteAsync(string procedure, int id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                sql.Open();
                using (SqlCommand cmd = new SqlCommand(procedure, sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
