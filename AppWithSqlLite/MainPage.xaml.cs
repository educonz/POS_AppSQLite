using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppWithSqlLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            CarregarDados();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPage));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListaPessoa.SelectedItem != null)
            {
                Frame.Navigate(typeof(AddPage), (Pessoa)ListaPessoa.SelectedItem);
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListaPessoa.SelectedItem != null)
            {
                MessageDialog messageDialog = new MessageDialog("Deseja excluir?");
                messageDialog.Commands.Add(new UICommand("Sim") { Id = 1 });
                messageDialog.Commands.Add(new UICommand("Não") { Id = 0 });
                messageDialog.DefaultCommandIndex = 0;
                messageDialog.CancelCommandIndex = 0;
                var result = await messageDialog.ShowAsync();
                if ((int)result.Id == 1)
                {
                    new DatabaseHelper().DeletePessoa((ListaPessoa.SelectedItem as Pessoa).Id);
                    var message = new MessageDialog("Excluído com sucesso");
                    await message.ShowAsync();
                    CarregarDados();
                }
            }
        }

        private void ListaPessoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnDelete.Visibility = Visibility.Visible;
            BtnEdit.Visibility = Visibility.Visible;
        }

        private void CarregarDados()
        {
            ListaPessoa.ItemsSource = new DatabaseHelper().ReadAllPessoa();
        }
    }
}
