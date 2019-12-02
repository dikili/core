import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Event } from '../_models/event';


@Injectable({
  providedIn: 'root'
})
export class EventService {

  baseUrl = environment.apiUrl;
constructor(private http: HttpClient) { }

getEvents() {
  return this.http.get<Event[]>(this.baseUrl  + 'events');
}

}
