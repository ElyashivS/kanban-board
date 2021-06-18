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
        private BoardVM boardvm;
        public KanbanMenu(UserModel userModel)
        {
            InitializeComponent();
            this.boardvm = new BoardVM(userModel);
            this.DataContext = boardvm;
            this.userModel = userModel;
        }

        private void AddBoardButton(object sender, RoutedEventArgs e)
        {
            AddBoard addBoard = new(userModel);
            boardvm.AddBoard(addBoard.addBoardvm);
            addBoard.Show();
            
        }

        private void JoinBoardButton(object sender, RoutedEventArgs e)
        {
            boardvm.JoinBoard();
        }

        private void DeleteBoardButton(object sender, RoutedEventArgs e)
        {
            boardvm.RemoveBoard();
        }

        private void LogoutButton(object sender, RoutedEventArgs e)
        {
            boardvm.Logout();
            Login login = new();
            login.Show();
            this.Close();
        }

        private void BoardDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (boardvm.SelectedBoard != null)
            {
                ColumnMenu columnMenu = new(userModel, boardvm.SelectedBoard);
                columnMenu.Show();
            }
        }
    }
}
