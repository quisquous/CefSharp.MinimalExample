using Advanced_Combat_Tracker;
using CefSharp;
using CefSharp.Wpf;
using System;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.Wpf
{
    public class ACTPlugin : IActPluginV1
    {
        MainWindow mainWindow = new MainWindow();

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            Cef.Initialize(new CefSettings(), true, true);
            mainWindow.Show();
        }

        public void DeInitPlugin()
        {
            mainWindow.Hide();

            // FIXME: This needs to be called from the right thread, so it can't happen automatically.
            // However, calling it here means the plugin can never be reinitialized, oops.
            Cef.Shutdown();
        }
    }
}
