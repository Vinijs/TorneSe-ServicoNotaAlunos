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
        }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataCadastro { get; set; }

        public Turma Turma { get; set; }
        public Aluno Aluno { get; set; }
    }