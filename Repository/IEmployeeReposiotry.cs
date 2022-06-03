using Caching.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caching.Repository
{
    public interface IEmployeeReposiotry
    {
        List<Employee> GetEmployeDetails();
        List<Employee> GetEmploye(int Id);
    }
}
