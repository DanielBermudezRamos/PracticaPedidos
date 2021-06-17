import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AdminService } from '../service/admin.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AdminService, private router: Router) { }
  canActivate() {
    if (this.authService.getCurrentUser()) {
      // login TRUE
      return true;
    } else {
      this.router.navigate(['/user/login']);
      return false;
    }
  }
}
