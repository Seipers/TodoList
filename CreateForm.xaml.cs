using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace todolist
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CreateForm : Page
    {
        public CreateForm()
        {
            this.InitializeComponent();
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (!localSettings.Values.ContainsKey("Id"))
                localSettings.Values["Id"] = 0;
        }

        private void CreateButton(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["Id"] = (int)localSettings.Values["Id"] + 1;
            int id = (int)localSettings.Values["Id"];

            localSettings.CreateContainer(id.ToString(), Windows.Storage.ApplicationDataCreateDisposition.Always);

            var time = new DateTime(Date.Date.Year, Date.Date.Month, Date.Date.Day, Time.Time.Hours, Time.Time.Minutes, Time.Time.Seconds);
            var tmp = new DateTimeOffset(time);

            localSettings.Containers[id.ToString()].Values["Id"] = id.ToString();
            localSettings.Containers[id.ToString()].Values["Name"] = Title.Text;
            localSettings.Containers[id.ToString()].Values["Content"] = Content.Text;
            localSettings.Containers[id.ToString()].Values["Time"] = tmp;
            localSettings.Containers[id.ToString()].Values["Check"] = false;
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private void BackToHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

    }
}
