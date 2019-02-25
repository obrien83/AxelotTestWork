using System;
using System.Collections.Generic;

namespace AxelotTestWork.Application.Models
{
    public class Employees
    {
        public Employees()
        {
            Customers = new HashSet<Customers>();
            InverseReportsToNavigation = new HashSet<Employees>();
        }

        public long EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public long? ReportsTo { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public virtual Employees ReportsToNavigation { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Employees> InverseReportsToNavigation { get; set; }
    }
}
