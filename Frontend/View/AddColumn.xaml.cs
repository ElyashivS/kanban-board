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
    /// Interaction logic for AddColumn.xaml
    /// </summary>
    public partial class AddColumn : Window
    {
        private UserModel userModel;
        private BoardModel boardModel;
        internal AddColumnVM addColumnvm;
        public AddColumn(UserModel userModel, BoardModel boardModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boardModel = boardModel;
            addColumnvm = new(userModel, boardModel);
            DataContext = addColumnvm;
        }

        private void AddColumnButton(object sender, RoutedEventArgs e)
        {
            if (addColumnvm.AddColumn() == true)
            {
                this.Close();
            }
        }
    }
}
