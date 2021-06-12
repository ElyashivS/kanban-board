using Frontend.Model;
using Frontend.ViewModel;
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
    /// Interaction logic for KanbanMenu.xaml
    /// </summary>
    public partial class KanbanMenu : Window
    {
        private UserModel userModel;
        private KanbanMenuVM kanbanMenuvm;
        public KanbanMenu(UserModel userModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.kanbanMenuvm = new KanbanMenuVM(userModel);
            this.DataContext = kanbanMenuvm;
        }

        private void AddBoardButton(object sender, RoutedEventArgs e)
        {
            AddBoard addBoard = new AddBoard(userModel);
            kanbanMenuvm.AddBoard(addBoard.addBoardvm);
            addBoard.Show();
        }

        private void JoinBoardButton(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBoardButton(object sender, RoutedEventArgs e)
        {

        }

        private void ChangePasswordButton(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton(object sender, RoutedEventArgs e)
        {

        }

        private void TaskInProgressButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
