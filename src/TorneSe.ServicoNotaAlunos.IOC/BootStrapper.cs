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
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers.Interfaces;
using TorneSe.ServicoNotaAlunos.Domain.DomainObjects;
using TorneSe.ServicoNotaAlunos.IOC.Extensions;
using TorneSe.ServicoNotaAlunos.Domain.Validations.Handlers;

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
            RegistrarEncadeamentos(services);
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

        private static void RegistrarEncadeamentos(IServiceCollection services)
        {
            services.AdicionarEncadeamento<IHandler<ServicoNotaValidacaoRequest>, ServicoNotaValidacaoRequest>
            (typeof(AlunoValidacaoHandler),typeof(ProfessorValidacaoHandler), typeof(DisciplinaValidacaoHandler));

            services.AdicionarEncadeamentoAssincrono<IAsyncHandler<ServicoNotaValidacaoRequest>, ServicoNotaValidacaoRequest>
            (typeof(AlunoRequestBuildHandler), typeof(ProfessorRequestBuildHandler), typeof(DisciplinaRequestBuildHandler));
        } 

    }