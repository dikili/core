import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { New } from "../_models/new";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class NewsService {
  constructor(private http: HttpClient) {}

  getNews(): Observable<New> {
    return this.http.get<New>(
      "https://newsapi.org/v2/everything?q=business&apiKey=8b5df5fd3a5245c9af5c2fabcc9a6e96"
    );
  }

  getLocalNews(): Observable<New> {
    // tslint:disable-next-line:max-line-length
    return this.http.get<New>(
      "https://newsapi.org/v2/everything?q=canary%20wharf&language=en&sortBy=publishedAt&apiKey=8b5df5fd3a5245c9af5c2fabcc9a6e96"
    );
  }

  getCustomNews(phrase): Observable<New> {
    phrase = phrase.toLowerCase().trim().replace(" ", "%20");
    return this.http.get<New>(
      "https://newsapi.org/v2/everything?q=" +
        phrase +
        "&language=en&apiKey=8b5df5fd3a5245c9af5c2fabcc9a6e96"
    );
  }
}
