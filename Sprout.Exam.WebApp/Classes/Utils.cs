using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Classes.Creators;
using Sprout.Exam.WebApp.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes
{
    public class Utils
    {
        public static CalculateSalaryCreator GetCreator(EmployeeType type)
        {
            CalculateSalaryCreator factory = null;
            switch (type)
            {
                case EmployeeType.Regular:
                    factory = new RegularSalaryCreator();
                    break;
                case EmployeeType.Contractual:
                    factory = new ContractualSalaryCreator();
                    break;
            }
            return factory;
        }

        public static double GetRate(EmployeeType type)
        {
            double rate = 0;
            switch (type)
            {
                case EmployeeType.Regular:
                    rate = 20000;
                    break;
                case EmployeeType.Contractual:
                    rate = 500;
                    break;
            }
            return rate;
        }
    }
}
