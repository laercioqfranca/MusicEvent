import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AMBIENTE_PRODUCTION, ApiUrlInterceptor, BASE_API_URL, BASE_REPORT_URL } from '../shared/interceptors/api-url-interceptor';

@NgModule({
    providers: [
        { provide: BASE_API_URL, useValue: environment.baseApiUrl },
        { provide: BASE_REPORT_URL, useValue: environment.baseReportUrl },
        { provide: AMBIENTE_PRODUCTION, useValue: environment.production },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ApiUrlInterceptor,
            multi: true
        }
    ]
})
export class ConfigurationApiModule {
}
