using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TorneSe.ServicoNotaAlunos.Domain.Notification;
public class ContextoNotificacao : IReadOnlyCollection<string>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly Collection<string> _notificacoes = new Collection<string>();

    public bool TemNotificacoes => _notificacoes.Any();

    public int Count => _notificacoes.Count;

    public void Add(string notificacao) => _notificacoes.Add(notificacao);

    public IEnumerator<string> GetEnumerator() => _notificacoes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _notificacoes.GetEnumerator();

    public void AddRange(IEnumerable<string> notifications)
    {
        foreach (var i in notifications)
            _notificacoes.Add(i);
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(_notificacoes, _jsonSerializerOptions);
    }
}