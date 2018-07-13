namespace MongoService.Infrastructure.AutofacModules
{
    using System.Reflection;
    using Autofac;
    using EventHandlers;
    using Microservices.EventBus.Abstractions;
    using Mongo.Infrustructure.Repositories;

    public class ApplicationModule
        : Autofac.Module
    {
        public ApplicationModule(string qconstr)
        {
            this.QueriesConnectionString = qconstr;
        }

        public string QueriesConnectionString { get; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageingDataRepository>()
                .As<IMessageingDataRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BasicIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
