using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.DataAccessLayer
{
    public interface IDataTransferObject<T>
    {
        T Init(SqlDataReader dr);
        Task<T> SaveAsync(SqlCommand cmd, T obj);
    }
}
