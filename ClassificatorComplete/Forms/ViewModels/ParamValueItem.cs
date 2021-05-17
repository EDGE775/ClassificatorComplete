using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassificatorComplete.Forms.ViewModels
{
    public class ParamValueItem : BaseViewModel
    {
        public string _paramValue { get; set; }
        public string paramValue
        {
            get
            {
                return _paramValue;
            }
            set
            {
                _paramValue = value;
                NotifyPropertyChanged();
                if (parent != null)
                {
                    parent.colourOfRule = "#ff6900";
                }
            }
        }
        public RuleItem parent { get; set; }
        private int _valueNumber { get; set; }
        public int valueNumber
        {
            get
            {
                return _valueNumber;
            }
            set
            {
                _valueNumber = value;
                NotifyPropertyChanged();
            }
        }
        public string valueNumberText
        {
            get
            {
                return "Значение параметра " + _valueNumber;
            }
        }

        public ParamValueItem(string paramValue, RuleItem parent)
        {
            this.paramValue = paramValue;
            this.parent = parent;
        }

        public override bool Equals(object obj)
        {
            return obj is ParamValueItem item &&
                   paramValue == item.paramValue;
        }

        public override int GetHashCode()
        {
            int hashCode = 663154009;
            hashCode = hashCode * 31 + EqualityComparer<string>.Default.GetHashCode(paramValue);
            return hashCode;
        }

        public override string ToString()
        {
            return this.GetType().ToString() + " " + paramValue;
        }
    }
}
