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
    public class DisciplinaRepository : IDisciplinaRepository
    {
        private readonly FakeDbContexto _contexto;
        private readonly ServicoNotaAlunosContexto _servicoNotaAlunosContexto;
        public DisciplinaRepository(FakeDbContexto contexto, 
                                    ServicoNotaAlunosContexto servicoNotaAlunosContexto)
        {
            _contexto = contexto;            
            _servicoNotaAlunosContexto = servicoNotaAlunosContexto;
        }
        public IUnitOfWork UnitOfWork => _servicoNotaAlunosContexto;


        public async Task<bool> ConectadoAoBanco() =>
            await _servicoNotaAlunosContexto.Database.CanConnectAsync();

        public async Task<Disciplina> BuscarDisciplinaPorAtividadeId(int atividadeId) =>
            await Task.FromResult(_contexto.Disciplinas.FirstOrDefault(x => x.Conteudos.SelectMany(y => y.Atividades).Any(y => y.Id == atividadeId)));


         public async Task<Disciplina> BuscarDisciplinaPorAtividadeIdDb(int atividadeId) =>
            await _servicoNotaAlunosContexto.Disciplinas.FirstOrDefaultAsync(x => x.Conteudos.SelectMany(y => y.Atividades).Any(y => y.Id == atividadeId));

        public void Dispose()
        {
            _contexto?.Dispose();
            _servicoNotaAlunosContexto?.Dispose();
        }
        
    }