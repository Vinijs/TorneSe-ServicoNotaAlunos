using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
public class Nota: Entidade
{
    public Nota(int alunoId,int atividadeId, double valor,DateTime dataLancamento,
                int usuarioId, bool canceladaPorRetentativa)
    {
        AlunoId = alunoId;
        AtividadeId = atividadeId;
        Valor = valor;
        dataLancamento = DataLancamento;
        UsuarioId = usuarioId;
        CanceladaPorRetentativa = canceladaPorRetentativa;
    }

    protected Nota() { }

    public int AlunoId { get; private set; }
    public int AtividadeId { get; private set; }
    public double Valor { get; private set; }
    public DateTime DataLancamento { get; private set; }
    public int UsuarioId { get; private set; } //Usuario Sistemico
    public bool CanceladaPorRetentativa { get; private set; }
    public Aluno ALuno { get; set; }
    public Atividade Atividade { get; set; }
}