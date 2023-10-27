import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { NotificationService } from '../services/root/notification.service';
import { JwtService } from '../services/root/jwt.service';
import { AuthService } from '../services/root/auth.service';


@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  
  constructor( 
    private authService: AuthService, 
    private router: Router, 
    private jwtService:JwtService
    ) {}

  canActivate(): boolean {

    const currentUser = this.authService.currentUserValue;
    const isTokenExpired = this.jwtService.isTokenExpired();

    if (!isTokenExpired) {
      // Se o token não estiver expirado, permitir o acesso
      return true;
    } else {
      // Se o usuário não está autenticado, redirecionar para a página de login
      this.authService.clearAuth();
      this.router.navigateByUrl('/login');
      return false;
    }
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot) {
    return this.canActivate();
  }

}
