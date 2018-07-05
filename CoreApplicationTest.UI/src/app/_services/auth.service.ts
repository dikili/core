import { Injectable } from '@angular/core';
// import { Http, Headers, RequestOptions, Response } from '@angular/http'; line not needed as HttpClient is added
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs/Observable';
import { modelGroupProvider } from '@angular/forms/src/directives/ng_model_group';
// import { tokenNotExpired, JwtHelper } from 'angular2-jwt';
import { User } from '../_models/User';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthUser } from '../_models/authUser';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthService {
 baseUrl = environment.apiUrl;
 userToken: any = {};
 decodedToken: any;
//  JwtHelper: JwtHelper = new JwtHelper(); line not needed as jwtHelperService
 currentUser: User;
 private photoUrl = new BehaviorSubject<string>('../../assets/user.png');
currentPhotoUrl = this.photoUrl.asObservable();

constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

changeMemberPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
}
// before HttpClient using Http
// login(model: any) {
//    return this.http.post(this.baseUrl + 'login', model, {headers: new HttpHeaders()
//     .set('Content-Type', 'application/json')})
//     .map((response: Response) => {
//     const user = response.json();
//     if (user && user.tokenString)  {
//         localStorage.setItem('token', user.tokenString);
//         this.userToken = user.tokenString;
//         // this.decodedToken = this.JwtHelper.decodeToken(this.userToken); line not valid as jwtHelperService is below
//         this.decodedToken = this.jwtHelperService.decodeToken(this.userToken);
//         localStorage.setItem('user', JSON.stringify(user.mappedUser));
//         this.currentUser = user.mappedUser;
//         this.userToken = user.tokenString;
//         if (this.currentUser.photoUrl !== null) {
//           this.changeMemberPhoto(this.currentUser.photoUrl);
//         } else {
//           this.changeMemberPhoto('../../assets/user.png');
//         }
//         console.log(this.decodedToken);
//         console.log(user.mappedUser);
//     }
// }).catch(this.handleError);
// }

login(model: any) {
    return this.http.post<AuthUser>(this.baseUrl + 'auth/login', model, {headers: new HttpHeaders()
     .set('Content-Type', 'application/json')})
     .map(user => {
     if (user)  {
         localStorage.setItem('token', user.tokenString);
         this.userToken = user.tokenString;
         localStorage.setItem('user', JSON.stringify(user.mappedUser));
         // this.decodedToken = this.JwtHelper.decodeToken(this.userToken); line not valid as jwtHelperService is below
         this.decodedToken = this.jwtHelperService.decodeToken(this.userToken);
         this.currentUser = user.mappedUser;
         this.userToken = user.tokenString;
         if (this.currentUser.photoUrl !== null) {
           this.changeMemberPhoto(this.currentUser.photoUrl);
         } else {
           this.changeMemberPhoto('../../assets/user.png');
         }
         console.log(this.decodedToken);
         console.log(user.mappedUser);
     }
 });
 }

register(user: User) {
    // below line not needed as httpclient implementation is as one below
// return  this.http.post(this.baseUrl + 'register', user, this.getRequestOptions()).catch(this.handleError);
return  this.http.post(this.baseUrl + 'auth/register', user, {headers: new HttpHeaders()
    .set('Content-Type', 'application/json')});
}

loggedIn() {
    // return tokenNotExpired('token'); jwthelperservice implementation is below thisline not valid anymore...
    const token = this.jwtHelperService.tokenGetter();
    if (!token) {
        return false;
    }

    return !this.jwtHelperService.isTokenExpired(token);
}

logout() {
   // when the token is removed it automatically becomes expired..
    localStorage.removeItem('token');
    this.userToken = null;
    console.log('logged out');
}
// nolonger needed as HttpClient
// private getRequestOptions() {
//     const headers = new Headers({'Content-type': 'application/json'});
//    return new RequestOptions({headers: headers});
// }


}
