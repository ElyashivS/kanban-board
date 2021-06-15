using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class KanbanMenuVM : Notifiable
    {
        private UserModel userModel;
        private BackendController backendController;
        public KanbanMenuVM(UserModel userModel)
        {
            backendController = userModel.backendController;
            this.userModel = userModel;
        }

        internal void AddBoard(AddBoardVM viewModel)
        {
            //TODO
        }

        internal void Logout()
        {
            backendController.Logout(userModel.Email);
        }
    }
}
