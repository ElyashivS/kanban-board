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
    /// Interaction logic for AddBoard.xaml
    /// </summary>
    public partial class AddBoard : Window
    {
        private UserModel userModel;
        internal AddBoardVM addBoardvm;
        public AddBoard(UserModel userModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            addBoardvm = new AddBoardVM(userModel);
            DataContext = addBoardvm;
        }

        private void AddBoardButton(object sender, RoutedEventArgs e)
        {
            if (addBoardvm.AddBoard() == true)
            {
                this.Close();
            }
        }
    }
}
