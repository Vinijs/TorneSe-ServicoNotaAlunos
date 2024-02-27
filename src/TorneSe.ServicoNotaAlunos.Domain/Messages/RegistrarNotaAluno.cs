using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Validations;

namespace TorneSe.ServicoNotaAlunos.Domain.Messages;
public class RegistrarNotaAluno : Mensagem
{
    public int AlunoId { get; set; }
    public int ProfessorId { get; set; }
    public int AtividadeId { get; set; }
    public Guid CorrelationId { get; set; }
    public double ValorNota { get; set; }
    public bool NotaSubstitutiva { get; set; }

    public override bool MensagemEstaValida()
    {
        Validacoes = RegistrarNotaAlunoValidacao.Instance.Validate(this);
        return Validacoes.IsValid;
    }
}