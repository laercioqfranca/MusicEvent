import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EventoService } from 'src/app/services/evento.service';
import { NotificationService } from 'src/app/services/root/notification.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  criarEventoForm!: FormGroup;
  evento!:any;
  
  mostrarCriarEvento: boolean = true;
  listaEventos: any[] = []; 

  constructor(
    private notificationService: NotificationService,
    private eventoService: EventoService
    ) { }  
  
  ngOnInit() {
    this.iniciarForm();
    this.listarEventos();
  }
    
  iniciarForm(){
    this.criarEventoForm = new FormGroup({
      descricao: new FormControl(null, [Validators.required]),
      data: new FormControl(null, [Validators.required])
    });
  }

  onSubmit(){
    if(this.criarEventoForm.valid){

      if(!this.evento){
        this.criarEvento(this.criarEventoForm?.value);
        this.criarEventoForm.reset();
      }
      
      if(this.evento){ // se tiver algo na variável evento, então edita
        this.autualizarEvento(this.criarEventoForm?.value);
        this.criarEventoForm.reset();
        this.evento = null;
      }

    }
  }

  obterEventoPorId(id:any){
    this.eventoService.getById(id)
      .subscribe({
        next: (res) => {
          this.evento = res.data;
          this.carregarDados(); // carrega os dados no formulário
        },
        error: (e) => {
          this.notificationService.showError("Ocorreu algum erro ao carregar o evento!", "Ops...");
        },
      });
  }

  listarEventos(){
    this.eventoService.getAll()
      .subscribe({
        next: (res) => {
          this.listaEventos = res.data;

          this.listaEventos.forEach(element => {
            element.data = new Date(element.data).toLocaleDateString();
          });
          
        },
        error: (e) => {
          this.notificationService.showError("Ocorreu algum erro ao carregar os eventos!", "Ops...");
        },
      });
  }

  editar(id:any){
    this.obterEventoPorId(id);
  } 

  carregarDados(){
    setTimeout(() => {
      this.criarEventoForm.get('descricao')?.setValue(this.evento.descricao);
      this.criarEventoForm.get('data')?.setValue(new Date(this.evento.data));
    }, 1000);

  }

  deletar(id:any){
    if(id != null){
      this.eventoService.delete(id).subscribe(
        {
          next:(res) => {
            if (res?.success) {
                this.notificationService.showSuccess('Evento excluído com sucesso!', '');
                this.listarEventos();
            }
          },
          error: (e) => {
            this.notificationService.showError("Ocorreu algum erro ao deletar o evento!", "Ops...");
          },
        }

      );
    }
  }

  criarEvento(evento:any){
    this.eventoService.create(evento).subscribe({
      next:(res) => {
        if (res?.success) {
          this.notificationService.showSuccess('Evento criado com sucesso!', '');
          this.listarEventos();
        }
      },
      error: (e) => {
        this.notificationService.showError("Ocorreu algum erro ao criar o evento!", "Ops...");
      },
    });
  }

  autualizarEvento(evento:any){
    const viewModel = {
      id: this.evento.id,
      descricao: evento.descricao,
      data: evento.data
    }

    this.eventoService.update(viewModel).subscribe({
      next:(res) => {
        if (res?.success) {
          this.notificationService.showSuccess('Evento atualizado com sucesso!', '');
          this.listarEventos();
        }
      },
      error: (e) => {
        this.notificationService.showError("Ocorreu algum erro ao atualizar o evento!", "Ops...");
      },
    });
  }

  limpar(){
    this.evento = null;
    this.criarEventoForm.reset();
  }

}
