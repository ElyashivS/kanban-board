using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class ColumnMenuModel : NotifiableModelObject
    {
        private readonly UserModel userModel;
        private readonly BoardModel boardModel;
        public ObservableCollection<ColumnModel> Columns { get; set; }
        public ColumnMenuModel(BackendController backendController, UserModel userModel, BoardModel boardModel) : base(backendController)
        {
            this.userModel = userModel;
            this.boardModel = boardModel;
            //Columns = new ObservableCollection<ColumnModel>(backendController.GetAllColumn(userModel, boardModel));
           // Columns.CollectionChanged += HandleChange;
        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardModel y in e.OldItems)
                {
                    backendController.RemoveBoard(userModel.Email, y.Creator, y.BoardName);
                }
            }
        }
        public void RemoveColumn(ColumnModel columnModel)
        {
            Columns.Remove(columnModel);
        }
        public void AddColumn(ColumnModel columnModel)
        {
            Columns.Add(columnModel);
        }
    }
}
