export class RecuperarSenhaModel {
  email: string;
  login: string;
  // senha?: string;
  // confirmarSenha?: string;
  url: string;
  
  constructor(email:string, login:string, url:string){
    this.email = email;
    this.login = login;
    this.url = url
  }

}
