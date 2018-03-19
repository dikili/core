import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../_models/User';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { UserService } from '../../_services/user.service';
import { AuthService } from '../../_services/auth.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
user: User;
@ViewChild('editForm') editForm: NgForm;
  constructor(private route: ActivatedRoute,
     private alertify: AlertifyService,
    private userService: UserService,
  private authService: AuthService) { }

  ngOnInit() {

    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
  }

  updateUser() {
    // console.log(this.user);
    // passing user.id does the job but just in case , we are going to get the id from
    // the decoded token by using authservice
    // this.userService.updateUser(this.user.id, this.user).subscribe(next => {
    //   this.alertify.success('updated the user');
    //   this.editForm.reset(this.user);
    // }, error => {
    //   this.alertify.error(error);
    // });


    this.userService.updateUser(this.authService.decodedToken.nameid, this.user).subscribe(next => {
      this.alertify.success('updated the user');
      this.editForm.reset(this.user);
    }, error => {
      this.alertify.error(error);
    });
  }

  updateParentCompPhoto (photoUrl) {
    this.user.photoUrl = photoUrl;
  }

}
