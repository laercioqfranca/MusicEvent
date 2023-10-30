import { NgModule, Optional, SkipSelf } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ConfigurationApiModule } from './configuration-api.module';

@NgModule({
  providers: [],
  declarations: [],
  imports: [RouterModule, ConfigurationApiModule],
  exports: [ConfigurationApiModule],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() core: CoreModule) {
    if (core) {
      throw new Error('O CoreModule só deve ser importado no AppModule');
    }
  }
}
