import { Component, OnInit } from '@angular/core';
import { EventService } from '../_services/event.service';
import { AlertifyService } from '../_services/alertify.service';
import { Event } from '../_models/event';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  events: Event[];

  constructor(private alertifyService: AlertifyService,
    private eventService: EventService) { }

  ngOnInit() {
    this.getEvents();
  }
  getEvents() {

    this.eventService.getEvents().subscribe(resp => {
      this.events = resp;
   console.log(this.events);
     }, error => this.alertifyService.error(error));
  }
}
