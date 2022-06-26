using Sprout.Exam.WebApp.Classes.Creators;
using Sprout.Exam.WebApp.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes
{
    public class ContractualSalary : ICalculateSalary
    {
        private double _workedDays;
        private double _rate;
        public double Rate
        {
            get => _rate;
            set => _rate = value;
        }

        public double Days
        {
            get => _workedDays;
            set => _workedDays = value;
        }
        public double ComputeSalary()
        {
            return Math.Round(Rate * Days, 2);
        }

    }
}
