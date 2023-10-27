import { NgModule } from '@angular/core';

import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ClienteComponent } from './cliente.component';

@NgModule({
  declarations: [
    ClienteComponent
  ],

  imports:[
    CommonModule,
    RouterModule.forChild([
        { path: '', component: ClienteComponent },
      ]),
  ]

})
export class ClienteModule { }
