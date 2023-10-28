import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { MenuComponent } from './core/menu/menu.component';
import { AppRoutingModule } from './app.routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './auth/login/login.component';
import { BsDatepickerConfig, BsDatepickerModule, BsLocaleService} from 'ngx-bootstrap/datepicker';
import { defineLocale, ptBrLocale } from 'ngx-bootstrap/chronos';
import { FooterComponent } from './core/footer/footer.component';
import { CoreModule } from './core/core.module';
import { NgxSpinnerModule } from 'ngx-spinner';

defineLocale('pt-br', ptBrLocale);

function getDatepickerConfig(): BsDatepickerConfig {
  return Object.assign(new BsDatepickerConfig(), {
    dateInputFormat: 'DD/MM/YYYY',
    dateFormat: 'DD/MM/YYYY',
    containerClass: 'theme-red',
    showWeekNumbers: false,
  });
}

@NgModule({
  declarations: [	
    AppComponent,
    MenuComponent,
    FooterComponent
   ],
  imports: [
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    HttpClientModule,
    BsDatepickerModule.forRoot(),
    CoreModule,
    NgxSpinnerModule,

    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' }
    ]),

    TooltipModule.forRoot(),
    CollapseModule.forRoot(),
    ToastrModule.forRoot(),
  ],
  providers: [
    BsLocaleService,
    { provide: BsDatepickerConfig, useFactory: getDatepickerConfig },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private bsLocaleService: BsLocaleService) {
    this.bsLocaleService.use('pt-br');
  }
}
