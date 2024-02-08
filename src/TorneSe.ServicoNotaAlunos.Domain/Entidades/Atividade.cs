using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.Enums;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
public class Atividade : Entidade
{
    public Atividade(string descricao,TipoAtividade tipoAtividade, DateTime dataAtividade,
                     DateTime dataCadastro, bool possuiRetentativa)
    {
        Descricao = descricao;
        TipoAtividade = tipoAtividade;
        DataAtividade = dataAtividade;
        DataCadastro = dataCadastro;
        PossuiRetentativa = possuiRetentativa;
        Notas = new List<Nota>();
        
    }
    
    protected Atividade(){ }

    public string Descricao { get; private set; }
    public TipoAtividade TipoAtividade { get; private set; }
    public DateTime DataAtividade { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public bool PossuiRetentativa { get; private set; }

    public Conteudo Conteudo { get; private set; }
    public ICollection<Nota> Notas { get; private set; }
}
