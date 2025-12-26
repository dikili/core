import { Component, OnInit, TemplateRef } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';
import { stringify } from 'querystring';
import { Message } from '../_models/message';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  photoUrl: string;
  messages: Message[];
  pagination: Pagination;
  messageContainer: 'Unread';
  isUnRead: boolean;
  modalRef: BsModalRef;
  registerMode= false;


  constructor(public authService: AuthService, private alertify: AlertifyService, private userService: UserService,
      private router: Router, private modalService: BsModalService) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
    this.userService.isUnread.subscribe(isUnRead => this.isUnRead = isUnRead);
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  registerToggle() {
    this.registerMode = true;
  }
  cancelRegister(cancelFlag: boolean) {
    this.registerMode = cancelFlag;
    this.modalRef.hide();
   }
  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in successfully');
     this.userService.areThereUnReadMessages(this.authService.currentUser.id);
      console.log(this.isUnRead);
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
  }



  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
