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
    /// Interaction logic for AssignTask.xaml
    /// </summary>
    public partial class AssignTask : Window
    {
        private UserModel userModel;
        private BoardModel boardModel;
        private ColumnModel columnModel;
        private TaskModel taskModel;
        internal AssignTaskVM assignTaskvm;
        public AssignTask(UserModel userModel, BoardModel boardModel, ColumnModel columnModel, TaskModel taskModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boardModel = boardModel;
            assignTaskvm = new(userModel, boardModel, columnModel, taskModel);
            DataContext = assignTaskvm;
        }

        private void AssignButton(object sender, RoutedEventArgs e)
        {
            if (assignTaskvm.AddAssign() == true)
            {
                this.Close();
            }
        }
    }
}
