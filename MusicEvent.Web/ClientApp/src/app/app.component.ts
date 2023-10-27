import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'MusicEvents';

  isCliente: boolean = false;
  isAdmin: boolean = true;

  isLoggedIn: boolean;

  constructor(private authService: AuthService) {
    this.isLoggedIn = !!this.authService.currentUserValue;
    this.authService.isAuthenticated.subscribe((res) => {
      this.isLoggedIn = res;

      this.isLoggedIn = true; //remover

    });
  }

}
