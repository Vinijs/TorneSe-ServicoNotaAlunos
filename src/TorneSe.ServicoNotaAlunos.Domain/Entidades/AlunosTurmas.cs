using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
    public class AlunosTurmas
    {
        public AlunosTurmas(int alunoId,int turmaId, DateTime dataCadastro) 
        {
            AlunoId = alunoId;
            TurmaId = turmaId;
            DataCadastro = dataCadastro;
            Turmas = new List<Turma>();
            Alunos = new List<Aluno>();
        }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataCadastro { get; set; }

        public ICollection<Turma> Turmas { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
    }