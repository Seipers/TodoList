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
    public sealed partial class EditTask : Page
    {
        Task task;
        public EditTask()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            task = (Task)e.Parameter;
            if (task.Content == null)
                Content.Text = "";
            else
                Content.Text = task.Content;
            Title.Text = task.Name;
            Date.Date = task.Date.Date;
            Time.Time = new TimeSpan(task.Date.Hour, task.Date.Minute, task.Date.Second);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            var time = new DateTime(Date.Date.Year, Date.Date.Month, Date.Date.Day, Time.Time.Hours, Time.Time.Minutes, Time.Time.Seconds);
            var tmp = new DateTimeOffset(time);

            localSettings.Containers[task.Id].Values["Name"] = Title.Text;
            localSettings.Containers[task.Id].Values["Content"] = Content.Text;
            localSettings.Containers[task.Id].Values["Time"] = tmp;
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.DeleteContainer(task.Id);
            this.Frame.Navigate(typeof(MainPage), null);
        }
    }
}
