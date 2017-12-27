using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMVVM.ViewModels
{
    public class DialogYesNoViewModel : Screen
    {
        private string _Message;
        private string _Title;
        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }
        public DialogYesNoViewModel()
        {
           
        }

        public DialogYesNoViewModel(string Message,string Title)
        {
            this.Message = Message;
            this.Title = Title;
        }

        public void Ok()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }
    }
}
