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
        public const string NAO_HOUVE_MUDANCAS_NO_PROCESSAMENTO = "Não foi modificado nada nesse envio";
    }

    public static class MensagensExcecao
    {
        public const string ALUNO_INEXISTENTE = "O aluno informado não existe";
        public const string PROFESSOR_INEXISTENTE = "O professor informado não existe";
        public const string DISCIPLINA_INEXISTENTE = "A atividade informada não possui uma disciplina";
    }

     public static class MensagensValidacao
    {
        public const string ALUNO_INATIVO = "O aluno não está ativo para receber notas";
        public const string ALUNO_NAO_ESTA_MATRICULADO = "O aluno não está matriculado na disciplina para receber nota em uma atividade";
        public const string PROFESSOR_INATIVO = "O professor não está ativo para lançar notas";
        public const string PROFESSOR_NAO_MINISTRA_A_DISCIPLINA = "O professor não ministra a disciplina para lançar nota";
        public const string PROFESSOR_DEVE_SER_TITULAR = "O professor para lançar notas deve ser titular da disciplina";
        public const string DISCIPLINA_TIPO_ENCONTRO = "Uma disciplina do tipo encontro não pode recber notas";
        public const string DISCIPLINA_INATIVA = "A disciplina está fora do periodo de lançamentos de notas";
    }
}