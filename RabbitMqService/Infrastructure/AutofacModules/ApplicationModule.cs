namespace RabbitMqService.Infrastructure.AutofacModules
{
    using System.Reflection;
    using Autofac;
    using EventHandlers;
    using Microservices.EventBus.Abstractions;

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
            //builder.RegisterType<OrderRepository>()
            //    .As<IOrderRepository>()
            //    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BasicEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
