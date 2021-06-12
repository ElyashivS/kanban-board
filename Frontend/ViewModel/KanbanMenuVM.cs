using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class KanbanMenuVM
    {
        private UserModel userModel;
        private BackendController backendController;
        public KanbanMenuVM(UserModel userModel)
        {
            backendController = userModel.Controller;
            this.userModel = userModel;
        }

        internal void AddBoard(AddBoardVM viewModel)
        {
            //TODO
        }
    }
}
