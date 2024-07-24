using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Microsoft.Extensions.DependencyInjection;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction, INeed<IImageHolder>
    {
        private IImageHolder imageHolder;

        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            
            // создаём painter с такими настройками
            var services = new ServiceCollection();
            services.AddSingleton(imageHolder);
            services.AddSingleton(dragonSettings);
            services.AddSingleton<DragonPainter>();
            var sp = services.BuildServiceProvider();
            
            var painter = sp.GetService<DragonPainter>();
            
            painter.Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}