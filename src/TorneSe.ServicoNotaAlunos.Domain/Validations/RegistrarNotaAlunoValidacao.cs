using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TorneSe.ServicoNotaAlunos.Domain.Messages;

namespace TorneSe.ServicoNotaAlunos.Domain.Validations;
public class RegistrarNotaAlunoValidacao : AbstractValidator<RegistrarNotaAluno>
{

    public static RegistrarNotaAlunoValidacao Instance => new RegistrarNotaAlunoValidacao(); 
    public static string AlunoIdMsgErro => "O id do aluno está inválido";

    public static string ProfessorIdMsgErro => "O id do professor está inválido";

    public static string AtividadeIdMsgErro => "O id da atividade está inválida";

    public static string CorrelationIdMsgErro => "O id da transação está inválida";

    public static string ValorNotaMsgErro => "O valor da nota não pode ser inferior a zero";



    public RegistrarNotaAlunoValidacao()
    {
        RuleFor(x => x.AlunoId)
            .GreaterThan(default(int))
            .WithMessage(AlunoIdMsgErro);

        RuleFor(x => x.ProfessorId)
            .GreaterThan(default(int))
            .WithMessage(ProfessorIdMsgErro);

        RuleFor(x => x.AtividadeId)
            .GreaterThan(default(int))
            .WithMessage(AtividadeIdMsgErro);

        RuleFor(x => x.CorrelationId)
            .NotNull()
            .WithMessage(CorrelationIdMsgErro)
            .NotEqual(Guid.Empty)
            .WithMessage(CorrelationIdMsgErro);

        RuleFor(x => x.ValorNota)
            .GreaterThanOrEqualTo(default(int))
            .WithMessage(ValorNotaMsgErro);
    }
}