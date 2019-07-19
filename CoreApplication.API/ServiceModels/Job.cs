using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.API.ServiceModels
{
    /// <summary>
    ///            "jobId": 38201243,
    //"employerId": 610943,
    //"employerName": "Kingsley Hamilton Estates",
    //"employerProfileId": null,
    //"employerProfileName": null,
    //"jobTitle": "Property Manager",
    //"locationName": "Canary Wharf",
    //"minimumSalary": null,
    //"maximumSalary": null,
    //"currency": null,
    //"expirationDate": "24/07/2019",
    //"date": "12/06/2019",
    //"jobDescription": " Kingsley Hamilton Estates is an established company with departments covering lettings, new homes, sales and property management. We currently have branches in One Canada Square, Canary Wharf and in Beijing, China. The successful candidate will be based  in our Canary Wharf office. This is a great opportunity for a motivated and experienced Property Manager to work with a boutique agency who pride themselves in providing not only a fi... ",
    //"applications": 4,
    //"jobUrl": "https://www.reed.co.uk/jobs/property-manager/38201243"
    /// </summary>
    /// 
    public class JobList
    {
        public List<Job> Results { get; set; }
    }
    public class Job
    {
        public int JobId { get; set; }
        public int EmployerId { get; set; }

        public string EmployerName { get; set; }

        public int? EmployerProfileId { get; set; }

        public string EmployerProfileName { get; set; }

        public string JobTitle { get; set; }

        public string MinimumSalary { get; set; }

        public string MaximumSalary { get; set; }
        public string Currency { get; set; }
        public string LocationName { get; set; }

        public int Applications { get; set; }
        public string JobDescription { get; set; }
        public string JobUrl { get; set; }

        public string ExpirationDate { get; set; }

        public string Date { get; set; }
    }
}
