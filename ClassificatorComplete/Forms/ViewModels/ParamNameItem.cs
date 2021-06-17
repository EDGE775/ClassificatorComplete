using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete.Forms.ViewModels
{
    public class ParamNameItem : BaseViewModel
    {
        private string _paramName { get; set; }
        public string paramName
        {
            get
            {
                return _paramName;
            }
            set
            {
                _paramName = value;
                NotifyPropertyChanged();
            }
        }
        private int _paramNumber { get; set; }
        public int paramNumber
        {
            get
            {
                return _paramNumber;
            }
            set
            {
                _paramNumber = value;
                NotifyPropertyChanged();
            }
        }
        public ParamNameItem(string paramName)
        {
            this.paramName = paramName;
        }
    }
}
