using DotNetCoreWebApplicationSample.ServiceContracts;

namespace DotNetCoreWebApplicationSample.RepositoryContracts
{
    public class CustomLogger: ICustomLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
