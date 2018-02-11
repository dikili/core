import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router, CanActivate , ActivatedRouteSnapshot , RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {
/**
 *
 */
constructor(private authService: AuthService, private alertifyService: AlertifyService , private routeService: Router ) {

}

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.loggedIn()) {
      return true;
    }

   this.alertifyService.error('You need to be logged in to access this area');
   this.routeService.navigate(['/home']);
   return false;
  }
}
