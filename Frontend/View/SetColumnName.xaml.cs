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
    /// Interaction logic for SetColumnName.xaml
    /// </summary>
    public partial class SetColumnName : Window
    {
        private UserModel Usermodel;
        private BoardModel boardModel;
        private ColumnModel columnModel;
        internal SetColumnNameVM columnSetNamevm;
        public SetColumnName()
        {
            InitializeComponent();
        }

        private void SetButton(object sender, RoutedEventArgs e)
        {
            if (columnSetNamevm.SetColumnName() == true)
            {
                this.Close();
            }
        }
    }
}
