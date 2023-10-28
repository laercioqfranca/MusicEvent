import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

    // this.listaEventos.push(
    //   { descricao: "Festival de Jazz ao Ar Livre", data: "15/11/2023" },
    //   { descricao: "Concerto de Rock no Estádio", data: "02/12/2023" },
    //   { descricao: "Noite de Blues no Clube", data: "10/12/2023" },
    //   { descricao: "Apresentação de Ópera no Teatro", data: "05/01/2024" },
    //   { descricao: "Show de Música Eletrônica no Centro de Convenções", data: "20/01/2024" },
    //   { descricao: "Festival de Música Country no Campo", data: "08/02/2024" },
    //   { descricao: "Concerto de Orquestra Sinfônica no Auditório", data: "25/02/2024" },
    //   { descricao: "Apresentação de Bandas Locais no Bar da Cidade", data: "12/03/2024" },
    //   { descricao: "Noite de Reggae na Praia", data: "30/03/2024" },    
    //   { descricao: "Festival de Hip-Hop na Praça", data: "15/04/2024" },
    //   { descricao: "Concerto de Música Clássica na Igreja Antiga", data: "01/05/2024" },
    //   { descricao: "Show de Pop Internacional no Estádio", data: "20/05/2024" },
    //   { descricao: "Noite de Música Latina no Clube", data: "10/06/2024" },
    //   { descricao: "Festival de Indie Rock no Parque", data: "28/06/2024" },
    //   { descricao: "Concerto de Folk Acústico no Café", data: "15/07/2024" },
    //   { descricao: "Apresentação de Cantores Locais no Teatro Pequeno", data: "03/08/2024" },
    //   { descricao: "Noite de R&B na Casa Noturna", data: "20/08/2024" },
    //   { descricao: "Festival de World Music no Jardim Botânico", data: "09/09/2024" }
    // );

    // this.listaMeusEventos.push(
    //   { descricao: "Show de Música Eletrônica no Centro de Convenções", data: "20/01/2024" },
    //   { descricao: "Festival de Música Country no Campo", data: "08/02/2024" },
    //   { descricao: "Concerto de Orquestra Sinfônica no Auditório", data: "25/02/2024" },
    //   { descricao: "Apresentação de Bandas Locais no Bar da Cidade", data: "12/03/2024" }
    // )

  }

  inscrever(id: any) {
    var model = { idUsuario: this.user?.id, idEvento: id };
    this.inscricaoService.create(model).subscribe({
      next: (res) => {
        if (res?.success) {
          this.notificationService.showSuccess('Inscrição feita com sucesso!', '');
          this.listarMeusEventos(this.user.id);
        }
      },
      error: (e) => {
        this.notificationService.showError(e?.message)
      },
    });
  }

  deletar(id: any) {
    // console.log(id); //teste
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
            this.notificationService.showError(e?.message)
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
            // console.log(this.listaEventos);
          }
        },
        error: (e) => {
          this.notificationService.showError(e?.message)
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
            // console.log(this.listaMeusEventos);
          }
        },
        error: (e) => {
          this.notificationService.showError(e?.message)
        },
      }

    );
  }


}
