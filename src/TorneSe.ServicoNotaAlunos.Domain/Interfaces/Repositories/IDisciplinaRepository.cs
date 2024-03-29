using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Entidades;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<Disciplina> BuscarDisciplinaPorAtividadeId(int atividadeId);
        Task<Disciplina> BuscarDisciplinaPorAtividadeIdDb(int atividadeId);
        Task<bool> ConectadoAoBanco();
    }