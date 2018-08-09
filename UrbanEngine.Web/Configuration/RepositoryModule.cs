using Autofac;
using UrbanEngine.Infrastructure.Repository;

public class RepositoryModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<DbRepository>().As<IDbRepository>();
  }
}