using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.Utils;
public static class Constantes
{
    public static class MensagensAplicacao
    {
        public const string SEM_MENSAGEM_NA_FILA = "Não possui mensagens a ser processadas na fila...";
        public const string INICIANDO_SERVICO = "Iniciando o serviço de notas";
    }

    public static class MensagensExcecao
    {
        public const string ALUNO_INEXISTENTE = "O aluno informado não existe";
        public const string PROFESSOR_INEXISTENTE = "O professor informado não existe";
        public const string DISCIPLINA_INEXISTENTE = "A atividade informada não possui uma disciplina";
    }
}