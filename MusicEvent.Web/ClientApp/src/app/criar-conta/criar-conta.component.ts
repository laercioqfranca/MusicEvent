import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UsuarioService } from '../services/usuario.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/root/notification.service';

@Component({
  selector: 'app-criar-conta',
  templateUrl: './criar-conta.component.html',
  styleUrls: ['./criar-conta.component.css']
})
export class CriarContaComponent implements OnInit {

  criarContaForm!: FormGroup;
  constructor(
    private usuarioService: UsuarioService,
    private router: Router,
    private notificationService: NotificationService,
  ) { }

  ngOnInit() {
    this.criarForm();
  }

  criarForm(){
    this.criarContaForm = new FormGroup({
      nome: new FormControl('', [Validators.required]),
      idade: new FormControl('', [Validators.required, Validators.min(18)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      senha: new FormControl('', [Validators.required, Validators.minLength(8)]),
    });

  }

  onSubmit() {
    if (this.criarContaForm.valid) {
      this.usuarioService.create(this.criarContaForm?.value).subscribe({
        next: (res: any) => {
          if (res?.success) {
            this.notificationService.showSuccess('Conta criada com sucesso!', '');
            this.criarContaForm.reset();
            this.router.navigateByUrl('/login');
          }
        },
        error: (e) => {
          this.notificationService.showError("Ocorreu algum erro ao criar a conta!", "Ops...");
        },
    });
    }
  }  

  cancelar(){
    this.criarContaForm.reset();
    this.router.navigateByUrl('/login');
  }
  
}
