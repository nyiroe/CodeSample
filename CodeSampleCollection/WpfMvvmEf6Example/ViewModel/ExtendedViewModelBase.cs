using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmEf6Example.ViewModel
{
    public abstract class ExtendedViewModelBase : ViewModelBase, IDataErrorInfo
    {
        public event EventHandler Closed;

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        protected abstract string Validate(string propertyName);

        protected virtual void Close()
        {
            if (Closed != null)
                Closed(this, EventArgs.Empty);
        }
    }
}
