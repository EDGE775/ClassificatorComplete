using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassificatorComplete.Forms
{
    class GroupBoxRules
    {
        private GroupBox key;
        private List<GroupBox> values;

        public GroupBoxRules(GroupBox key, List<GroupBox> values)
        {
            this.key = key;
            this.values = values;
        }

        public GroupBoxRules(GroupBox key)
        {
            this.key = key;
            values = new List<GroupBox>();
        }

        public GroupBox getKey()
        {
            return key;
        }

        public List<GroupBox> getValues()
        {
            return values;
        }

        public int valuesCount()
        {
            return values.Count;
        }

        //public void addValue(GroupBox value)
        //{
        //    values.Add(value);
        //}

        //public void addValueByIndex(int index, GroupBox value)
        //{
        //    values.Insert(index, value);
        //}

        //public void deleteValueByIndex(int index)
        //{
        //    values.RemoveAt(index);
        //}
    }
}
