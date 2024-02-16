using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
public class Nota: Entidade
{
    public Nota(int alunoId,int atividadeId, double valorNota,DateTime dataLancamento,
                int usuarioId)
    {
        AlunoId = alunoId;
        AtividadeId = atividadeId;
        ValorNota = valorNota;
        dataLancamento = DataLancamento;
        UsuarioId = usuarioId;
    }

    protected Nota() { }

    public int AlunoId { get; private set; }
    public int AtividadeId { get; private set; }
    public double ValorNota { get; private set; }
    public DateTime DataLancamento { get; private set; }
    public int UsuarioId { get; private set; } //Usuario Sistemico
    public bool CanceladaPorRetentativa { get; private set; }
    public Aluno Aluno { get; set; }
    public Atividade Atividade { get; set; }
    public Usuario Usuario { get; set; }

    public void CancelarNotaPorRetentativa() => 
        CanceladaPorRetentativa = true;
}