
using Autofac;
using FileService.Acu.Helper;

namespace FileService.Autofac
{
    public class FileServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<PathBuilder>();
        }
    }
}