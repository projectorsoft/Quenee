using Microsoft.Extensions.DependencyInjection;
using Queene.Core;
using System;
using System.Runtime.InteropServices;

namespace Quenee.ConsoleApp
{
    public class Startup
    {
        private readonly IServiceCollection _serviceCollection;

        public IServiceProvider ServiceProvider { get; private set; }

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(SetConsoleCtrlEventHandler handler, bool add);
        private delegate bool SetConsoleCtrlEventHandler(CtrlType sig);

        public Startup()
        {
            _serviceCollection = new ServiceCollection();

            SetConsoleCtrlHandler(ConsoleExitHandler, true);
        }

        public void ConfigureServices()
        {
            QueeneIocModule.RegisterServices(_serviceCollection);

            ServiceProvider = _serviceCollection.BuildServiceProvider(true);
        }

        public void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }
            if (ServiceProvider is IDisposable)
            {
                ((IDisposable)ServiceProvider).Dispose();
            }
        }

        private bool ConsoleExitHandler(CtrlType signal)
        {
            switch (signal)
            {
                case CtrlType.CTRL_BREAK_EVENT:
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                    Console.WriteLine("Closing app");

                    DisposeServices();
                    Environment.Exit(0);
                    return false;

                default:
                    return false;
            }
        }
    }

    enum CtrlType
    {
        CTRL_C_EVENT = 0,
        CTRL_BREAK_EVENT = 1,
        CTRL_CLOSE_EVENT = 2,
        CTRL_LOGOFF_EVENT = 5,
        CTRL_SHUTDOWN_EVENT = 6
    }
}
