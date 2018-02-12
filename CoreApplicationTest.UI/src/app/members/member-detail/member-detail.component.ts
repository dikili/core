import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../_models/User';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User;
  constructor(private userService: UserService, private alertifier: AlertifyService, private router: ActivatedRoute) { }

  ngOnInit() {
   // this.loadUser();
   this.router.data.subscribe(data => {
     this.user = data['user'];
   });
  }

  // loadUser() {
  //  this.userService.getUser(+this.router.snapshot.params['id']).subscribe((user: User) => {
  //    this.user = user;
  //  }, error => this.alertifier.error(error));
  // }
}
