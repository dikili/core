import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Http, RequestOptions, Headers } from '@angular/http';
import { User } from '../_models/User';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

@Injectable()
export class UserService {
 baseUrl = environment.apiUrl;

// if http were used instead of angular 2 jwt lib. no need to send the token now
constructor(private http: Http) { }

getUsers(): Observable<User[]> {
  return this.http.get(this.baseUrl + 'users', this.jwt()).map(response => <User[]>response.json())
  .catch(this.handleError);
}


getUser(id): Observable<User> {
    return this.http.get(this.baseUrl + 'users/' + id, this.jwt())
    .map(response => <User>response.json())
    .catch(this.handleError);
}

// constructor(private authHttp: AuthHttp) { }

// getUsers(): Observable<User[]> {
//   return this.authHttp.get(this.baseUrl + 'users')
//   .map(response => <User[]>response.json())
//   .catch(this.handleError);
// }

jwt() {
 const token = localStorage.getItem('token');

 if (token) {
  const headers = new Headers();
  headers.append('Authorization', 'Bearer ' + token);
  headers.append('Content-type', 'application/json');

  const options = new RequestOptions();
  options.headers = headers;
  return options;
 }


 }

private handleError(error: any) {
    const applicationError = error.headers.get('Application-Error');
    if (applicationError) {
      return Observable.throw(applicationError);
    }
    const serverError = error.json();
    let modelStateErrors = '';
    if (serverError) {
        for (const key in serverError) {
            if (serverError[key]) {
                modelStateErrors += serverError[key] + '\n';
            }
        }
    }

}


}
