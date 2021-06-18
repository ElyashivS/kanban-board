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
    /// Interaction logic for MoveColumn.xaml
    /// </summary>
    public partial class MoveColumn : Window
    {
        internal MoveColumnVM moveColumnvm;
        private UserModel userModel;
        private BoardModel boardModel;
        private ColumnModel columnModel;
        public MoveColumn(UserModel userModel, BoardModel boardModel, ColumnModel columnModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boardModel = boardModel;
            this.columnModel = columnModel;
            this.moveColumnvm = new MoveColumnVM(userModel, boardModel, columnModel);
            this.DataContext = moveColumnvm;
        }

        private void MoveButton(object sender, RoutedEventArgs e)
        {
            if (moveColumnvm.MoveColumn())
            {
                this.Close();
            }
        }
    }
}
