using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Excecoes;
public class DomainException : Exception
{
    public DomainException(string mensagem) : base (mensagem) {}
    public DomainException(string mensagem, Exception excecao) : base(mensagem, excecao){}
}