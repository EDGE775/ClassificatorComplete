﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuiltInCategory = Autodesk.Revit.DB.BuiltInCategory;

namespace ClassificatorComplete.Forms.ViewModels
{
    public class RuleItem : BaseViewModel
    {
        private string _colourOfRule { get; set; }
        public string colourOfRule
        {
            get
            {
                return _colourOfRule;
            }
            set
            {
                _colourOfRule = value;
                NotifyPropertyChanged();
            }
        }
        private string _familyNameValue { get; set; }
        public string familyNameValue
        {
            get
            {
                return _familyNameValue;
            }
            set
            {
                _familyNameValue = value;
                NotifyPropertyChanged();
                this.colourOfRule = "#ff6900";
            }
        }
        private string _familyNameTextBox { get; set; }
        public string familyNameTextBox
        {
            get
            {
                return _familyNameTextBox;
            }
            set
            {
                _familyNameTextBox = value;
                NotifyPropertyChanged();
            }
        }
        private string _typeNameValue { get; set; }
        public string typeNameValue
        {
            get
            {
                return _typeNameValue;
            }
            set
            {
                _typeNameValue = value;
                NotifyPropertyChanged();
                this.colourOfRule = "#ff6900";
            }
        }
        private string _typeNameTextBox { get; set; }

        public string typeNameTextBox
        {
            get
            {
                return _typeNameTextBox;
            }
            set
            {
                _typeNameTextBox = value;
                NotifyPropertyChanged();
            }
        }
        private BuiltInCategory _builtInCategoryName { get; set; }

        public BuiltInCategory builtInCategoryName
        {
            get
            {
                return _builtInCategoryName;
            }
            set
            {
                _builtInCategoryName = value;
                familyNameTextBox = builtInCategoryName.Equals(BuiltInCategory.OST_Rooms) ? "Параметр \"Назначение\" помещения содержит:" : "Имя семейства содержит:";
                typeNameTextBox = builtInCategoryName.Equals(BuiltInCategory.OST_Rooms) ? "Параметр \"Имя\" помещения содержит:" : "Имя типа содержит:";
                NotifyPropertyChanged();
                this.colourOfRule = "#ff6900";
            }
        }

        private int _ruleNumber { get; set; }
        public int ruleNumber
        {
            get
            {
                return _ruleNumber;
            }
            set
            {
                _ruleNumber = value;
                NotifyPropertyChanged();
            }
        }
        public string ruleNumberText
        {
            get
            {
                return "Правило " + _ruleNumber;
            }
        }
        public ObservableCollection<ParamValueItem> valuesOfParams { get; set; }
        public static List<BuiltInCategory> builtInCats { get; set; }

        public RuleItem(string familyNameValue, string typeNameValue, BuiltInCategory builtInCategoryName)
        {
            this.familyNameValue = familyNameValue;
            this.typeNameValue = typeNameValue;
            this.valuesOfParams = new ObservableCollection<ParamValueItem>();
            this.builtInCategoryName = builtInCategoryName;
        }

        public static void refreshNumbersOfValues(ObservableCollection<ParamValueItem> valuesOfParams)
        {
            for (int i = 0; i < valuesOfParams.Count; i++)
            {
                valuesOfParams[i].valueNumber = i + 1;
            }
        }

        public void addValueOfParam(string paramValue)
        {
            valuesOfParams.Add(new ParamValueItem(paramValue, this));
            this.colourOfRule = "#ff6900";
            refreshNumbersOfValues(valuesOfParams);
        }

        public void addValueOfParamByIndex(string paramValue, int index)
        {
            valuesOfParams.Insert(index, new ParamValueItem(paramValue, this));
            this.colourOfRule = "#ff6900";
            refreshNumbersOfValues(valuesOfParams);
        }

        public bool removeValueOfParam(ParamValueItem valueItem)
        {
            if (valuesOfParams.Remove(valueItem))
            {
                this.colourOfRule = "#ff6900";
                refreshNumbersOfValues(valuesOfParams);
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is RuleItem item &&
                   familyNameValue == item.familyNameValue &&
                   typeNameValue == item.typeNameValue &&
                   builtInCategoryName == item.builtInCategoryName &&
                   EqualityComparer<ObservableCollection<ParamValueItem>>.Default.Equals(valuesOfParams, item.valuesOfParams);
        }

        public override int GetHashCode()
        {
            int hashCode = -25894503;
            hashCode = hashCode * 31 + EqualityComparer<string>.Default.GetHashCode(familyNameValue);
            hashCode = hashCode * 31 + EqualityComparer<string>.Default.GetHashCode(typeNameValue);
            hashCode = hashCode * 31 + builtInCategoryName.GetHashCode();
            hashCode = hashCode * 31 + EqualityComparer<ObservableCollection<ParamValueItem>>.Default.GetHashCode(valuesOfParams);
            return hashCode;
        }

        public override string ToString()
        {
            return this.GetType().ToString() + " " + familyNameValue + " " + typeNameValue;
        }
    }
}
