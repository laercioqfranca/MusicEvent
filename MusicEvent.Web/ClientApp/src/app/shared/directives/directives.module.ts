import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DateMaskDirective } from './date-mask.directive';

@NgModule({
  imports: [CommonModule],
  declarations: [DateMaskDirective],
  exports: [DateMaskDirective],
})
export class DirectivesModule {}
