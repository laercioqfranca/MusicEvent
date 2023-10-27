import { Directive, HostBinding, HostListener } from "@angular/core";

@Directive({
    selector: '[bsDatepicker]'
  })
  export class DateMaskDirective {
    constructor() { }
  
    @HostBinding('attr.keyup')
    get onKeyUp() {
      return this.dateMask;
    }
  
    @HostListener('keyup', ['$event'])
    dateMask(event: any) {
      MaskedDate(event)
    }
  }
  
export function MaskedDate(event: any) {
    let v = event.target.value;

    if (event.code == "Backspace" && (v.length == 2 || v.length == 5)) {
        event.target.value = v.substr(0, v.length - 1)
    }
    else if (v.match(/^\d{2}$/) !== null) {
        event.target.value = v + '/';
    }
    else if (v.match(/^\d{2}\/\d{2}$/) !== null) {
        event.target.value = v + '/';
    }
}