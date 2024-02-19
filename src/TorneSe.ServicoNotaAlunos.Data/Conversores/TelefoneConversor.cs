using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TorneSe.ServicoNotaAlunos.Domain.ValueObjects;

namespace TorneSe.ServicoNotaAlunos.Data.Conversores;
public class TelefoneConversor : ValueConverter<Telefone,string> 
{
    public static TelefoneConversor Instance => new TelefoneConversor();
    public TelefoneConversor(): base(x => ConverterTelefoneParaTexto(x), x => ConverterTextoParaTelefone(x))
    {        
    }

    private static string ConverterTelefoneParaTexto(Telefone telefone) =>
        telefone.ToString();

    private static Telefone ConverterTextoParaTelefone(string texto) =>
        new Telefone(texto);

}
