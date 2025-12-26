import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { NewsService } from '../_services/news.service';
import { New } from '../_models/new';
import { Observable } from 'rxjs';
import { Article } from '../_models/Article';


@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {
  news: New;
  newsphrase: string;
  isWarning = true;
  isAnotherWarning = true;
  articles: Article[];
  constructor(private newsService: NewsService,
    private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.getLocalNews();
  }

  onChange(source) {

  if (source === 'Local') {this.getLocalNews(); }
  if (source === 'Global') {this.getNews(); }
  }

  getNews() {
    this.newsService
    .getNews()
     .subscribe((news) => {
      console.log('global news');
      this.news = news;
       console.log(this.articles);
     }, error => {
       this.alertifyService.error(error);
     });
  }

  getLocalNews() {
    this.newsService
    .getLocalNews()
     .subscribe((news) => {
       console.log('local news');
       this.news = news;
       this.articles = this.news.articles;
       console.log(this.articles);
     }, error => {
       this.alertifyService.error(error);
     });
  }

  getCustomNews(newsPhrase) {
    this.newsphrase = newsPhrase;
    this.newsService
    .getCustomNews(this.newsphrase)
     .subscribe((news) => {
       console.log('local news');
       this.news = news;
       console.log(this.articles);
     }, error => {
       this.alertifyService.error(error);
     });
  }

  toggleWarning() {
    console.log('clicked x');
    console.log(this.isWarning);
    this.isWarning = false;
       return this.isWarning;
  }
  toggleAnotherWarning() {
    console.log('clicked x');
    console.log(this.isAnotherWarning);
    this.isAnotherWarning = false;
       return this.isAnotherWarning;
  }
}
