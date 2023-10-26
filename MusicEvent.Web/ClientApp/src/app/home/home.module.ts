import { NgModule } from '@angular/core';

import { ClienteComponent } from './cliente/cliente.component';
import { AdminComponent } from './admin/admin.component';
import { HomeComponent } from './home.component';
import { Router, RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    HomeComponent,
    ClienteComponent,
    AdminComponent
  ],

  imports:[
    RouterModule.forChild([
        { path: '', component: HomeComponent },
      ]),
  ]

})
export class HomeModule { }
