import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any= {};

  constructor(private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    // console.log(this.model);

    this.authService.login(this.model).subscribe(data => {
      // console.log('logged in successfully');
      this.alertifyService.success('logged in successfully');
    }, error => {
    //  console.log(error);
    this.alertifyService.error(error);
    });
  }

  logout() {
    this.alertifyService.message('logged out');
    this.authService.logout();

  }

  loggedIn() {
    // changing this implementation to use angular2-jwt
    // const token = localStorage.getItem('token');
    // return !!token;

    return this.authService.loggedIn();

  }


}
