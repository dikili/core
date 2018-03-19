import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router/src/directives/router_link';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any= {};

  constructor(public authService: AuthService, private alertifyService: AlertifyService, private routerService: Router) { }

  ngOnInit() {
  }

  login() {
    // console.log(this.model);

    this.authService.login(this.model).subscribe(data => {
      // console.log('logged in successfully');
      this.alertifyService.success('logged in successfully');
    }, error => {
    //  console.log(error);
    this.alertifyService.error('Failed to login');
    }, () => {
       this.routerService.navigate(['/members']);
   });
  }
  logout() {
    this.authService.userToken = null;
    this.authService.currentUser = null;
    localStorage.removeItem('token');
    this.alertifyService.message('logged out');
    this.authService.logout();
    this.routerService.navigate(['/home']);
  }

  loggedIn() {
    // changing this implementation to use angular2-jwt
    // const token = localStorage.getItem('token');
    // return !!token;

    return this.authService.loggedIn();

  }


}
