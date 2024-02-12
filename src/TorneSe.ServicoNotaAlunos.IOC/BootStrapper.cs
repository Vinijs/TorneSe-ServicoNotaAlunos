using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TorneSe.ServicoNotaAlunos.Application.Interfaces;
using TorneSe.ServicoNotaAlunos.Application.Services;
using TorneSe.ServicoNotaAlunos.Data.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Repositories;
using TorneSe.ServicoNotaAlunos.Domain.Interfaces.Services;
using TorneSe.ServicoNotaAlunos.Domain.Services;
using TorneSe.ServicoNotaAlunos.Domain.Notification;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients;
using TorneSe.ServicoNotaAlunos.Data.Context;

namespace TorneSe.ServicoNotaAlunos.IOC;
    public static class BootStrapper
    {
        public static IServiceCollection ConfigurarInjecaoDependencia(this IServiceCollection services)
        {
            RegistrarServicos(services);
            RegistrarContextos(services);
            RegistrarRepositorios(services);
            RegistrarFilas(services);
            RegistrarContextoNotificacao(services);
            return services;
        }

        private static void RegistrarServicos(IServiceCollection services)
        {
            services.AddScoped<IServicoAplicacaoNotaAluno, ServicoAplicacaoNotaAluno>();
            services.AddScoped<IServicoNotaAluno, ServicoNotaAluno>();
        }

        private static void RegistrarContextos(IServiceCollection services)
        {
            services.AddScoped<FakeDbContexto>();
        }

        private static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        private static void RegistrarFilas(IServiceCollection services)
        {
            services.AddScoped<ILancarNotaAlunoFakeClient, LancarNotaAlunoFakeClient>();
        }

        private static void RegistrarContextoNotificacao(IServiceCollection services)
        {
            services.AddScoped<ContextoNotificacao>();
        } 

    }