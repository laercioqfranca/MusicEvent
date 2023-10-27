import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {

  isLoggedIn: boolean;

  perfilUsuario:string = 'admin';

  constructor(
    private authService: AuthService,
    private loginService: LoginService,
    private router : Router
    ) {

    this.isLoggedIn = !!this.authService.currentUserValue;
    this.authService.isAuthenticated.subscribe((res) => {
      this.isLoggedIn = res;
    });
  }

  logout(){
    this.loginService.logout();
    this.router.navigateByUrl('/login');
  }

  getUserName(){
    // return this.authService.currentUserValue?.nome;
    return 'Dev';
  }

  getPerfilUsuario(){

  }
}
