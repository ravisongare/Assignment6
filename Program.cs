
using System;

using System.Collections;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



public class Program
{
    List <Employee>employeeList;
    List <Salary>salaryList;

    public Program()
    {
        employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();
        Console.ReadLine();
    }

    public void Task1()
    {
        var query = salaryList.GroupBy(s => s.EmployeeID).Join(employeeList, s => s.Key, e => e.EmployeeID, (s, e) =>
        new {

                                         name = e.EmployeeFirstName ,

                                         salary = TotalSalary(s)

                                     })

                                     .OrderBy(s => s.salary);



        Console.WriteLine("Total Salary of all the employee with their corresponding names......");

        foreach (var item in query)

        {

            Console.WriteLine("Name :" +item.name+ "\nTotal Salary :" +item.salary+"\n");

        }
    }

    public int TotalSalary(IGrouping<int, Salary> s)

    {



        int sum = 0;

        foreach (var t in s)

        {

            sum += t.Amount;

        }

        return sum;

    }


    public void Task2()
    {
        //Implementation
        var query = salaryList.GroupBy(s => s.EmployeeID).Join(employeeList, s => s.Key, e => e.EmployeeID, (s, e) =>
      new
      {

          Firstname = e.EmployeeFirstName,
          LastName = e.EmployeeLastName,
          Age = e.Age,
          salary = TotalSalary(s),
          ID=e.EmployeeID

      })
       .OrderByDescending(e => e.Age).Skip(1).Take(1);
        Console.WriteLine("Details of 2nd oldest Employee : ");
        foreach(var temp in query)
            Console.WriteLine("\nEmpID : " + temp.ID+"\nFirst name : " +temp.Firstname+"\n Last name : " +temp.LastName+"\nTotal salary: "+temp.salary+"\n Age : "+temp.Age );


    }

    public void Task3()
    {
        //Implementation


        var query = employeeList.Where(e => e.Age > 30)

            .Join(salaryList, e => e.EmployeeID, s => s.EmployeeID, (e, s) => new {  s.Amount, s.Type });

        var MonthlyQuery = query.Where(q => q.Type==SalaryType.Monthly)

                                .Select(q => q.Amount);



        Console.WriteLine("\nMean of monthly salary of employee whose age is greater than 30 year : "+ MonthlyQuery.Average());



        var PerfomanceQuery = query.Where(q => q.Type==SalaryType.Performance)

                                    .Select(q => q.Amount);



        Console.WriteLine("\nMean of perfomance salary of employee  whose age is greaters than 30 year : " +PerfomanceQuery.Average());



        var BonusQuery = query.Where(q => q.Type==SalaryType.Bonus)

                                .Select(q => q.Amount);



        Console.WriteLine("\nMean of bonus salary of employee  whose age is greater than 30 year : "+ BonusQuery.Average());
    }
}

public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}

public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}