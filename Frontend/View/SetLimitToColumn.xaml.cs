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
    /// Interaction logic for SetLimitToColumn.xaml
    /// </summary>
    public partial class SetLimitToColumn : Window
    {
        public SetLimitToColumnVM setLimitToColumnvm;
        private BoardModel boardModel;
        private UserModel userModel;
        private ColumnModel columnModel;
        public SetLimitToColumn()
        {
            InitializeComponent();
        }

        

        public SetLimitToColumn(UserModel userModel, BoardModel boardModel, ColumnModel columnModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boardModel = boardModel;
            this.columnModel = columnModel;
            this.setLimitToColumnvm = new SetLimitToColumnVM(userModel, boardModel, columnModel);
            this.DataContext = setLimitToColumnvm;
        }

        private void SetButton(object sender, RoutedEventArgs e)
        {
            if (setLimitToColumnvm.SetLimitColumn())
            {
                this.Close();
            }
        }
    }
}
