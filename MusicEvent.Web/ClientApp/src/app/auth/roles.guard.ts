import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Route,
  Router
} from '@angular/router';
import { LoginService } from '../services/root/login.service';

@Injectable({
  providedIn: 'root',
})
export class RolesGuards implements CanActivate, CanLoad {
  constructor(private router: Router, private loginService: LoginService) {}

  canLoad(route: Route) {
    return this.validaPerfil(route);
  }

  canActivate(route: ActivatedRouteSnapshot) {
    return this.validaPerfil(route);
  }

  validaPerfil(route: any) {
    const dados: any[] | null = route.data.roles;
    const perfilUsuario = 2;

    if (dados?.includes(perfilUsuario)) {
      return true;
    }
    this.router.navigateByUrl('/acesso');
    return false;
  }
}
