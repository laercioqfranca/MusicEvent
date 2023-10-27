import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    AdminComponent
  ],

  imports:[
    SharedModule,
    CommonModule,
    RouterModule.forChild([
        { path: '', component: AdminComponent },
      ]),
  ]

})
export class AdminModule { }
