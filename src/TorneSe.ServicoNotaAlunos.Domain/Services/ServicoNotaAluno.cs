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
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;

namespace TorneSe.ServicoNotaAlunos.Domain.Services;
    public class ServicoNotaAluno : IServicoNotaAluno
    {
        private readonly ContextoNotificacao _contextoNotificacao;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly ServicoValidacaoNotaAluno _servicoValidacaoNotaAluno;
        private readonly IAsyncHandler<ServicoNotaValidacaoRequest> _requestBuildHandler;
        public ServicoNotaAluno(ContextoNotificacao contextoNotificacao, IUsuarioRepository usuarioRepository,
                                IDisciplinaRepository disciplinaRepository, ServicoValidacaoNotaAluno servicoValidacaoNotaAluno,
                                IAsyncHandler<ServicoNotaValidacaoRequest> requestBuildHandler)
        {
            _contextoNotificacao = contextoNotificacao;
            _usuarioRepository = usuarioRepository;
            _disciplinaRepository = disciplinaRepository;
            _servicoValidacaoNotaAluno = servicoValidacaoNotaAluno;
            _requestBuildHandler = requestBuildHandler;
        }

        public async Task LancarNota(RegistrarNotaAluno registrarNotaAluno)
        {
            Console.WriteLine("Processando lógica de negócio....");

            var conectado = await _disciplinaRepository.ConectadoAoBanco();

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

            //var (aluno,professor,disciplina) = await BuscarAlunoProfessorDisciplina2(registrarNotaAluno);

            var request = await ConstruirRequest(registrarNotaAluno);

            if(_contextoNotificacao.TemNotificacoes)
                return;

            // _servicoValidacaoNotaAluno.ValidarLancamento(aluno,professor,disciplina);

            _servicoValidacaoNotaAluno.ValidarLancamento(request);

            if(_contextoNotificacao.TemNotificacoes)
                return;

            if(AlunoPossuiNotaParaCancelar(request.Aluno, registrarNotaAluno.AtividadeId, registrarNotaAluno.NotaSubstitutiva))
            {
                RemoverNotaAluno(request.Aluno, registrarNotaAluno.AtividadeId);
            }

            var nota = new Nota(request.AlunoId, request.AtividadeId, registrarNotaAluno.ValorNota,
                            DateTime.Now, 101020);

            request.Aluno.AdicionarNotas(nota);
                
        }

        private void RemoverNotaAluno(Aluno aluno, int atividadeId)
        {
            var nota = aluno.Notas.FirstOrDefault(x => x.AtividadeId == atividadeId);
            nota.CancelarNotaPorRetentativa();
        }

        private bool AlunoPossuiNotaParaCancelar(Aluno aluno, int atividadeId, bool notaSubstitutiva) =>
            aluno.Notas.Any(x => x.AtividadeId == atividadeId && notaSubstitutiva);

        private async Task<ServicoNotaValidacaoRequest> ConstruirRequest(RegistrarNotaAluno registrarNotaAluno)
        {
            var request = ServicoNotaValidacaoRequest.Instance;
            request.AlunoId = registrarNotaAluno.AlunoId;
            request.ProfessorId = registrarNotaAluno.ProfessorId;
            request.AtividadeId = registrarNotaAluno.AtividadeId;

            // var initialHandler = new AlunoRequestBuildHandler(_contextoNotificacao, _usuarioRepository);
            // initialHandler.SetNext(new ProfessorRequestBuildHandler(_contextoNotificacao, _usuarioRepository))
            //                         .SetNext(new DisciplinaRequestBuildHandler(_contextoNotificacao, _disciplinaRepository));

            // await initialHandler.Handle(request);

            await _requestBuildHandler.Handle(request);

            request.Professor.AlterarNome("Danillloo");

            return request;
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