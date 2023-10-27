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

    this.listaEventos.push(
      { descricao: "Evento 1", data: "25/10/2023"},
      { descricao: "Evento 2", data: "25/10/2023"},
      { descricao: "Evento 3", data: "25/10/2023"},
      { descricao: "Evento 4", data: "25/10/2023"},
      { descricao: "Evento 5", data: "25/10/2023"}
      
      )
  }
    
  iniciarForm(){
    this.criarEventoForm = new FormGroup({
      descricao: new FormControl('', [Validators.required]),
      data: new FormControl('', [Validators.required])
    });
  }

  onSubmit(){
    console.log(this.criarEventoForm); //teste
    
    if(this.criarEventoForm.valid){

      if(!this.evento){
        console.log("create"); //teste
        this.criarEvento(this.criarEventoForm?.value);
      }
      
      if(this.evento){ // se tiver algo na variável evento, então edita
        console.log("update"); //teste
        this.autualizarEvento(this.criarEventoForm?.value);
      }

    }
  }

  obterEventoPorId(id:any){
    this.eventoService.getById(id)
      .subscribe({
        next: (res) => {
          this.evento = res.data;
          console.log(this.evento); //teste
        },
        error: (e) => {
          this.notificationService.showError(e?.message)
        },
      });
  }

  editar(id:any){

    this.obterEventoPorId(id); // busca o elemento selecionado e guarda na variável evento

    console.log(id); //teste
    console.log(this.evento); //teste

    this.carregarDados(); // carrega os dados da variável evento no formulário

  } 

  carregarDados(){
    setTimeout(() => {
      this.criarEventoForm.get('descricao')?.setValue(this.evento.descricao);
      this.criarEventoForm.get('data')?.setValue(this.evento.data);
    }, 1000);
  }

  deletar(id:any){
    console.log(id); //teste
    if(id != null){
      this.eventoService.delete(id).subscribe(
        {
          next:(res) => {
            if (res?.success) {
                this.notificationService.showSuccess('Evento excluído com sucesso!', '');
            }
          },
          error: (e) => {
            this.notificationService.showError(e?.message)
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
          this.criarEventoForm.reset();
        }
      },
      error: (e) => {
        this.notificationService.showError(e?.message)
      },
    });
  }

  autualizarEvento(evento:any){
    this.eventoService.update(evento).subscribe({
      next:(res) => {
        if (res?.success) {
          this.notificationService.showSuccess('Evento atualizado com sucesso!', '');
          this.evento = null;
          this.criarEventoForm.reset();
        }
      },
      error: (e) => {
        this.notificationService.showError(e?.message)
      },
    });
  }

}
