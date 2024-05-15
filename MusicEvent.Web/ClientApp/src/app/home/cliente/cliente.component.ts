import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { InscricaoModel } from 'src/app/models/InscricaoModel';
import { EventoService } from 'src/app/services/evento.service';
import { InscricaoService } from 'src/app/services/inscricao.service';
import { AuthService } from 'src/app/services/root/auth.service';
import { JwtService } from 'src/app/services/root/jwt.service';
import { NotificationService } from 'src/app/services/root/notification.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {

  evento!: any;
  user!: any;

  mostrarCriarEvento: boolean = true;
  listaEventos: any[] = [];
  listaMeusEventos: any[] = [];

  constructor(
    private notificationService: NotificationService,
    private eventoService: EventoService,
    private inscricaoService: InscricaoService,
    private jwtService: JwtService,
  ) { }

  ngOnInit() {

    let token = this.jwtService.getToken()?.toString();
    this.user = this.jwtService.decodeToken(token);

    this.listarEventos();
    this.listarMeusEventos(this.user?.id);

  }

  inscrever(idEvento: any) {
    let model = new InscricaoModel(idEvento);

    if(!this.inscricaoExistente(idEvento)){
      this.inscricaoService.create(model).subscribe({
        next: (res) => {
          if (res?.success) {
            this.notificationService.showSuccess('Inscrição realizada com sucesso!', '');
            this.listarMeusEventos(this.user.id);
          }
        },
        error: (e) => {
          this.notificationService.showError("Ocorreu algum erro durante a inscrição!", "Ops...");
        },
      });
    }

  }

  inscricaoExistente(idEvento:string){
    let inscricaoExistente = this.listaMeusEventos.filter(e => e.id == idEvento);
    return inscricaoExistente.length > 0;
  }

  deletar(id: any) {
    if (id != null) {
      this.inscricaoService.delete(id).subscribe(
        {
          next: (res) => {
            if (res?.success) {
              this.notificationService.showSuccess('Inscrição cancelada com sucesso!', '');
              this.listarMeusEventos(this.user.id);
            }
          },
          error: (e) => {
            this.notificationService.showError("Ocorreu algum erro durante o cancelamento da inscrição!", "Ops...");
          },
        }

      );
    }
  }

  listarEventos() {
    this.eventoService.getAll().subscribe(
      {
        next: (res) => {
          if (res?.success) {
            this.listaEventos = res.data;

            this.listaEventos.forEach(element => {
              element.data = new Date(element.data).toLocaleDateString();
            });

          }
        },
        error: (e) => {
          this.notificationService.showError("Ocorreu algum erro ao carregar os seus eventos!", "Ops...");
        },
      }

    );
  }

  listarMeusEventos(id: any) {
    this.inscricaoService.getAllById(id).subscribe(
      {
        next: (res) => {
          if (res?.success) {
            this.listaMeusEventos = res.data;

            this.listaMeusEventos.forEach(element => {
              element.data = new Date(element.data).toLocaleDateString();
            });
            
          }
        },
        error: (e) => {
          this.notificationService.showError("Ocorreu algum erro ao carregar os seus eventos!", "Ops...");
        },
      }

    );
  }


}
