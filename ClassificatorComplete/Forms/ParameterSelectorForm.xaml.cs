using ClassificatorComplete.Forms.ViewModels;
using ClassificatorComplete.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassificatorComplete.Forms
{
    /// <summary>
    /// Логика взаимодействия для ParameterSelectorForm.xaml
    /// </summary>
    public partial class ParameterSelectorForm : Window
    {
        public List<MyParameter> mparams;
        public ParamNameItem paramItem;
        public ParamValueItem valueItem;

        public ParameterSelectorForm(List<MyParameter> mparams, ParamNameItem paramItem)
        {
#if Revit2020
            Owner = ModuleData.RevitWindow;
#endif
#if Revit2018
            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.Owner = ModuleData.MainWindowHandle;
#endif

            InitializeComponent();
            this.mparams = mparams;
            this.Collection.ItemsSource = this.mparams;
            this.paramItem = paramItem;
        }

        public ParameterSelectorForm(List<MyParameter> mparams, ParamValueItem valueItem)
        {
            InitializeComponent();
            this.mparams = mparams;
            this.Collection.ItemsSource = this.mparams;
            this.valueItem = valueItem;
        }

        private void Accept_ParamName_Click(object sender, RoutedEventArgs e)
        {
            MyParameter parameter = (MyParameter)Collection.SelectedItem;
            if (valueItem == null)
            {
                paramItem.paramName = parameter.Name;
            }
            else if (paramItem == null)
            {
                var regex = new Regex(Regex.Escape("[]"));
                valueItem.paramValue = regex.Replace(valueItem.paramValue, string.Format("[{0}]", parameter.Name), 1);
            }
            this.Close();
        }
    }
}
