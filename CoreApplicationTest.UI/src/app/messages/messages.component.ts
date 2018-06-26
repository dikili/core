import { Component, OnInit } from '@angular/core';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Message } from '../_models/message';
import { Pagination, PaginatedResult } from '../_models/Pagination';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
messages: Message[];
pagination: Pagination;
messageContainer: 'Unread';

  constructor(private userService: UserService,
     private alertifyService: AlertifyService,
    private route: ActivatedRoute,
  private authService: AuthService) { }

  ngOnInit() {
   // this.loadUsers();
   this.route.data.subscribe(data => {
this.messages = data['messages'].result;
this.pagination = data['messages'].pagination;
   });
  }

  loadMessages() {
    this.userService
    .getMessages(this.authService.decodedToken.nameid,
      this.pagination.currentPage,
      this.pagination.itemsPerPage,
      this.messageContainer)
     .subscribe((res: PaginatedResult<Message[]>) => {
       this.messages = res.result;
       this.pagination = res.pagination;
     }, error => {
       this.alertifyService.error(error);
     });
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
       }
  // loadUsers() {
  //   this.userService.getUsers().subscribe((users: User[]) => {
  //     this.users = users;
  //   }, error => this.alertifyService.error(error));
  // }
}
