import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  // REDIRECTS
  { path: '', redirectTo: '/login', pathMatch: 'full'},

  // ADMINISTRAÇÃO
        // Autenticação  

  { path: 'login', loadChildren: () => import('./login/login.module').then(x => x.LoginModule) },

        // Usuário admin
  { path: 'home', loadChildren: () => import('./home/home.module').then(x=>x.HomeModule), 
    pathMatch:'full'},
  
  { path: 'criar-conta', loadChildren: () => import('./criar-conta/criar-conta.module').then(x=>x.CriarContaModule), 
    pathMatch:'full'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
