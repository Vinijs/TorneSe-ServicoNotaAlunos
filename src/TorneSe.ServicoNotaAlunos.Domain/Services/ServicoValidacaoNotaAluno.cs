
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

namespace TorneSe.ServicoNotaAlunos.Domain.Services;
public class ServicoValidacaoNotaAluno : IServicoValidacaoNotaAluno
{
    private readonly ContextoNotificacao _contextoNotificacao;

    public ServicoValidacaoNotaAluno(ContextoNotificacao contextoNotificacao)
    {
        _contextoNotificacao = contextoNotificacao;
    }
    private void ValidarProfessor(Professor professor,int disciplinaId)
    {
        //o professor deve ser um usuário ativo
        if(!professor.Usuario.Ativo)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_INATIVO);
            return;
        }

        //Deve ministrar a disciplina
        if(!(professor.DisciplinaId == disciplinaId))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_NAO_MINISTRA_A_DISCIPLINA);
            return;
        }

        //Deve ser professor titular e não suplente
        if(!professor.ProfessorTitular && professor.ProfessorSuplente)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.PROFESSOR_DEVE_SER_TITULAR);
            return;
        }
    }

    private void ValidarDisciplina(Disciplina disciplina)
    {
        //A disciplina não pode ser do tipo encontro
        if(disciplina.TipoDisciplina == TipoDisciplina.Encontro)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.DISCIPLINA_TIPO_ENCONTRO);
            return;
        }

        //A disciplina deve estar ativa
        if(!DisciplinaAtiva(disciplina))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.DISCIPLINA_INATIVA);
            return;
        }
    }

    private void ValidarAluno(Aluno aluno,int disciplinaId)
    {
        //O aluno deve ser um usuario ativo
        if(!aluno.Usuario.Ativo)
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.ALUNO_INATIVO);
            return;
        }

        //O aluno deve estar inscrito na disciplina pela sua turma
        if(!AlunoEstaMatriculado(aluno, disciplinaId))
        {
            _contextoNotificacao.Add(Constantes.MensagensValidacao.ALUNO_NAO_ESTA_MATRICULADO);
            return;
        }
    }

    private bool DisciplinaAtiva(Disciplina disciplina) =>
        disciplina.DataInicio <= DateTime.Now && disciplina.DataFim >= DateTime.Now;

    private bool AlunoEstaMatriculado(Aluno aluno, int disciplinaId) =>
        aluno.AlunosTurmas
            .SelectMany(alunoTurma => alunoTurma.Turmas)
            .Any(turma => turma.DisciplinaId == disciplinaId);
    public void ValidarLancamento(Aluno aluno, Professor professor, Disciplina disciplina)
    {
        ValidarDisciplina(disciplina);
        ValidarProfessor(professor, disciplina.Id);
        ValidarAluno(aluno, disciplina.Id);
    }

    public void ValidarLancamento(ServicoNotaValidacaoRequest request)
    {
        var inicialHandler = new AlunoValidacaoHandler(_contextoNotificacao);
        
        inicialHandler.SetNext(new ProfessorValidacaoHandler(_contextoNotificacao))
        .SetNext(new DisciplinaValidacaoHandler(_contextoNotificacao));

        inicialHandler.Handle(request);
    }

}