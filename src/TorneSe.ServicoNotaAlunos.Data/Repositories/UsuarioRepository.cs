using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Data.Context;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;

namespace TorneSe.ServicoNotaAlunos.Data.Repositories;
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FakeDbContexto _contexto;
        public UsuarioRepository(FakeDbContexto contexto)
        {
            _contexto = contexto;
        }

        public IUnitOfWork UnitOfWork => _contexto;

        public async Task<Aluno> BuscarAluno(int alunoId) =>
            await Task.FromResult(_contexto.Alunos.FirstOrDefault(x => x.Id == alunoId));

        public async Task<Professor> BuscarProfessor(int professorId) =>
            await Task.FromResult(_contexto.Professores.FirstOrDefault(x => x.Id == professorId));

        public void Dispose()
        {
            _contexto?.Dispose();
        }
    }