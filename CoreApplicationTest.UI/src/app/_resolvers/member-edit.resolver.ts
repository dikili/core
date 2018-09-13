import { Resolve } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRouteSnapshot } from '@angular/router/src/router_state';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';


@Injectable()
export class MemberEditResolver implements Resolve<User> {

/**
 *
 */
constructor(private userService: UserService,  private alertify: AlertifyService , private authService: AuthService) {

}

resolve(route: ActivatedRouteSnapshot): Observable<User>  {
  // tslint:disable-next-line:no-debugger
  debugger;
  return this.userService.getUser(this.authService.decodedToken.nameid)
        .catch(error => {
      console.log(this.authService.decodedToken.nameid);
      this.alertify.error('Problem retrieving data 3');
      return Observable.of(null);
  });
}
}
