using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Business.Creators
{
    public class ConcreteSalaryFactory : SalaryFactory
    {
        public override ICalculateSalary GetSalaryFactory(CalculateSalaryDto input)
        {
            var type = (EmployeeType)input.EmployeeTypeId;
            switch (type)
            {
                case EmployeeType.Regular:
                    return new RegularCalculateSalary(input);
                case EmployeeType.Contractual:
                    return new ContractualCalculateSalary(input);
                default:
                    throw new ApplicationException(string.Format("Salary creator '{0}' cannot be created", type));
            }
        }
    }
}
