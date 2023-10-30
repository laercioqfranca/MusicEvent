import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NotificationService } from '../../services/root/notification.service';
import { LoginService } from '../../services/root/login.service';
import { ILogin } from '../../interfaces/interfaces';
import { JwtService } from '../../services/root/jwt.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/root/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formLogin!: FormGroup;
  
  constructor(
    private notificationService: NotificationService,
    private loginService: LoginService,
    private jwtService: JwtService,
    private router: Router,
    private authService: AuthService
    ) { }

  ngOnInit() {
    this.criaFormLogin();
  }

  criaFormLogin(){
    this.formLogin = new FormGroup({
      login: new FormControl('', [Validators.required, Validators.email]),
      senha: new FormControl('', Validators.required),
    });
  }

  onSubmit(){

    if(this.formLogin.valid){
      this.loginService.login(this.formLogin?.value).subscribe({
        next: (res: any) => {
  
          if (res.success == true) {
            const userJwt = <ILogin>res.data;
            const usuario = this.jwtService.decodeToken(userJwt.accessToken);

            if(usuario?.enumPerfil == '1'){
              this.router.navigateByUrl('/admin/home');
            }

            if(usuario?.enumPerfil == '2'){
              this.router.navigateByUrl('/cliente/home');
            }

            this.authService.setSession(userJwt);
  
          }
  
        },
        error: (e) => {
          this.formLogin.get('senha')?.setValue('');
          this.notificationService.showError(e.message);
        },
      });
    }
  }

}
