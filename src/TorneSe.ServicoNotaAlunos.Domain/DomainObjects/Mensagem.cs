using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
    public abstract class Mensagem
    {
        [JsonIgnore]
        public DateTime MensagemCriada { get; protected set; }
        [JsonIgnore]
        public ValidationResult Validacoes { get; protected set; }
        public Mensagem()
        {
            // MensagemCriada = DateTime.Now;
        }

        public virtual bool MensagemEstaValida()
        {
            throw new NotImplementedException();
        }
    }