using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Messages;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.Excecoes;
using TorneSe.ServicoNotaAlunos.Domain.Utils;

namespace TorneSe.ServicoNotaAlunos.Domain.Services;
    public class ServicoNotaAluno : IServicoNotaAluno
    {
        private readonly ContextoNotificacao _contextoNotificacao;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        public ServicoNotaAluno(ContextoNotificacao contextoNotificacao, IUsuarioRepository usuarioRepository,
                                IDisciplinaRepository disciplinaRepository)
        {
            _contextoNotificacao = contextoNotificacao;
            _usuarioRepository = usuarioRepository;
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task LancarNota(RegistrarNotaAluno registrarNotaAluno)
        {
            Console.WriteLine("Processando lógica de negócio....");

            if(!registrarNotaAluno.MensagemEstaValida())
            {
                AdicionarMensagensErroNoContexto(registrarNotaAluno);
                return;
            }

            // var aluno = await _usuarioRepository.BuscarAluno(registrarNotaAluno.AlunoId);
            // var professor = await _usuarioRepository.BuscarProfessor(registrarNotaAluno.ProfessorId);
            // var disciplina = await  _disciplinaRepository.BuscarDisciplinaPorAtividadeId(registrarNotaAluno.AtividadeId);

            // await Task.WhenAll(new List<Task> {aluno, professor, disciplina });

            // var (aluno,professor,disciplina) = await BuscarAlunoProfessorDisciplina(registrarNotaAluno);

            var (aluno,professor,disciplina) = await BuscarAlunoProfessorDisciplina2(registrarNotaAluno);

            if(_contextoNotificacao.TemNotificacoes)
                return;
                
        }

        private async Task<(Aluno aluno, Professor professor, Disciplina disciplina)> BuscarAlunoProfessorDisciplina2(RegistrarNotaAluno registrarNotaAluno)
        {
            var aluno = await _usuarioRepository.BuscarAluno(registrarNotaAluno.AlunoId);

            if(aluno is null)
            {
                _contextoNotificacao.Add(Constantes.MensagensExcecao.ALUNO_INEXISTENTE);
                return (null, null, null);
            }
            var professor = await _usuarioRepository.BuscarProfessor(registrarNotaAluno.ProfessorId);

            if(professor is null)
            {
                _contextoNotificacao.Add(Constantes.MensagensExcecao.PROFESSOR_INEXISTENTE);
                return (aluno, null, null);
            }
            var disciplina = await  _disciplinaRepository.BuscarDisciplinaPorAtividadeId(registrarNotaAluno.AtividadeId);
            
            if(disciplina is null)
            {
                _contextoNotificacao.Add(Constantes.MensagensExcecao.DISCIPLINA_INEXISTENTE);
                return (aluno, professor, null);
            }

            return (aluno, professor, disciplina);
        }

        private async Task<(Aluno aluno, Professor professor, Disciplina disciplina)> BuscarAlunoProfessorDisciplina(RegistrarNotaAluno registrarNotaAluno)
        {
            var aluno = await _usuarioRepository.BuscarAluno(registrarNotaAluno.AlunoId) 
                        ?? throw new DomainException(Constantes.MensagensExcecao.ALUNO_INEXISTENTE);
            var professor = await _usuarioRepository.BuscarProfessor(registrarNotaAluno.ProfessorId) 
                        ?? throw new DomainException(Constantes.MensagensExcecao.PROFESSOR_INEXISTENTE);
            var disciplina = await  _disciplinaRepository.BuscarDisciplinaPorAtividadeId(registrarNotaAluno.AtividadeId) 
                        ?? throw new DomainException(Constantes.MensagensExcecao.DISCIPLINA_INEXISTENTE);

            return (aluno, professor, disciplina);
        }

        private void AdicionarMensagensErroNoContexto(RegistrarNotaAluno registrarNotaAluno) =>
            _contextoNotificacao.AddRange(registrarNotaAluno.Validacoes.Errors.Select(x => x.ErrorMessage));

    }