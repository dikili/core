import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../_models/User';
import { AuthService } from '../../_services/auth.service';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
 @Input() user: User;

  constructor(private authService: AuthService,
     private userService: UserService,
     private alertifyService: AlertifyService) { }

  ngOnInit() {
  }


  sendLike2(id: number, receipentId: number) {
this.userService.sendLike(id, receipentId).subscribe(resp => {
 console.log('user with id: ' + receipentId + 'is liked by : ' + id);
 this.alertifyService.success('user with id: ' + receipentId + 'is liked by : ' + id);
}, error => this.alertifyService.error(error));
  }

  sendLike(id: number) {
    this.userService.sendLike(this.authService.decodedToken.nameid, id).subscribe(resp => {
     this.alertifyService.success('you have liked ' + this.user.knownAs);
    }, error => this.alertifyService.error(error));
      }

}
