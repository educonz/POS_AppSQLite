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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppWithSqlLite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddPage : Page
    {
        private Pessoa _pessoa = null;

        public AddPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                _pessoa = (Pessoa)e.Parameter;
                NomeTextBox.Text = _pessoa.Nome;
                FoneTextBox.Text = _pessoa.Fone;
            }
            base.OnNavigatedTo(e);
        }

        private async void AdicionarPessoaButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelper db_helper = new DatabaseHelper();
            
            if (!string.IsNullOrEmpty(NomeTextBox.Text) & !string.IsNullOrEmpty(FoneTextBox.Text))
            {
                if (_pessoa == null)
                {
                    db_helper.Insert(new Pessoa(NomeTextBox.Text, FoneTextBox.Text));
                    var message = new MessageDialog("Incluído com sucesso!");
                    await message.ShowAsync();
                }
                else
                {
                    _pessoa.Nome = NomeTextBox.Text;
                    _pessoa.Fone = FoneTextBox.Text;
                    db_helper.UpdateDetails(_pessoa);
                    var message = new MessageDialog("Alterado com sucesso!");
                    await message.ShowAsync();
                }
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Preencher os campos");
                await messageDialog.ShowAsync();
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
