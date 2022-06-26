using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataAccessLayer
{
    public static class DALFactory
    {
        public static IDataTransferObject<T> GetDALIntance<T>()
        {
            var type = typeof(T);
            if(type == typeof(EmployeeDto))
            {
                return (IDataTransferObject<T>)EmployeeDtoDAL.Instantiate();
            }
            throw new Exception("Cannot find DAL object for " + typeof(T).ToString());
        }
    }
}
