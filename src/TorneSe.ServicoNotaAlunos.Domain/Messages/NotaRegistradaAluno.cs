using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Messages;
public class NotaRegistradaAluno : Mensagem
{
    public NotaRegistradaAluno()
    {
        Erros = new ();
    }

    public int AlunoId { get; set; }
    public int AtividadeId { get; set; }
    public bool PossuiErros { get; set; }
    public Guid CorrelationId { get; set; }
    public List<string> Erros { get; set; }
    
    public override bool MensagemEstaValida()
    {
        return base.MensagemEstaValida();
    }
}