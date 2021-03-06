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
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        private UserModel userModel;
        private BoardModel boardModel;
        internal AddTaskVM addTaskvm;

        public AddTask(UserModel userModel, BoardModel boardModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boardModel = boardModel;
            addTaskvm = new(userModel, boardModel);
            DataContext = addTaskvm;
        }

        private void AddTaskButton(object sender, RoutedEventArgs e)
        {
            if (addTaskvm.AddTask() == true)
            {
                this.Close();
            }
        }
    }
}
