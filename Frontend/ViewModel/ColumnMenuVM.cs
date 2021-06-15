using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class ColumnMenuVM : Notifiable
    {
        private UserModel userModel;
        private BoardModel boardModel;
        private BackendController backendController;
        public ColumnMenuVM()
        {

        }
        public ColumnMenuVM(UserModel userModel, BoardModel boardModel)
        {
            this.backendController = userModel.backendController;
            this.userModel = userModel;
            this.boardModel = boardModel;
        }
    }
}
