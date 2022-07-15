using System;
using System.Collections.Generic;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager ahmet = new Manager { Name = "Ahmet", Salary=8000 };
            Manager salih = new Manager { Name = "Salih", Salary=900 };

            Worker derin = new Worker { Name = "Derin", Salary = 800 };
            Worker ali = new Worker { Name = "Ali", Salary = 1000 };

            ahmet.Subordinates.Add(salih);
            salih.Subordinates.Add(derin);
            salih.Subordinates.Add(ali);

            OrgazinationalStructure orgazinationalStructure = new OrgazinationalStructure(ahmet);
            PayrollVisitor payrollVisitor = new PayrollVisitor();
            PayriseVisitor payriseVisitor = new PayriseVisitor();

            orgazinationalStructure.Accept(payrollVisitor);
            orgazinationalStructure.Accept(payriseVisitor);

            Console.ReadLine();
        }
    }
    class OrgazinationalStructure
    {
        public EmployeeBase Employee;
        public OrgazinationalStructure(EmployeeBase firstloyeeBase)
        {
            Employee = firstloyeeBase;
        }
        public void Accept(VisitorBase visitor)
        {
            Employee.Accept(visitor);
        }
    }
    abstract class EmployeeBase
    {
        public abstract void Accept(VisitorBase visitor);
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
    class Manager : EmployeeBase
    {
        public Manager()
        {
            Subordinates = new List<EmployeeBase>();
        }
        public List<EmployeeBase> Subordinates { get; set; }
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
            foreach (var employee in Subordinates)
            {
                employee.Accept(visitor);
            }
        }
    }
    class Worker : EmployeeBase
    {
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
    abstract class VisitorBase
    {
        public abstract void Visit(Worker worker);
        public abstract void Visit(Manager manager);
    }
    class PayrollVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} paid {1}", worker.Name, worker.Salary);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} paid {1}", manager.Name, manager.Salary);
        }
    }
    class PayriseVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased to {1}", worker.Name, worker.Salary * (decimal)1.1);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} salary increased to {1}", manager.Name, manager.Salary * (decimal)1.2);
        }
    }
}
