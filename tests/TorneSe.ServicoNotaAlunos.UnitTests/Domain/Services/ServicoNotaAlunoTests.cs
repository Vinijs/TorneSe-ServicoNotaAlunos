using FluentAssertions;
using Moq;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Services;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
using TorneSe.ServicoNotaAlunos.UnitTests.Fakes;

namespace TorneSe.ServicoNotaAlunos.UnitTests.Domain.Services;
public class ServicoNotaAlunoTests
{
    private readonly ContextoNotificacao _contextoNotificacao = new();
    private readonly Mock<IUsuarioRepository> _usuarioRepository;
    private readonly Mock<IDisciplinaRepository> _disciplinaRepository;
    private readonly Mock<IServicoValidacaoNotaAluno> _servicoValidacaoNotaAluno;
    private readonly Mock<IAsyncHandler<ServicoNotaValidacaoRequest>> _requestBuildHandler;
    private ServicoNotaAluno _sut;

    public ServicoNotaAlunoTests()
    {
        _disciplinaRepository = new Mock<IDisciplinaRepository>();
        _usuarioRepository = new Mock<IUsuarioRepository>();
        _servicoValidacaoNotaAluno = new Mock<IServicoValidacaoNotaAluno>();
        _requestBuildHandler = new Mock<IAsyncHandler<ServicoNotaValidacaoRequest>>();
        _sut = new ServicoNotaAluno(_contextoNotificacao, _usuarioRepository.Object, 
                                    _disciplinaRepository.Object, _servicoValidacaoNotaAluno.Object,
                                    _requestBuildHandler.Object);
    }

    [Fact(DisplayName = "Dada mensagem invalida deve adicionar notificacao no contexto")]
    [Trait("Domain","ServicoNotaAluno")]
    public async Task DadaMensagem_QuandoMensagemInvalida_DeveAdicionarNotificacao()
    {
        // Arrange
        var mensagem = RegistrarNotaAlunoFakes.RetornaMensagemInvalida();
        _disciplinaRepository.Setup(x => x.ConectadoAoBanco()).ReturnsAsync(true);
    
        // Act
        _sut.LancarNota(mensagem);

        // Assert
        _contextoNotificacao.TemNotificacoes.Should().BeTrue();
        _contextoNotificacao.Count.Should().NotBe(default(int));
    }
}