import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
// HttpClient makes below obsolete
// import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { User } from '../_models/User';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { PaginatedResult } from '../_models/Pagination';
import { query } from '@angular/core/src/animation/dsl';
import { Message } from '../_models/message';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable()
export class UserService {
 baseUrl = environment.apiUrl;

// if http were used instead of angular 2 jwt lib. no need to send the token now
constructor(private http: HttpClient) { }
// HttpClient made below obsolete now..
// getUsers(page?: number, itemsPerPage?: number, userParams?: any, likeParams?: any) {
//   const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
//   let queryString = '?';

//   if (page != null && itemsPerPage != null) {
//       queryString += 'pageNumber=' +
//        page + '&pageSize=' +
//        itemsPerPage + '&';
//   }

// if (userParams != null) {
//     queryString += 'minAge=' + userParams.minAge +
//     '&maxAge=' + userParams.maxAge +
//     '&gender=' + userParams.gender +
//     '&orderBy=' + userParams.orderBy;
// }
// if (likeParams === 'Likers') {
//     queryString += 'Likers=true&';
// }

// if (likeParams === 'Likees') {
//     queryString += 'Likees=true&';
// }
//   return this.http.get(this.baseUrl + 'users' + queryString, this.jwt())
//   .map(response => {
//       paginatedResult.result = response.json();
//       if (response.headers.get('Pagination') != null) {
//        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
//       }
//       return paginatedResult;
//   })
//   .catch(this.handleError);
// }

getUsers(page?, itemsPerPage?, userParams?: any, likeParams?: any) {
  const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
  let params = new HttpParams();

  if (page != null && itemsPerPage != null) {
     params = params.append('pageNumber', page);
     params = params.append('pageSize', itemsPerPage);
  }

if (userParams != null) {
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
}
if (likeParams === 'Likers') {
    params = params.append('Likers', 'true');
}

if (likeParams === 'Likees') {
    params = params.append('Likees', 'true');
}
  return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params })
  .map(response => {
      paginatedResult.result = response.body;
      if (response.headers.get('Pagination') != null) {
       paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }
      return paginatedResult;
  });
}




sendLike(id: number, receipentId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/like/' + receipentId, {});
}
// HttpClien update made this obsolete
// getMessages(id: number, page?: number, itemsPerPage?: number, messageContainer?: string) {
// const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();

// let queryString = '?MessageContainer=' + messageContainer;

// if (page != null && itemsPerPage != null) {
// queryString += '&pageNumber=' + page + '&pageSize=' + itemsPerPage;
// }

// return this.http.get(this.baseUrl + 'users/' + id + '/messages' + queryString, this.jwt())
// .map(resp => {
// paginatedResult.result = resp.json();
// if (resp.headers.get('Pagination') != null) {
// paginatedResult.pagination = JSON.parse(resp.headers.get('Pagination'));
// }

// return paginatedResult;
// }).catch(this.handleError);

// }

// HttpClient Update made below obsolete
// getMessageThread(id: number, receiverId: number) {
//    return this.http.get(this.baseUrl + 'users/' + id + '/messages/thread/' + receiverId, this.jwt()).map((response: Response) => {
//         return response.json();
//     }).catch(this.handleError);
// }


getMessages(id: number, page?, itemsPerPage?, messageContainer?: string) {
const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();

let params = new HttpParams();

params.append('MessageContainer', messageContainer);

if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
}

return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/messages', { observe: 'response', params})
.map(resp => {
paginatedResult.result = resp.body;
if (resp.headers.get('Pagination') != null) {
paginatedResult.pagination = JSON.parse(resp.headers.get('Pagination'));
}

return paginatedResult;
});

}


getMessageThread(id: number, receiverId: number) {
    return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/messages/thread/' + receiverId);
 }
// HttpClient Update made below obsolete
// sendMessage(id: number, message: Message) {
//     return this.http.post(this.baseUrl + 'users/' + id + '/messages', message, this.jwt()).map((response: Response) => {
// return response.json();
//     }).catch(this.handleError);
// }

sendMessage(id: number, message: Message) {
    return this.http.post<Message>(this.baseUrl + 'users/' + id + '/messages', message);
}

deleteMessage(id: number, userId: number) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + id, {}).map(response => {});
}

markAsRead(userId: number, messageId: number) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/messages/' + messageId + '/read', {} ).subscribe();
}
// HttpClient Update made below obsolete
// getUser(id): Observable<User> {
//     return this.http.get(this.baseUrl + 'users/' + id, this.jwt())
//     .map((response: Response) => <User>response.json())
//     .catch(this.handleError);
// }

getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
}

updateUser(id: number, user: User) {
   return this.http.put(this.baseUrl + 'users/' + id, user);

}

setMainPhoto(userid: number, id: number) {
    console.log('set main photo called and id is ', id);
    return this.http.post(this.baseUrl + 'users/' + userid + '/photos/' + id + '/setMain', {});
}

deletePhoto(userid: number, id: number) {
  console.log('delete photo function is called and id id ', id);
  return this.http.delete(this.baseUrl + 'users/' + userid + '/photos/' + id);
}
// constructor(private authHttp: AuthHttp) { }

// getUsers(): Observable<User[]> {
//   return this.authHttp.get(this.baseUrl + 'users')
//   .map(response => <User[]>response.json())
//   .catch(this.handleError);
// }

// jwt() {
//  const token = localStorage.getItem('token');

//  if (token) {
//   const headers = new Headers();
//   headers.append('Authorization', 'Bearer ' + token);
//   headers.append('Content-type', 'application/json');

//   const options = new RequestOptions();
//   options.headers = headers;
//   return options;
//  }


//  }

// private handleError(error: any) {
//     if (error.status === 400) {
//      return Observable.throw(error._body);
//     }
//     const applicationError = error.headers.get('Application-Error');
//     if (applicationError) {
//       return Observable.throw(applicationError);
//     }
//     const serverError = error.json();
//     let modelStateErrors = '';
//     if (serverError) {
//         for (const key in serverError) {
//             if (serverError[key]) {
//                 modelStateErrors += serverError[key] + '\n';
//             }
//         }
//     }

// }


}
