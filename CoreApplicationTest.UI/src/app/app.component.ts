import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelper } from 'angular2-jwt';
import { User } from './_models/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
 JwtHelper: JwtHelper = new JwtHelper();
 /**
  *
  */
 constructor(private authService: AuthService) {
 }

 ngOnInit() {
   // make sure you assign something to decodedToken to make the variable available
   // even after page refresh globally ...
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.JwtHelper.decodeToken(token);
    }

    if (user) {
      this.authService.currentUser = user;
      if (this.authService.currentUser.photoUrl !== null) {
        this.authService.changeMemberPhoto(user.photoUrl);
      } else {
        this.authService.changeMemberPhoto('../assets/user.png');
      }
    }
 }
}
