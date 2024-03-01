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
using TorneSe.ServicoNotaAlunos.Data.UnitOfWork;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context.Interfaces;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Context;
using TorneSe.ServicoNotaAlunos.MessageBus.SQS.Clients.Interfaces;
using TorneSe.ServicoNotaAlunos.IOC.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TorneSe.ServicoNotaAlunos.IOC;
    public static class BootStrapper
    {
        public static IServiceCollection ConfigurarInjecaoDependencia(this IServiceCollection services,
                                                                           IConfiguration configuration,
                                                                           IHostEnvironment hostEnvironment)
        {
            services
                .RegistrarServicos()
                .RegistrarContextos()
                .RegistrarRepositorios()
                .RegistrarFilas()
                .RegistrarContextoNotificacao()
                .RegistrarEncadeamentos()
                .RegistrarUnitofWork()
                .RegistrarContextoSqs()
                .RegistrarHealthChecks()
                .ConfigurarSerilog(configuration, hostEnvironment)
                .ConfigurarHealthChecks(configuration, hostEnvironment);
                
            return services;
        }

        private static IServiceCollection RegistrarServicos(this IServiceCollection services)
        {
            services.AddScoped<IServicoAplicacaoNotaAluno, ServicoAplicacaoNotaAluno>();
            services.AddScoped<IServicoNotaAluno, ServicoNotaAluno>();
            services.AddScoped<IServicoValidacaoNotaAluno, ServicoValidacaoNotaAluno>();
            services.AddScoped<INotaAlunoReceberMensagemService, NotaAlunoReceberMensagemService>();
            services.AddScoped<INotaAlunoRespostaMensagemService, NotaAlunoRespostaMensagemService>();
            return services;
        }

        private static IServiceCollection RegistrarContextos(this IServiceCollection services)
        {
            services.AddScoped<FakeDbContexto>();
            services.AddScoped<ServicoNotaAlunosContexto>();
            return services;
        }

        private static IServiceCollection RegistrarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }

        private static IServiceCollection RegistrarFilas(this IServiceCollection services)
        {
            services.AddScoped<ILancarNotaAlunoFakeClient, LancarNotaAlunoFakeClient>();
            services.AddScoped<ILancarNotaAlunoRecebimentoClient, LancarNotaAlunoRecebimentoClient>();
            services.AddScoped<ILancarNotaAlunoRespostaClient, LancarNotaAlunoRespostaClient>();
            return services;
        }

        private static IServiceCollection RegistrarContextoNotificacao(this IServiceCollection services)
        {
            services.AddScoped<ContextoNotificacao>();
            return services;
        }

        private static IServiceCollection RegistrarEncadeamentos(this IServiceCollection services)
        {
            services.AdicionarEncadeamento<IHandler<ServicoNotaValidacaoRequest>, ServicoNotaValidacaoRequest>
            (typeof(AlunoValidacaoHandler),typeof(ProfessorValidacaoHandler), typeof(DisciplinaValidacaoHandler));

            services.AdicionarEncadeamentoAssincrono<IAsyncHandler<ServicoNotaValidacaoRequest>, ServicoNotaValidacaoRequest>
            (typeof(AlunoRequestBuildHandler), typeof(ProfessorRequestBuildHandler), typeof(DisciplinaRequestBuildHandler));
            return services;
        }

        private static IServiceCollection RegistrarUnitofWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            return services;
        }

        private static IServiceCollection RegistrarContextoSqs(this IServiceCollection services)
        {
            return services.AddScoped<ISqsContext,SqsContext>();
        }

        private static IServiceCollection RegistrarHealthChecks(this IServiceCollection services)
        {
            return services.AddScoped<AwsSqsHealthCheck>();
        }



    }