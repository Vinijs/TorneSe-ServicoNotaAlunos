using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Data.Context;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;


namespace TorneSe.ServicoNotaAlunos.Data.Repositories;
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly FakeDbContexto _contexto;
        public DisciplinaRepository(FakeDbContexto contexto)
        {
            _contexto = contexto;            
        }
        public IUnitOfWork UnitOfWork => _contexto;

        public async Task<Disciplina> BuscarDisciplinaPorAtividadeId(int atividadeId) =>
            await Task.FromResult(_contexto.Disciplinas.FirstOrDefault(x => x.Conteudos.SelectMany(y => y.Atividades).Any(y => y.Id == atividadeId)));

        public void Dispose()
        {
            _contexto?.Dispose();
        }
        
    }