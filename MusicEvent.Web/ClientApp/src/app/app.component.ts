import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  usuarioLogado: boolean = true;
  isCliente: boolean = false;
  isAdmin: boolean = true;

}
