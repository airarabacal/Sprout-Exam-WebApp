using Sprout.Exam.Common.DataTransferObjects;
using System;

namespace Sprout.Exam.Business
{
    public class RegularCalculateSalary : CalculateSalary
    {
        private double _tax = 0.12;
        private double _rate = 20000;

        public RegularCalculateSalary(CalculateSalaryDto calculateSalaryInfo) : base(calculateSalaryInfo)
        { }
        public override double ComputeSalary()
        {
            double salaryDeducted = 0;
            double taxDeducted = _rate * _tax;
            if (CalculateSalaryInfo.AbsentDays > 0)
            {
                salaryDeducted = (_rate / 22) * CalculateSalaryInfo.AbsentDays;
            }
            return Math.Round(_rate - salaryDeducted - taxDeducted, 2);
        }

    }
}
