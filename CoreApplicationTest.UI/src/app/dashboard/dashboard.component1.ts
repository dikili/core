import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  displayName: string;

  constructor(public authService: AuthService) {}

  ngOnInit() {
    const u: any = this.authService.currentUser;
    this.displayName = (u && (u.knownAs || u.username)) ? (u.knownAs || u.username) : 'member';
  }
}
