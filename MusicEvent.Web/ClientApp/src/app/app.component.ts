import { Component } from '@angular/core';
import { AuthService } from './services/root/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'MusicEvents';

  isLoggedIn: boolean;

  constructor(private authService: AuthService) {
    this.isLoggedIn = !!this.authService.currentUserValue;
    this.authService.isAuthenticated.subscribe((res) => {
      this.isLoggedIn = res;
    });
  }

}
