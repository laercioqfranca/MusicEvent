import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { ILogin } from '../../interfaces/interfaces';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) {
  }

  login(credentials:any): Observable<ILogin> {
    return this.http.post<ILogin>('v1/authentication/login', credentials);
  }

  logout() {
    this.authService.clearAuth();
  }

}
