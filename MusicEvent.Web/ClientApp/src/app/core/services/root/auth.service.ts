import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { ILogin } from '../../interfaces/interfaces';
import { UsuarioModel } from '../../models/usuarioModel';
import { JwtService } from './jwt.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<UsuarioModel | null>;
  public currentUser: Observable<UsuarioModel | null>;

  public isAuthenticated: Observable<boolean>;

  constructor(private jwtService: JwtService) {
    const user = jwtService.decodeToken();
    
    this.currentUserSubject = new BehaviorSubject<UsuarioModel | null>(user);
    this.currentUser = this.currentUserSubject.asObservable();

    //Cria o Observable para a autenticação do usuário
    this.isAuthenticated = this.currentUser.pipe(
      map(user => !!user && !jwtService.isTokenExpired())
    )

    if (!user && jwtService.isTokenExpired()) this.clearAuth();
  }

  public get currentUserValue(): UsuarioModel | null {
    return this.currentUserSubject.value;
  }

  setSession(authResult: ILogin) {
    const token = authResult.accessToken;
    const user = this.jwtService.decodeToken(token);
    this.jwtService.saveToken(token);
    this.currentUserSubject.next(user);
  }

  clearAuth() {
    this.jwtService.destroyToken();
    this.currentUserSubject.next(null);
  }


}

