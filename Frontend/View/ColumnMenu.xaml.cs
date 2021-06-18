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
    /// Interaction logic for ColumnManu.xaml
    /// </summary>
    public partial class ColumnMenu : Window
    {
        private UserModel userModel;
        private BoardModel boardModel;
        private ColumnMenuVM columnMenuvm;

        private void AddColumnButton(object sender, RoutedEventArgs e)
        {
            AddColumn addColumn = new(userModel, boardModel);
            columnMenuvm.AddColumn(addColumn.addColumnvm);
            addColumn.Show();
        }

        private void MoveColumnButton(object sender, RoutedEventArgs e)
        {
            MoveColumn moveColumn = new(userModel, boardModel, columnMenuvm.SelectedColumn);
            columnMenuvm.MoveColumn(moveColumn.moveColumnvm);
            moveColumn.Show();
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            columnMenuvm.RemoveColumn();
        }
        public ColumnMenu(UserModel userModel, BoardModel boardModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boardModel = boardModel;
            this.columnMenuvm = new ColumnMenuVM(userModel, boardModel);
            this.DataContext = columnMenuvm;
        }
    }
}
