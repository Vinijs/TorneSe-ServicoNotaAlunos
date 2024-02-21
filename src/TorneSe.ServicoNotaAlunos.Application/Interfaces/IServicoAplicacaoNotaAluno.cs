using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Messages;

namespace TorneSe.ServicoNotaAlunos.Application.Interfaces;
    public interface IServicoAplicacaoNotaAluno
    {
        Task ProcessarLancamentoNota();
    }