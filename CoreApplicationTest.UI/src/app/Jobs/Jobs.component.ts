import { Component, OnInit } from "@angular/core";
import { AlertifyService } from "../_services/alertify.service";
import { JobsService } from "../_services/Jobs.service";
import { Job } from "../_models/job";
import { forEach } from "@angular/router/src/utils/collection";
import { Jobswithdate } from "../_models/jobswithdate";
import { PageChangedEvent } from "ngx-bootstrap";

@Component({
  selector: "app-jobs",
  templateUrl: "./Jobs.component.html",
  styleUrls: ["./Jobs.component.css"],
})
export class JobsComponent implements OnInit {
  jobs: Job[];
  returnedJobs: Job[] = [];
  itemsPerPage: number = 10;
  totalItems: number;
  isWarning = true;
  constructor(
    private alertifyService: AlertifyService,
    private jobsService: JobsService
  ) {}

  ngOnInit() {
    this.getJobs();
    this.returnedJobs = this.jobs.slice(0, 10);
  }

  getJobs() {
    this.jobsService.getJobs().subscribe(
      (resp) => {
        this.jobs = resp;

        this.totalItems = this.jobs ? this.jobs.length : 0;
        this.returnedJobs = this.jobs.slice(0, this.itemsPerPage);
        // for (let i = 0, len = this.jobs.length; i < len; i++) {
        //   // tslint:disable-next-line:max-line-length
        //   console.log(+this.jobs[i].date.substring(6, 10), +this.jobs[i].date.substring(3, 5), +this.jobs[i].date.substring(0, 2));
        // tslint:disable-next-line:max-line-length
        //   this.jobswithDate[i].date = new Date(+this.jobs[i].date.substring(6, 10), +this.jobs[i].date.substring(3, 5), +this.jobs[i].date.substring(0, 2));
        //   this.jobswithDate[i].jobDescription = this.jobs[i].jobDescription;
        //   this.jobswithDate[i].applications = this.jobs[i].applications;
        //   this.jobswithDate[i].employerName = this.jobs[i].employerName;
        //   this.jobswithDate[i].expirationDate = this.jobs[i].expirationDate;
        //   this.jobswithDate[i].jobTitle = this.jobs[i].jobTitle;
        //   this.jobswithDate[i].jobUrl = this.jobs[i].jobUrl;
        //   this.jobswithDate[i].locationName = this.jobs[i].locationName;
        // }
        this.sortBy("date");
      },
      (error) => this.alertifyService.error(error)
    );
  }
  toggleWarning() {
    console.log("clicked x");
    console.log(this.isWarning);
    this.isWarning = false;
    return this.isWarning;
  }

  sortBy(field: string) {
    // change ascending or descending order of the jobs by -1 and 1
    this.jobs.sort((a: any, b: any) => {
      if (a[field] < b[field]) {
        return 1;
      } else if (a[field] > b[field]) {
        return -1;
      } else {
        return 0;
      }
    });
    this.jobs = this.jobs;
  }
  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.returnedJobs = this.jobs.slice(startItem, endItem);
  }
}
