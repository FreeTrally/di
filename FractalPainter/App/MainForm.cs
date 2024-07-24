using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Microsoft.Extensions.DependencyInjection;

namespace FractalPainting.App
{
    public class MainForm : Form
    {
        public MainForm()
            : this(
                new IUiAction[]
                {
                    new SaveImageAction(),
                    new DragonFractalAction(),
                    new KochFractalAction(),
                    new ImageSettingsAction(),
                    new PaletteSettingsAction()
                })
        {
        }

        public MainForm(IEnumerable<IUiAction> actions)
        {
            var actionsArray = actions.ToArray();
            
            var imageSettings = CreateSettingsManager().Load().ImageSettings;
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actionsArray.ToMenuItems());
            Controls.Add(mainMenu);

            var pictureBox = new PictureBoxImageHolder();
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            DependencyInjector.Inject<IImageHolder>(actionsArray, pictureBox);
            DependencyInjector.Inject<IImageDirectoryProvider>(actionsArray, CreateSettingsManager().Load());
            DependencyInjector.Inject<IImageSettingsProvider>(actionsArray, CreateSettingsManager().Load());
            DependencyInjector.Inject(actionsArray, new Palette());
        }

        private static SettingsManager CreateSettingsManager()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IObjectSerializer, XmlObjectSerializer>();
            services.AddSingleton<IBlobStorage, FileBlobStorage>();
            services.AddSingleton<SettingsManager>();

            var sp = services.BuildServiceProvider();
            var settingsManager = sp.GetService<SettingsManager>();

            return settingsManager;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Fractal Painter";
        }
    }
}