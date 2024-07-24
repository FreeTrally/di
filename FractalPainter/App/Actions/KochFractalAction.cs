using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Microsoft.Extensions.DependencyInjection;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction, INeed<IImageHolder>, INeed<Palette>
    {
        private IImageHolder imageHolder;
        private Palette palette;

        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }

        public void SetDependency(Palette dependency)
        {
            palette = dependency;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {
            var services = new ServiceCollection();
            services.AddSingleton(imageHolder);
            services.AddSingleton(palette);
            services.AddSingleton<KochPainter>();
            var sp = services.BuildServiceProvider();
            
            var painter = sp.GetService<KochPainter>();
            
            painter.Paint();
        }
    }
}