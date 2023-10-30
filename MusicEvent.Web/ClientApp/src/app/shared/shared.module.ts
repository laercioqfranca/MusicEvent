import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import {
  MatCommonModule,
  MatNativeDateModule,
  MatOptionModule,
  MatRippleModule,
} from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { MatTreeModule } from '@angular/material/tree';
import { RouterModule } from '@angular/router';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import {MatGridListModule} from '@angular/material/grid-list';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

const mainModulesPack = [
  CommonModule,
  FormsModule,
  ReactiveFormsModule,
  RouterModule,
];

const materialModulesPack = [
  MatTreeModule,
  MatCheckboxModule,
  MatRadioModule,
  MatCommonModule,
  MatOptionModule,
  MatRippleModule,
  MatNativeDateModule,
  MatDatepickerModule,
  MatInputModule,
  MatTableModule,
  MatPaginatorModule,
  MatAutocompleteModule,
  MatInputModule,
  MatDialogModule,
  MatGridListModule,
  MatSelectModule,
  MatFormFieldModule

];

const MODULOS_COMPARTILHADOS = [
    ...mainModulesPack,
    ...materialModulesPack,
    BsDatepickerModule
]


@NgModule({
  declarations: [],
  imports: [
    ...MODULOS_COMPARTILHADOS
  ],
  exports: [
    ...MODULOS_COMPARTILHADOS
  ]
})
export class SharedModule { }
