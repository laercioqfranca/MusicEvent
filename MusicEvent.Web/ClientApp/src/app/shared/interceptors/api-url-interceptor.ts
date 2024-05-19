import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Inject, Injectable, InjectionToken } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { JwtService } from 'src/app/core/services/root/jwt.service';
import { AES, enc, mode, pad } from 'crypto-js';

export const BASE_API_URL = new InjectionToken<string>('baseApiUrl');
export const BASE_REPORT_URL = new InjectionToken<string>('baseReportUrl');
export const AMBIENTE_PRODUCTION = new InjectionToken<string>('production');

@Injectable()
export class ApiUrlInterceptor implements HttpInterceptor {
  requisitions = 0;

  constructor(
    @Inject(BASE_API_URL) private apiUrl: string,
    @Inject(BASE_REPORT_URL) private reportUrl: string,
    @Inject(AMBIENTE_PRODUCTION) private router: Router,
    private jwtService: JwtService,
    private spinner: NgxSpinnerService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.requisitions == 0)
      this.spinner.show();

    this.requisitions++;
    const headersConfig = {
      'Accept': 'application/json',
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache',
      'x-requestid': this.newGuid(),
      'Authorization': ''
    };
    const token = this.jwtService.getToken();
    if (token) {
      headersConfig['Authorization'] = `Bearer ${token}`;
      this.isExpired();
    }

    if (req.url.search('/login') >= 0) {
      const key = enc.Utf8.parse('sJgppTL83sr1HILcta0LX0yEMJUo8qs3'); // replace with your secret key
      const iv = enc.Utf8.parse('HR$2pIjHR$2pIj12'); // replace with your initialization vector
      const encrypted = AES.encrypt(JSON.stringify(req.body), key,
        {
          keySize: 128 / 8,
          iv: iv,
          mode: mode.CBC,
          padding: pad.Pkcs7
        }).toString();

      const encryptedObj = { encrypted };
      req = req.clone({ url: this.prepareUrl(req.url), setHeaders: headersConfig, body: encryptedObj });

    } else {
      if (req.url.search(/report:/gi) >= 0)
        req = req.clone({ url: this.prepareUrlReport(req.url.replace('report:', '')) });
      else
        req = req.clone({ url: this.prepareUrl(req.url), setHeaders: headersConfig });
    }

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorStatusMessage: any;
        let errorMessage: string | string[];
        if (error.error instanceof ErrorEvent) {
          // Erro do lado do cliente ou de rede

          errorMessage = `Erro: ${error.error.message}`;
        } else {
          // Erro retornando do servidor
          switch (error.status) {
            case 404:
              errorMessage = 'Recurso não encontrado!';
              break;
            case 403:
              errorMessage = 'Acesso proibido!';
              break;
            // Adicione outros códigos de erro que deseja tratar aqui
            default:
              if (error.error.errors) {
                const errorsList = error.error.errors as Record<string, string[]>;
                errorMessage = Object.values(errorsList).flat();
              }
              else {
                // trate o caso em que error.error.errors é nulo ou indefinido
                errorMessage = 'Ocorreu um problema inesperado, entre em contato com o suporte.';
              }
          }
        }
        errorStatusMessage = {
          message: errorMessage,
          status: error.status
        }
        return throwError(errorStatusMessage);
      }),
      finalize(() => {
        this.requisitions--;
        if (this.requisitions == 0)
          this.spinner.hide();
      })
    );
  }

  private prepareUrl(url: string): string {
    url = this.isAbsoluteUrl(url) || this.apiUrl === '/' ? url : this.apiUrl + '/' + url;
    return url.replace(/([^:]\/)\/+/g, '$1');
  }

  private prepareUrlReport(url: string): string {
    url = this.reportUrl + '/' + url;
    return url.replace(/([^:]\/)\/+/g, '$1');
  }

  private isAbsoluteUrl(url: string): boolean {
    const absolutePattern = /^https?:\/\//i;
    return absolutePattern.test(url);
  }

  isExpired(): void {
    var tokenExpired = this.jwtService.isTokenExpired()

    if (tokenExpired === null) {
      this.jwtService.destroyToken();
      this.router.navigateByUrl('/admin/login');
    }
    else if (tokenExpired) {
      this.jwtService.destroyToken();
      this.router.navigateByUrl('/admin/login');
    }
  }

  newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      const r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}
