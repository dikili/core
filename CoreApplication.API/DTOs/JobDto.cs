using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.DTOs
{
 
    public class JobDto
    {
        public string EmployerName { get; set; }

        public string JobTitle { get; set; }

        public string LocationName { get; set; }

        public int Applications { get; set; }

        public string JobDescription { get; set; }

        public string ExpirationDate { get; set; }

        public string Date { get; set; }

        public string JobUrl { get; set; }
    }

    public class JobwithDate
    {
        public string EmployerName { get; set; }

        public string JobTitle { get; set; }

        public string LocationName { get; set; }

        public int Applications { get; set; }

        public string JobDescription { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime Date { get; set; }

        public string JobUrl { get; set; }
    }
}
