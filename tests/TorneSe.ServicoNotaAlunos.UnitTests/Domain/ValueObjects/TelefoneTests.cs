using Xunit;
using TorneSe.ServicoNotaAlunos.Domain.ValueObjects;

namespace TorneSe.ServicoNotaAlunos.UnitTests.Domain.ValueObjects;

public class TelefoneTests
{
    [Fact(DisplayName = "Telefone valido deve preencher propriedades")]
    [Trait("Domain", "Value Objects")]
    public void DadoTelefone_QuandoInstanciadoComTextoFormatoValido_DevePreencherPropriedades()
    {
        //Arrange
        var textoTelefoneValido = "55 11 999999999";

        //Act
        var telefone = new Telefone(textoTelefoneValido);

        //Assert
        Assert.NotEmpty(telefone.Numero);
        Assert.NotEmpty(telefone.Area);
        Assert.NotEmpty(telefone.CodigoPais);
    }

    [Fact(DisplayName = "Telefone invalido propriedades devem ser nulas")]
    [Trait("Domain", "Value Objects")]
    public void DadoTelefone_QuandoInstanciadoComTextoNulo_DeveDeixarPropriedadesNulas()
    {
        //Arrange
        var textoTelefoneValido = (string)null;

        //Act
        var telefone = new Telefone(textoTelefoneValido);

        //Assert
        Assert.Null(telefone.Numero);
        Assert.Null(telefone.Area);
        Assert.Null(telefone.CodigoPais);
    }

    [Fact(DisplayName = "Telefone valido deve retornar texto formatado")]
    [Trait("Domain", "Value Objects")]
    public void DadoTelefone_QuandoSolicitadoValorEmTexto_DeveRetornarTextoFormatado()
    {
        //Arrange
        var textoTelefoneValido = "55 11 999999999";
        var telefone = new Telefone(textoTelefoneValido);

        //Act
        var telefoneEmTexto = telefone.ToString();
        

        //Assert
        Assert.NotNull(telefoneEmTexto);
        Assert.NotEmpty(telefoneEmTexto);
        Assert.True(telefoneEmTexto.Length > 0);
        Assert.Contains(telefone.Numero, telefoneEmTexto);
        Assert.Equal(telefoneEmTexto, textoTelefoneValido);
    }

    [Fact(DisplayName = "Telefone invalido deve retornar texto vazio")]
    [Trait("Domain", "Value Objects")]
    public void DadoTelefone_QuandoSolicitadoValorEmTexto_DeveRetornarTextoVazio()
    {
        //Arrange
        var textoTelefoneValido = (string)null;
        var telefone = new Telefone(textoTelefoneValido);

        //Act
        var telefoneEmTexto = telefone.ToString();
        

        //Assert
        Assert.NotNull(telefoneEmTexto);
        Assert.NotEmpty(telefoneEmTexto);
        Assert.Equal("  ", telefoneEmTexto);
    }
    

}