using Frontend.Model;
using Frontend.ViewModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private LoginVM loginvm;
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginVM();
            this.loginvm = (LoginVM)DataContext;
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            UserModel userModel = loginvm.Login();
            if (userModel != null)
            {
                KanbanMenu kanbanMenu = new KanbanMenu(userModel);
                kanbanMenu.Show();
                this.Close();
            }
        }

        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            loginvm.Register();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
