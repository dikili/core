import { Resolve, Router } from '@angular/router';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActivatedRouteSnapshot } from '@angular/router/src/router_state';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';


@Injectable()
export class ListsResolver implements Resolve<User[]> {
pageSize = 5;
pageNumber = 1;
likeParams = 'Likers';
/**
 *
 */
constructor(private userService: UserService, private router: Router , private alertify: AlertifyService) {

}

resolve(route: ActivatedRouteSnapshot): Observable<User[]>  {
  return this.userService.getUsers(this.pageNumber, this.pageSize, null, this.likeParams)
        .catch(error => {
      this.alertify.error('Problem retrieving data');
      this.router.navigate(['/home']);
      return Observable.of(null);
  });
}
}
