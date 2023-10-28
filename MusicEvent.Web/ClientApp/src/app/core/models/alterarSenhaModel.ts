export class AlterarSenhaModel {
    login: string;
    idUsuario: any;
    senhanova: string;
    senhaatual: string;
    
    constructor(login:string, senhaatual: string, senhanova: string, idUsuario: any ) {
        this.login = login;
        this.senhaatual = senhaatual;
        this.senhanova = senhanova;
        this.idUsuario = idUsuario;
    }
}
