import { Injectable } from '@angular/core';
import { UsuarioModel } from '../../models/UsuarioModel';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class JwtService {
    decodeToken(tempToken?: string): UsuarioModel | null {
      const token = tempToken ? tempToken : this.getToken();
      try {
        if (token) {
          const decodedToken: any = jwt_decode(token);
          const user = new UsuarioModel(decodedToken);

          return user;
        }
        return null;
      } catch (error) {
        console.error('Decode token error', error);
        return null;
      }
    }

    getToken(): string | null {
      return window.localStorage.getItem('jwtMusicEventsToken');
    }

    saveToken(token: string) {
      if (token) {
        window.localStorage['jwtMusicEventsToken'] = token;
      } else {
        console.error('Attempted to save undefined token');
      }
    }

    destroyToken() {
      window.localStorage.removeItem('jwtMusicEventsToken');
    }

    isTokenExpired(): boolean {
      const token = this.getToken();
      try {
        if (token) {
          const decodedToken: any = jwt_decode(token!);

          if (decodedToken.exp === undefined) {
            return false;
          }

          const date = new Date(0);
          let tokenExpDate = date.setUTCSeconds(decodedToken.exp);
          // verifica se a data de expiração é menor que a data e hora atual
          return !(tokenExpDate.valueOf() > new Date().valueOf());
        }
        return true;
      } catch (error) {
        console.error('Decode token error', error);
        return true;
      }
    }

}
