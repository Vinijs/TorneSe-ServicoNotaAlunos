using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.ValueObjects;
public class Telefone
{
    public Telefone(string texto)
    {
        if (!string.IsNullOrEmpty(texto) && texto.Split(" ").Count() == 3)
        {
            CodigoPais = texto.Split(" ")[0];
            Area = texto.Split(" ")[1];
            Numero = texto.Split(" ")[2];
        }
    }
    public string Numero { get; set; }
    public string Area { get; set; }
    public string CodigoPais { get; set; }

    public override string ToString()
    {
        return $"{CodigoPais} {Area} {Numero}";
    }
}
