using Microsoft.Extensions.DependencyInjection;
using Queene.Core.Converters;
using Queene.Core.Magics;
using Queene.Core.MovesGenerating.Hashing;

namespace Queene.Core
{
    public static class QueeneIocModule
    {
        public static void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMagicsBinaryPersisterService, MagicsBinaryPersisterService>();
            serviceCollection.AddSingleton<IBoardStateConverter, BoardStateToFenConverter>();
            serviceCollection.AddSingleton<IBitBoardContextConverter, BitBoardContextConverter>();
            serviceCollection.AddSingleton<ZorbistHash>();
            serviceCollection.AddSingleton<IQueeneGame, Game>();
        }
    }
}
