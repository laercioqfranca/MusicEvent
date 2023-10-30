import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [

  // REDIRECTS
  { path: '', redirectTo: '/login', pathMatch: 'full'},

  // ADMINISTRAÇÃO
        // Autenticação  

  { path: 'login', loadChildren: () => import('./auth/login/login.module').then(x => x.LoginModule) },

        // Usuário admin
  { path: 'admin/home', loadChildren: () => import('./home/admin/admin.module').then(x=>x.AdminModule), 
    pathMatch:'full', 
    canActivate: [AuthGuard]
  },

  { path: 'cliente/home', loadChildren: () => import('./home/cliente/cliente.module').then(x=>x.ClienteModule), 
    pathMatch:'full', 
    canActivate: [AuthGuard]
  },
  
  { path: 'criar-conta', loadChildren: () => import('./criar-conta/criar-conta.module').then(x=>x.CriarContaModule), 
    pathMatch:'full',
  },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
