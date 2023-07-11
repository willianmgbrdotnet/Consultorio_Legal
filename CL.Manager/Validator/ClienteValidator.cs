using System;
using System.Collections.Generic;
using System.Text;
using CL.Core.Domain;
using FluentValidation;

//Instalei o nugget "FluenteValidation.AspNetCore nos projetos "CL.Manager e CL.WebApi".
//Adicionei novas propriedades no Domain "Cliente" e novas "Migrations"

namespace CL.Manager.Validator
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty().MinimumLength(10).MaximumLength(150);
            RuleFor(x => x.DataNascimento).NotNull().NotEmpty().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-130));
            RuleFor(x => x.Documento).NotNull().NotEmpty().MinimumLength(4).MaximumLength(14);
            RuleFor(x => x.Telefone).NotNull().NotEmpty().Matches("[2-9]{10}").WithMessage("O Telefone precisa ter o formato [2-9]{10}");
            RuleFor(x => x.Sexo).NotNull().NotEmpty().Must(IsMorF).WithMessage("Sexo precisa ser M ou F");
        }

        private bool IsMorF(char Sexo)
        {
            //Aspas simples
            return Sexo == 'M' || Sexo == 'F';
        }
    }
}
