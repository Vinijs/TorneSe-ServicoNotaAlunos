using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
public class Aluno : Entidade
{
    public Aluno(int alunoId, string nomeAbreviado, string emailInterno, int usuarioId,
                 DateTime dataCadastro)
    {
        Id = alunoId;
        NomeAbreviado = nomeAbreviado;
        EmailInterno = emailInterno;
        UsuarioId = usuarioId;
        DataCadastro = dataCadastro;
        Notas = new List<Nota>();
        AlunosTurmas = new List<AlunosTurmas>();
    }

    protected Aluno() { }

    public string NomeAbreviado { get; private set; }
    public string EmailInterno { get; private set; }
    public int UsuarioId { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public Usuario Usuario { get; private set; }

    public ICollection<Nota> Notas { get; private set; }
    public ICollection<AlunosTurmas> AlunosTurmas { get; private set; }

    public void AdicionarNotas(Nota nota)
    {
        //Validar a Nota
        Notas.Add(nota);
    }
}