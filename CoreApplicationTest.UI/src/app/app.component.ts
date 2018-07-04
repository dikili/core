import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
// import { JwtHelper } from 'angular2-jwt'; no longer needed as JwtHelperService
import { User } from './_models/User';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
//  JwtHelper: JwtHelper = new JwtHelper(); line invalid as JwtHelperService in place
 /**
  *
  */
 constructor(private authService: AuthService, private jwtHelperService: JwtHelperService) {
 }

 ngOnInit() {
   // make sure you assign something to decodedToken to make the variable available
   // even after page refresh globally ...
    const token = localStorage.getItem('token');
    const userLS = localStorage.getItem('user');
    if (userLS !== 'undefined') {
      const user: User = JSON.parse(localStorage.getItem('user'));
      if (token) {
        // this.authService.decodedToken = this.JwtHelper.decodeToken(token); jwtHelperService implementation ...
        this.authService.decodedToken = this.jwtHelperService.decodeToken(token);
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
}
