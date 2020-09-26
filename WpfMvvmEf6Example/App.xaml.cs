using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace WpfMvvmEf6Example
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            base.OnStartup(e);
        }
    }
}
