﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEvent.Core.Commands;
using MusicEvent.Domain.Validations.Administracao;

namespace MusicEvent.Domain.Commands.Administracao
{
    public class UsuarioUpdateCommand : Command
    {
        public UsuarioUpdateCommand() { }
        public Guid Id { get; private set; }
        public string Nome { get; protected set; }
        public int Idade { get; protected set; }
        public string Email { get; protected set; }

        public Guid? IdPerfil { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new UsuarioUpdateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
