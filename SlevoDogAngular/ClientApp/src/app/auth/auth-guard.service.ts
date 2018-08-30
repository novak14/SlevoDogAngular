import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

import { AuthService } from './auth.service';
import {Observable} from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.authService.isAuthenticated();
  }

  // canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
  //   return undefined;
  // }
}
