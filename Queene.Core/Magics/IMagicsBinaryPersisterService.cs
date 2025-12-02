using Queene.Core.Enums;

namespace Queene.Core.Magics
{
    public interface IMagicsBinaryPersisterService
    {
        MagicResult[] LoadMagics(SliderTypeEnum sliderType);

        void SaveMagics(MagicResult[] magics, SliderTypeEnum sliderType);
    }
}
