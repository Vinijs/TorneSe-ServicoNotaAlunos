using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
using Xunit;
using Moq;
using FluentAssertions;
using TorneSe.ServicoNotaAlunos.Domain.Services;
using TorneSe.ServicoNotaAlunos.UnitTests.Fakes;
using TorneSe.ServicoNotaAlunos.Domain.Utils;
namespace TorneSe.ServicoNotaAlunos.UnitTests.Domain.Services;
public class ServicoValidacaoNotaAlunoTests
{
    private readonly ContextoNotificacao _contextoNotificacao = new();
    private readonly Mock<IHandler<ServicoNotaValidacaoRequest>> _validacaoHandler;
    private readonly ServicoValidacaoNotaAluno _sut;
    public ServicoValidacaoNotaAlunoTests()
    {
        _validacaoHandler = new Mock<IHandler<ServicoNotaValidacaoRequest>>();
        _sut = new ServicoValidacaoNotaAluno(_contextoNotificacao, _validacaoHandler.Object);
    }

    [Fact(DisplayName = "Dado aluno inativo deve adicionar notificacao")]
    [Trait("Domain", "ServicoValidacaoNotaAluno")]
    public void DadoAluno_QuandoAlunoInativo_DeveAdicionarMensagemNoContexto()
    {
        //Arrange
        var aluno = AlunoFakes.RetornaAlunoInativo();
        var disciplina = DisciplinaFakes.RetornaDisciplinaValida();

        //Act
        _sut.ValidarLancamento(aluno, null, disciplina);

        //Assert
        _contextoNotificacao.TemNotificacoes.Should().BeTrue();
        _contextoNotificacao.Count.Should().NotBe(default(int));
        _contextoNotificacao.ToList().Should().Contain(Constantes.MensagensValidacao.ALUNO_INATIVO);
    }

    [Fact(DisplayName = "Dado professor inativo deve adicionar notificacao")]
    [Trait("Domain", "ServicoValidacaoNotaAluno")]
    public void DadoProfessor_QuandoProfessorInativo_DeveAdicionarMensagemNoContexto()
    {
        //Arrange
        var aluno = AlunoFakes.RetornaAluno();
        var professor = ProfessorFakes.RetornaProfessorInativo();
        var disciplina = DisciplinaFakes.RetornaDisciplinaValida();

        //Act
        _sut.ValidarLancamento(aluno, professor, disciplina);

        //Assert
        _contextoNotificacao.TemNotificacoes.Should().BeTrue();
        _contextoNotificacao.Count.Should().NotBe(default(int));
        _contextoNotificacao.ToList().Should().Contain(Constantes.MensagensValidacao.PROFESSOR_INATIVO);
    }

    [Fact(DisplayName = "Dada disciplina inativa deve adicionar notificacao")]
    [Trait("Domain", "ServicoValidacaoNotaAluno")]
    public void DadaDisciplina_QuandoDisciplinaInativa_DeveAdicionarMensagemNoContexto()
    {
        //Arrange
        var aluno = AlunoFakes.RetornaAluno();
        var professor = ProfessorFakes.RetornaProfessorValido();
        var disciplina = DisciplinaFakes.RetornaDisciplinaEncerrada();

        //Act
        _sut.ValidarLancamento(aluno, professor, disciplina);

        //Assert
        _contextoNotificacao.TemNotificacoes.Should().BeTrue();
        _contextoNotificacao.Count.Should().NotBe(default(int));
        _contextoNotificacao.ToList().Should().Contain(Constantes.MensagensValidacao.DISCIPLINA_INATIVA);
    }

     [Fact(DisplayName = "Quando solicitado validacao deve acionar handler")]
    [Trait("Domain", "ServicoValidacaoNotaAluno")]
    public void DadoRequest_QuandoInvocadoHandler_DeveExecutar()
    {
        //Arrange

        //Act
        _sut.ValidarLancamento(It.IsAny<ServicoNotaValidacaoRequest>());

        //Assert
        _validacaoHandler.Verify(x => x.Handle(It.IsAny<ServicoNotaValidacaoRequest>()));
    }
}