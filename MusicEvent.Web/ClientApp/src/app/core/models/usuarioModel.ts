export class UsuarioModel {
  id: string;
  idPerfil: string;
  nome: string;
  login: string;
  cpf: string;
  email: string;
  redefinirSenha: any;
  idTipoPerfil: number
  novaSenha!: string;
  nbf!: number;
  exp!: number;
  iss!: string;
  aud!: string;
  constructor(data: any) {
    if (!data) {
      throw new Error("Data is null or undefined");
    }

    this.id = data.Id;
    this.idPerfil = data.IdPerfil;
    this.nome = data.Nome;
    this.login = data.Login;
    this.cpf = data.Cpf;
    this.email = data.Email;
    this.idTipoPerfil = parseInt(data.IdTipoPerfil);
    this.novaSenha = data.NovaSenha;
    this.redefinirSenha = data.RedefinirSenha;
    this.nbf = data.nbf;
    this.exp = data.exp;
    this.iss = data.iss;
    this.aud = data.aud;

  }
}
