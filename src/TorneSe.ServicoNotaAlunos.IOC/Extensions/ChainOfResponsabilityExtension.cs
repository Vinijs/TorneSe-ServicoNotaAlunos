using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;

namespace TorneSe.ServicoNotaAlunos.IOC.Extensions;
public static class ChainOfResponsabilityExtension
{
    public static IServiceCollection AdicionarEncadeamento<TService, TRequest>
                                            (this IServiceCollection services, 
                                             params Type[] tiposImplementacao)
    where TService : IHandler<TRequest>
    where TRequest : IRequest
    {
        if(tiposImplementacao.Any())
            throw new ArgumentException(nameof(tiposImplementacao));
        
        foreach (Type tipo in tiposImplementacao)
        {
            services.AddScoped(tipo);
        }

        services.AddTransient(typeof(TService), provider => 
        {
            var services = provider.GetServices(tiposImplementacao);

            for (int i = 0; i < services.Count; i++)
            {
                if (services.Count > i + 1)
                {
                    ((IHandler<TRequest>)services[i]).SetNext((IHandler<TRequest>)services[i + 1]);
                }
            }
            return services.First();
        });


        return services;
    }

    public static IServiceCollection AdicionarEncadeamentoAssincrono<TService, TRequest>
                                            (this IServiceCollection services, 
                                             params Type[] tiposImplementacao)
    where TService : IAsyncHandler<TRequest>
    where TRequest : IRequest
    {
        if(tiposImplementacao.Any())
            throw new ArgumentException(nameof(tiposImplementacao));
        
        foreach (Type tipo in tiposImplementacao)
        {
            services.AddScoped(tipo);
        }

        services.AddTransient(typeof(TService), provider => 
        {
            var services = provider.GetServices(tiposImplementacao);

            for (int i = 0; i < services.Count; i++)
            {
                if (services.Count > i + 1)
                {
                    ((IAsyncHandler<TRequest>)services[i]).SetNext((IAsyncHandler<TRequest>)services[i + 1]);
                }
            }
            return services.First();
        });


        return services;
    }
}