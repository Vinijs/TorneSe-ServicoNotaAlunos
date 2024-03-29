
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Utils;
using TorneSe.ServicoNotaAlunos.Domain.Enums;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAlunos.Domain.Services;
public class ServicoValidacaoNotaAluno : IServicoValidacaoNotaAluno
{
    private readonly ContextoNotificacao _contextoNotificacao;

    private readonly IHandler<ServicoNotaValidacaoRequest> _validacaoHandler;

    public ServicoValidacaoNotaAluno(ContextoNotificacao contextoNotificacao,
                                     IHandler<ServicoNotaValidacaoRequest> validacaoHandler)
    {
        _contextoNotificacao = contextoNotificacao;
        _validacaoHandler = validacaoHandler;
    }
    private bool ValidarProfessor(Professor professor,int disciplinaId)
    {
        //o professor deve ser um usuário ativo
        if(!professor.Ativo)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_INATIVO);
            return false;
        }

        //Deve ministrar a disciplina
        if(!(professor.DisciplinaId == disciplinaId))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_NAO_MINISTRA_A_DISCIPLINA);
            return false;
        }

        //Deve ser professor titular e não suplente
        if(!professor.ProfessorTitular && professor.ProfessorSuplente)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_DEVE_SER_TITULAR);
            return false;
        }

        return true;

    }

    private bool ValidarDisciplina(Disciplina disciplina)
    {
        //A disciplina não pode ser do tipo encontro
        if(disciplina.TipoDisciplina == TipoDisciplina.Encontro)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.DISCIPLINA_TIPO_ENCONTRO);
            return false;
        }

        //A disciplina deve estar ativa
        if(!DisciplinaAtiva(disciplina))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.DISCIPLINA_INATIVA);
            return false;
        }

        return true;
    }

    private bool ValidarAluno(Aluno aluno,int disciplinaId)
    {
        //O aluno deve ser um usuario ativo
        if(!aluno.Ativo)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.ALUNO_INATIVO);
            return false;
        }

        //O aluno deve estar inscrito na disciplina pela sua turma
        if(!AlunoEstaMatriculado(aluno, disciplinaId))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.ALUNO_NAO_ESTA_MATRICULADO);
            return false;
        }

        return true;
    }

    private bool DisciplinaAtiva(Disciplina disciplina) =>
        disciplina.DataInicio <= DateTime.Now && disciplina.DataFim >= DateTime.Now;

    private bool AlunoEstaMatriculado(Aluno aluno, int disciplinaId) =>
        aluno.AlunosTurmas
            .Select(alunoTurma => alunoTurma.Turma)
            .Any(turma => turma.DisciplinaId == disciplinaId);
    public void ValidarLancamento(Aluno aluno, Professor professor, Disciplina disciplina)
    {
        if(!ValidarAluno(aluno, disciplina.Id))
            return;
        if(!ValidarProfessor(professor, disciplina.Id))
            return;
        ValidarDisciplina(disciplina);
    }

    public void ValidarLancamento(ServicoNotaValidacaoRequest request)
    {
        // var inicialHandler = new AlunoValidacaoHandler(_contextoNotificacao);
        
        // inicialHandler.SetNext(new ProfessorValidacaoHandler(_contextoNotificacao))
        // .SetNext(new DisciplinaValidacaoHandler(_contextoNotificacao));
        // inicialHandler.Handle(request);

        _validacaoHandler.Handle(request);
    }

}