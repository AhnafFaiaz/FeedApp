using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IMapper;
using FeedApp.Core.Interfaces.IRepositories;
using FeedApp.Core.Interfaces.IServices;
using FeedApp.Core.Mapper;
using FeedApp.Core.Services;
using FeedApp.Infra.Repositories;
using FeedApp.Core.Entities.Business;

namespace FeedApp.UI.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IProblemService, ProblemService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ISigninService, SigninService>();
            services.AddScoped<ISignupService, SignupService>();
            services.AddScoped<IEmailService, EmailService>();


            #endregion

            #region Repositories
            services.AddTransient<IProblemRepository, ProblemRepository>();
            services.AddTransient<IDashboardRepository, DashboardRepository>();
            services.AddTransient(typeof(ISigninRepository<>), typeof(SigninRepository<>));
            services.AddTransient(typeof(ISignupRepository<>), typeof(SignupRepository<>));

            #endregion

            #region Mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Problems, ProblemViewModel>();
                cfg.CreateMap<ProblemViewModel, Problems>();
            });

            IMapper mapper = configuration.CreateMapper();

            // Register the IMapperService implementation with your dependency injection container
            services.AddSingleton<IBaseMapper<Problems, ProblemViewModel>>(new BaseMapper<Problems, ProblemViewModel>(mapper));
            services.AddSingleton<IBaseMapper<ProblemViewModel, Problems>>(new BaseMapper<ProblemViewModel, Problems>(mapper));

            #endregion

            return services;
        }
    }
}
