using Sprout.Exam.WebApp.Classes.Creators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes
{
    public abstract class SalaryFactory
    {
        public abstract void ComputeSalary();
        public abstract double GetSalary();
    }
}
