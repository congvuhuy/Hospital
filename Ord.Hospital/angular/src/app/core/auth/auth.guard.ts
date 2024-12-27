import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor( private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('access_token');
    if (!token) {
      this.router.navigate(['Account/login']);
      return false;
    }

    const payload = this.parseJwt(token);
    const requiredRole = 'admin'

    if (payload && payload.role && payload.role.includes(requiredRole)) {
      return true;
    } else {
      this.router.navigate(['account/login']);
      return false;
    }
  }

  private parseJwt(token: string) {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      return JSON.parse(atob(base64));
    } catch (error) {
      return null;
    }
  }
}
