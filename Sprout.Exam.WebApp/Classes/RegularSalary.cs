using Sprout.Exam.WebApp.Classes.Creators;
using Sprout.Exam.WebApp.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes
{
    public class RegularSalary : ICalculateSalary
    {
        private double _absentDays;
        private double _rate;
        private double _tax = 0.12;
        public double Rate
        {
            get => _rate;
            set => _rate = value;
        }

        public double Days
        {
            get => _absentDays;
            set => _absentDays = value;
        }
        public double ComputeSalary()
        {
            double salaryDeducted = 0;
            double taxDeducted = Rate * _tax;
            if (Days > 0)
            {
                salaryDeducted = (Rate / 22) * Days;
            }
            return Math.Round(Rate - salaryDeducted - taxDeducted, 2);
        }

    }
}
