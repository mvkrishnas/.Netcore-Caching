using Caching.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Caching.Repository
{
    public class EmployeeReposiotry : IEmployeeReposiotry
    {
        private List<Employee> employees = new();
        private readonly double[] Salary = { 1000.10, 1500.15, 2000.20, 2500.25, 3000.30, 3500.35, 4000.40, 4500.45, 5000.50, 5500.55, 6000.60 };
        private readonly string[] Names = { "Venkat", "Krishna", "Test", "Nani", "Ting", "DIng", "Ping", "Pong", "John", "Jake", "Fusk" };
        private readonly string[] Dept = { "PE", "TS", "DS", "ES", "CS", "MS", "AD", "FS", "SS", "VS", "HR" };
        private readonly Random rnd = new();

        public List<Employee> GetEmploye(int Id)
        {
            if (employees.Count > 0)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));
                return employees.Where(x => x.EmppId == Id).ToList<Employee>();
            }
            else
                return employees;
        }

        public List<Employee> GetEmployeDetails()
        {
      
            employees.Add(new Employee() { EmppId = employees.Count + 1, EmpName = Names[rnd.Next(11)], EmpSalary = Salary[rnd.Next(11)], EmpDept = Dept[rnd.Next(11)] });
            return employees;
        }
    }
}
