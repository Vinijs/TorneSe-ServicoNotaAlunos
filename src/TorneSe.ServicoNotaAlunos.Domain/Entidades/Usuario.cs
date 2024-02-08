using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TorneSe.ServicoNotaAlunos.Domain.ValueObjects;

namespace TorneSe.ServicoNotaAlunos.Domain.Entidades;
public class Usuario: Entidade
{
    public Usuario(string nome, string documentoIdentificacao, DateTime dataNascimento,
     bool ativo, Telefone telefoneContato, string email, bool administrativo, DateTime dataCadastro)
    {
        Nome = nome;
        DocumentoIdentificacao = documentoIdentificacao;
        DataNascimento = dataNascimento;
        Ativo = ativo;
        Email = email;
        TelefoneContato = telefoneContato;
        Administrativo = administrativo;
        DataCadastro = dataCadastro;

    }

    protected Usuario() { }
    public string Nome { get; private set; }
    public string DocumentoIdentificacao { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public bool Ativo { get; private set; }
    public string Email { get; private set; }
    public Telefone TelefoneContato { get; private set; }
    public bool Administrativo { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public Aluno Aluno { get; private set; }
    public Professor Professor { get; private set; }
}
