using CefSharp.MinimalExample.Wpf.Mvvm;
using CefSharp.Wpf;
using System.ComponentModel;
using System.Windows;

namespace CefSharp.MinimalExample.Wpf.ViewModels
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        class BoundObject { }

        private IWpfWebBrowser webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return webBrowser; }
            set
            {
                webBrowser = value;
                if (value == null)
                    return;

                WebBrowser.RegisterJsObject("bound", new BoundObject());
                WebBrowser.LoadHtml(@"
                    <html>
                    <head>
                    <script>
                        function updateLoop()
                        {
                            var infoDiv = document.getElementById('info');
                            infoDiv.innerText = window.bound ? 'SUCCESS' : 'not bound';
                            window.requestAnimationFrame(updateLoop);
                        }

                        window.requestAnimationFrame(updateLoop);
                    </script>
                    </head>
                    <body>
                    <div class='box' style='background-color:white; width:500px;height:500px;'>
                        <div id='info'></div>
                    </div>
                    </body>
                    </html>", "http://example.com/");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { PropertyChanged.ChangeAndNotify(ref title, value, () => Title); }
        }

        public MainViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                Application.Current.MainWindow.Title = "CefSharp.MinimalExample.Wpf - " + Title;
            }
        }
    }
}
