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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace todolist
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Task> _list = new List<Task>();

        public MainPage()
        {
            this.InitializeComponent();
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;


            foreach (var task in localSettings.Containers.Values)
            {
                _list.Add(new Task { Id = (string)task.Values["Id"], Name = (string)task.Values["Name"], Content = (string)task.Values["Content"], Date = (DateTimeOffset)task.Values["Time"], State = (bool)task.Values["Check"] });
            }

            TaskList.ItemsSource = _list;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _list.Clear();

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            foreach (var task in localSettings.Containers.Values)
            {
                _list.Add(new Task { Id = (string)task.Values["Id"], Name = (string)task.Values["Name"], Content = (string)task.Values["Content"], Date = (DateTimeOffset)task.Values["Time"], State = (bool)task.Values["Check"] });
            }

            TaskList.ItemsSource = _list;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            this.Frame.Navigate(typeof(CreateForm), null);
        }

        private async void EditTask(object sender, TappedRoutedEventArgs e)
        {
            var task = TaskList.SelectedItem as Task;
            if (task != null)
                this.Frame.Navigate(typeof(EditTask), task);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            foreach (var task in _list)
            {
                localSettings.Containers[task.Id].Values["Check"] = task.State;
            }
        }
    }
}
