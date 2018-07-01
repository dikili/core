import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/User';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from '../../_models/Pagination';



@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];
  user: User = JSON.parse(localStorage.getItem('user'));
  genderList = [{value: 'male' , display: 'Males'}, {value: 'female', display: 'Females'}];
  userParams: any = {};
  pagination: Pagination;

  constructor(private userService: UserService, private alertifyService: AlertifyService, private router: ActivatedRoute) { }

  ngOnInit() {
  //  this.loadUsers();
  //  this.route.data.subscribe(data => {
  //    this.users = data['users'];
  //  });

  this.router.data.subscribe(data => {
    this.users = data['users'].result;
    this.pagination = data['users'].pagination;
  });

this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
this.userParams.minAge = 18;
this.userParams.maxAge = 99;
this.userParams.orderBy = 'lastActive';


  }
loadUsers() {
  this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
  .subscribe((res: PaginatedResult<User[]>) => {
this.pagination.currentPage = res.pagination.currentPage;
this.users = res.result;
  }, error => {
    this.alertifyService.error(error);
  });
}

loadUsers2() {
  this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
  .subscribe((res: PaginatedResult<User[]>) => {
this.pagination.currentPage = res.pagination.currentPage;
this.users = res.result;
  }, error => {
    this.alertifyService.error(error);
  });
}

resetFilters() {
  this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
this.userParams.minAge = 18;
this.userParams.maxAge = 99;
this.loadUsers();
}
  pageChanged(event: any) {
    console.log('page changed to : ' + event.page);
    console.log('items on the page: ' + event.itemsPerPage);
    this.pagination.currentPage = event.page;
    this.loadUsers();
  }
  // commented out to make sure users are loaded before the component so
  // their properties are accessible..
  loadUsersbeforeusingresolvers() {
    this.userService.getUsers().subscribe((users: User[]) => {
      this.users = users;
    }, error => this.alertifyService.error(error));
  }
}






