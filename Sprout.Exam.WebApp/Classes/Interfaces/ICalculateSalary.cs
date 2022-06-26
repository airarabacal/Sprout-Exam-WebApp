using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Classes.Interfaces
{
    public interface ICalculateSalary
    {
        double Rate{ get; set; }

        double Days { get; set; }
        double ComputeSalary();
    }
}
