using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
    public interface IUsuarioRepository : IRepository<Usuario> 
    {
        Task<Aluno> BuscarAluno(int alunoId);
        Task<Professor> BuscarProfessor(int professorId);
         Task<Aluno> BuscarAlunoDb(int alunoId);
        Task<Professor> BuscarProfessorDb(int professorId);
    }
