using Microsoft.Extensions.DependencyInjection;


namespace InterfaceDll
{
    public interface InterfaceDllClass
    {
    

      string GetActionName();




        void Configure(ServiceCollection service);

    }

    public interface IMyService
    {
        void Execute();
        string Teste();
    }
}
