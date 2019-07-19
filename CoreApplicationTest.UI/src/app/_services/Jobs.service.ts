import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Job } from '../_models/job';

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  baseUrl = environment.apiUrl;
constructor(private http: HttpClient) { }

getJobs() {
  return this.http.get<Job[]>(this.baseUrl  + 'jobs');
}

}
