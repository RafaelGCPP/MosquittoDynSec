import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class IsAuthenticatedGuard implements CanActivate {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {

    if (this.authService.isAuthenticated()) return true;

    var returnUrl = encodeURI('~' + state.url);
    this.authService.login(returnUrl);

    return false;

  }
}
