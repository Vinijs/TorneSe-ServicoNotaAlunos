using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;

public class Professor : Entidade
{
    public Professor(int professorId, string nomeAbreviado, string emailInterno, bool professorTitular,
                     bool professorSuplente, int usuarioId, DateTime dataCadastro)
    {
        Id = professorId;
        NomeAbreviado = nomeAbreviado;
        EmailInterno = emailInterno;
        ProfessorTitular = professorTitular;
        ProfessorSuplente = professorSuplente;
        UsuarioId = usuarioId;
        DataCadastro = dataCadastro;
    }

    protected Professor() { }

    public string NomeAbreviado { get; private set; }
    public string EmailInterno { get; private set; }
    public bool ProfessorTitular { get; private set; }
    public bool ProfessorSuplente { get; private set; }
    public int UsuarioId { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public Usuario Usuario { get; private set; }
    public Disciplina Disciplina { get; private set; }
}
