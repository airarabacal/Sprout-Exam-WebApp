using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.DataTransferObjects;

namespace Sprout.Exam.Business
{
    public class CalculateSalary : ICalculateSalary
    {
        private CalculateSalaryDto _input;
        public CalculateSalaryDto CalculateSalaryInfo
        {
            get => _input;
            set => _input = value;
        }

        public CalculateSalary(CalculateSalaryDto calculateSalaryInfo)
        {
            CalculateSalaryInfo = calculateSalaryInfo;
        }
        public virtual double ComputeSalary()
        {
            //to be overriden
            return 0;
        }
    }
}
