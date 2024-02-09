using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
    public abstract class Mensagem
    {
        public DateTime MensagemCriada { get; set; }
        public Mensagem()
        {
            // MensagemCriada = DateTime.Now;
        }

        public virtual bool MensagemEstaValida()
        {
            throw new NotImplementedException();
        }
    }