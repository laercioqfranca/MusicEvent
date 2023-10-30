import { NgModule } from '@angular/core';

import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ClienteComponent } from './cliente.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    ClienteComponent
  ],

  imports:[
    SharedModule,
    CommonModule,
    RouterModule.forChild([
        { path: '', component: ClienteComponent },
      ]),
  ]

})
export class ClienteModule { }
