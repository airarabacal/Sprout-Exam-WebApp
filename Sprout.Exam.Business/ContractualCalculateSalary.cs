using Sprout.Exam.Common.DataTransferObjects;
using System;

namespace Sprout.Exam.Business
{
    public class ContractualCalculateSalary : CalculateSalary
    {
        private double _rate = 500;

        public ContractualCalculateSalary(CalculateSalaryDto calculateSalaryInfo) : base(calculateSalaryInfo)
        { }
        public override double ComputeSalary()
        {
            double salary = 0;
            if(CalculateSalaryInfo.WorkDays > 0)
                salary = Math.Round(_rate * CalculateSalaryInfo.WorkDays, 2);

            return salary;
        }

    }
}
