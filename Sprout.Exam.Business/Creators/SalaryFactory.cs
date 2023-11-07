using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.DataTransferObjects;

namespace Sprout.Exam.Business
{
    public abstract class SalaryFactory
    {
        public abstract ICalculateSalary GetSalaryFactory(CalculateSalaryDto input);
    }
}
