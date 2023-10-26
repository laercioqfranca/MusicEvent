import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

    constructor(private toastr: ToastrService) { }

    showSuccess(message: string, title?: string) {
      this.toastr.success(message, title ?? '', { timeOut: 3000, positionClass: "toast-top-right" });
    }

    showError(messages: string | string[], title?: string) {
      const titleDefault = "Ops...";
      const errorMessages = Array.isArray(messages) ? messages : [messages];

      for (let i = 0; i < errorMessages.length && !(i > 3); i++) {
        this.toastr.error(errorMessages[i], title ? title : titleDefault, { timeOut: 5000, positionClass: "toast-top-right" });
      }
    }
    showInfo(message: string, title: string) {
      this.toastr.info(message, title ?? '', { timeOut: 4000, positionClass: "toast-top-right" });
    }

    showWarning(message: string, title: string) {
      this.toastr.warning(message, title ?? '', { timeOut: 4000, positionClass: "toast-top-right" });
    }

}
