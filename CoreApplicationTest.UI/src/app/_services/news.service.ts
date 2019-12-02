import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { New } from '../_models/new';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  baseUrl = environment.apiUrl;
constructor(private http: HttpClient) { }

getNews(): Observable<New> {
// return this.http.get<New>('https://newsapi.org/v2/top-headlines?country=gb&category=business&apiKey=8b5df5fd3a5245c9af5c2fabcc9a6e96');
return this.http.get<New>(this.baseUrl  + 'news');
}

getLocalNews(): Observable<New> {
  // tslint:disable-next-line:max-line-length
 // return this.http.get<New>('https://newsapi.org/v2/everything?q=canary%20wharf&language=en&sortBy=publishedAt&apiKey=8b5df5fd3a5245c9af5c2fabcc9a6e96');
   return this.http.get<New>(this.baseUrl + 'localnews');
}

getCustomNews(phrase): Observable<New> {
  // tslint:disable-next-line:no-debugger
  debugger;
 phrase = phrase.toLowerCase().trim();
 phrase = phrase.split(' ').join('+');
 // return this.http.get<New>('https://newsapi.org/v2/everything?q=' + phrase + '&language=en&apiKey=8b5df5fd3a5245c9af5c2fabcc9a6e96');
  return this.http.get<New>(this.baseUrl + 'customnews?phrase=' + phrase);

}

}
