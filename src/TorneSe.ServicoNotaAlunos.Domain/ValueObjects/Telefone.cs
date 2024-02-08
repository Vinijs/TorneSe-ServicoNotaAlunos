using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneSe.ServicoNotaAlunos.Domain.ValueObjects;
    public class Telefone
    {
        public string Numero { get; set; }
        public string Area { get; set; }
        public string CodigoPais { get; set; }

        public override string ToString()
        {
            return $"{CodigoPais} {Area} {Numero}";
        }        
    }
