using Sprout.Exam.WebApp.Classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes.Creators
{
    public abstract class CalculateSalaryCreator
    {
        public abstract ICalculateSalary FactoryMethod();

        public double CalculateSalary(double rate, double days)
        {
            var factory = FactoryMethod();
            factory.Days = days;
            factory.Rate = rate;
            var result = factory.ComputeSalary();

            return result;
        }

        public void AddProperties()
        {

        }
    }
}
