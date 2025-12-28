import { Component } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  constructor(public authService: AuthService) {}

  get displayName(): string {
    return (this.authService.decodedToken && (this.authService.decodedToken.unique_name || this.authService.decodedToken.name)) || 'Member';
  }
}
