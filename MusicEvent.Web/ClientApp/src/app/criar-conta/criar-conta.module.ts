import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { CriarContaComponent } from '../criar-conta/criar-conta.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      { path: '', component: CriarContaComponent },
    ]),
  ],
  declarations: [CriarContaComponent]
})
export class CriarContaModule { }
