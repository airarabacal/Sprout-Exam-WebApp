using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.DataAccessLayer
{
    public class EmployeeDtoDAL: IDataTransferObject<EmployeeDto>
    {
        private readonly static EmployeeDtoDAL Instance = new EmployeeDtoDAL();
        public static IDataTransferObject<EmployeeDto> Instantiate()
        {
            return Instance;
        }

        public EmployeeDto Init(SqlDataReader dr)
        {
            var employee = new EmployeeDto();
            employee.Id = (int)dr["Id"];
            employee.FullName = dr["FullName"] != null ? (string)dr["FullName"] : "";
            employee.Birthdate = dr["Birthdate"] != null ? ((DateTime)dr["Birthdate"]).ToString("yyyy-MM-dd") : "";
            employee.Tin = dr["Tin"] != null ? (string)dr["Tin"] : "";
            employee.TypeId = dr["EmployeeTypeId"] != null ? (int)dr["EmployeeTypeId"] : 0;
            return employee;
        }

        public async Task<EmployeeDto> SaveAsync(SqlCommand cmd, EmployeeDto obj)
        {
            addParam(cmd, obj);
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
            {
                if (await dr.ReadAsync())
                    return Instance.Init(dr);
                return new EmployeeDto();
            }
        }

        private void addParam(SqlCommand cmd, EmployeeDto emp)
        {
            if (emp.Id > 0) cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@FullName", emp.FullName);
            cmd.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime(emp.Birthdate));
            cmd.Parameters.AddWithValue("@TIN", emp.Tin);
            cmd.Parameters.AddWithValue("@EmployeeTypeId", emp.TypeId);

        }
    }
}
    