using Autofac;
using UrbanEngine.Core.Interfaces;
using UrbanEngine.Infrastructure.Managers;

public class ManagersModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<UserManager>().As<IUserManager>();
  }
}