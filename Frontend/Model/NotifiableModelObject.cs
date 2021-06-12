using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public abstract class NotifiableModelObject : Notifiable
    {
        public BackendController backendController { get; private set; }
        protected NotifiableModelObject(BackendController controller)
        {
            this.backendController = controller;
        }
    }
}
