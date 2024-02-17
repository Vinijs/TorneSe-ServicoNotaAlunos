using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Data.Context;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TorneSe.ServicoNotaAlunos.Data.Repositories;
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FakeDbContexto _contexto;
        private readonly ServicoNotaAlunosContexto _servicoNotaAlunosContexto;
        public UsuarioRepository(FakeDbContexto contexto, 
                                 ServicoNotaAlunosContexto servicoNotaAlunosContexto)
        {
            _contexto = contexto;
            _servicoNotaAlunosContexto = servicoNotaAlunosContexto;
        }

        public IUnitOfWork UnitOfWork => _servicoNotaAlunosContexto;

        public async Task<Aluno> BuscarAlunoDb(int alunoId) =>
            await Task.FromResult(_servicoNotaAlunosContexto.Alunos.FirstOrDefault(x => x.Id == alunoId));

        public async Task<Aluno> BuscarAluno(int alunoId) =>
            await Task.FromResult(_contexto.Alunos.FirstOrDefault(x => x.Id == alunoId));

        public async Task<Professor> BuscarProfessorDb(int professorId) =>
            await Task.FromResult(_servicoNotaAlunosContexto.Professores.FirstOrDefault(x => x.Id == professorId));

        public async Task<Professor> BuscarProfessor(int professorId) =>
            await Task.FromResult(_contexto.Professores.FirstOrDefault(x => x.Id == professorId));

        public void Dispose()
        {
            _contexto?.Dispose();
            _servicoNotaAlunosContexto?.Dispose();
        }
    }