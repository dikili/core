import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { User } from '../_models/User';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { PaginatedResult } from '../_models/Pagination';
import { query } from '@angular/core/src/animation/dsl';
import { Message } from '../_models/message';

@Injectable()
export class UserService {
 baseUrl = environment.apiUrl;

// if http were used instead of angular 2 jwt lib. no need to send the token now
constructor(private http: Http) { }

getUsers(page?: number, itemsPerPage?: number, userParams?: any, likeParams?: any) {
  const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
  let queryString = '?';

  if (page != null && itemsPerPage != null) {
      queryString += 'pageNumber=' +
       page + '&pageSize=' +
       itemsPerPage + '&';
  }

if (userParams != null) {
    queryString += 'minAge=' + userParams.minAge +
    '&maxAge=' + userParams.maxAge +
    '&gender=' + userParams.gender +
    '&orderBy=' + userParams.orderBy;
}
if (likeParams === 'Likers') {
    queryString += 'Likers=true&';
}

if (likeParams === 'Likees') {
    queryString += 'Likees=true&';
}
  return this.http.get(this.baseUrl + 'users' + queryString, this.jwt())
  .map(response => {
      paginatedResult.result = response.json();
      if (response.headers.get('Pagination') != null) {
       paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
  })
  .catch(this.handleError);
}

sendLike(id: number, receipentId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/like/' + receipentId, {}, this.jwt()).catch(this.handleError);
}

getMessages(id: number, page?: number, itemsPerPage?: number, messageContainer?: string) {
const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();

let queryString = '?MessageContainer=' + messageContainer;

if (page != null && itemsPerPage != null) {
queryString += '&pageNumber=' + page + '&pageSize=' + itemsPerPage;
}

return this.http.get(this.baseUrl + 'users/' + id + '/messages' + queryString, this.jwt())
.map(resp => {
paginatedResult.result = resp.json();
if (resp.headers.get('Pagination') != null) {
paginatedResult.pagination = JSON.parse(resp.headers.get('Pagination'));
}

return paginatedResult;
}).catch(this.handleError);

}


getMessageThread(id: number, receiverId: number) {
   return this.http.get(this.baseUrl + 'users/' + id + '/messages/thread/' + receiverId, this.jwt()).map((response: Response) => {
        return response.json();
    }).catch(this.handleError);
}

sendMessage(id: number, message: Message) {
    return this.http.post(this.baseUrl + 'users/' + id + '/messages', message, this.jwt()).map((response: Response) => {
return response.json();
    }).catch(this.handleError);
}

deleteMessage(id: number, userId: number) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + id, {}, this.jwt()).map(response => {}).catch(this.handleError);
}

getUser(id): Observable<User> {
    return this.http.get(this.baseUrl + 'users/' + id, this.jwt())
    .map((response: Response) => <User>response.json())
    .catch(this.handleError);
}

updateUser(id: number, user: User) {
   return this.http.put(this.baseUrl + 'users/' + id, user, this.jwt())
    .catch(this.handleError);

}

setMainPhoto(userid: number, id: number) {
    console.log('set main photo called and id is ', id);
    return this.http.post(this.baseUrl + 'users/' + userid + '/photos/' + id + '/setMain', {}, this.jwt()).catch(this.handleError);
}

deletePhoto(userid: number, id: number) {
  console.log('delete photo function is called and id id ', id);
  return this.http.delete(this.baseUrl + 'users/' + userid + '/photos/' + id, this.jwt()).catch(this.handleError);
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
    if (error.status === 400) {
     return Observable.throw(error._body);
    }
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
