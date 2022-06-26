using Sprout.Exam.WebApp.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes.Creators
{
    public class ContractualSalaryCreator : CalculateSalaryCreator
    {
        public override ICalculateSalary FactoryMethod()
        {
            return new ContractualSalary();
        }
    }
}
