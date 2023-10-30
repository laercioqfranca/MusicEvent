export class UsuarioModel {
    id: string;
    nome: string;
    email: string;
    enumPerfil: string;

    nbf!: number;
    exp!: number;
    iss!: string;
    aud!: string;

    constructor(data: any) {
      if (!data) {
        throw new Error("Data is null or undefined");
      }
  
      this.id = data.Id;
      this.nome = data.Nome;
      this.email = data.Email;
      this.enumPerfil = data.EnumPerfil;
      
      this.nbf = data.nbf;
      this.exp = data.exp;
      this.iss = data.iss;
      this.aud = data.aud;
  
    }
    
  }
  