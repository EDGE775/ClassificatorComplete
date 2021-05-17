using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassificatorComplete.Forms
{
    public partial class ConfigurationForm : Form
    {
        private int rulesCount;
        ClassificatorForm classificatorForm;
        private List<GroupBoxRules> boxes;
        public ConfigurationForm(ClassificatorForm classificatorForm)
        {
            InitializeComponent();
            this.classificatorForm = classificatorForm;
            boxes = new List<GroupBoxRules>();
            boxes.Add(new GroupBoxRules(groupBox, new List<GroupBox>() {groupBoxValueField}));
            rulesCount = 1;
        }

        private void buttonAddElems_Click(object sender, EventArgs e)
        {
            rulesCount++;

            GroupBox groupBox = new GroupBox();
            groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
| System.Windows.Forms.AnchorStyles.Right)));
            groupBox.Location = new Point(boxes.Last().getKey().Location.X, boxes.Last().getKey().Location.Y + boxes.Last().getKey().Height + 10);
            groupBox.Name = "groupBox";
            groupBox.Size = new System.Drawing.Size(boxes.First().getKey().Width, 137);
            groupBox.TabIndex = 0;
            groupBox.TabStop = false;
            groupBox.Text = "Правило " + rulesCount;
            this.panelOfRules.Controls.Add(groupBox);


            Button button = new Button();
            button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            button.Location = new System.Drawing.Point(boxes.First().getKey().Controls.OfType<Button>().Where(x => x.Name.Contains("DeleteRule")).First().Location.X, 19);
            button.Name = "buttonDeleteRule";
            button.Size = new System.Drawing.Size(27, 112);
            button.TabIndex = 8;
            button.Text = "Х";
            button.UseVisualStyleBackColor = true;
            button.Click += new EventHandler(buttonDeleteRule_Click);
            groupBox.Controls.Add(button);
            boxes.Add(new GroupBoxRules(groupBox));
        }

        private void ConfigurationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            classificatorForm.Show();
        }

        private void buttonDeleteRule_Click(object sender, EventArgs e)
        {
            Button pushedButton = (Button)sender;
            Control groupBoxForDelete = pushedButton.Parent;
            if (groupBoxForDelete != null)
            {
                int deletedIndex = -1;
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].getKey().Equals(groupBoxForDelete))
                    {
                        deletedIndex = i;
                        break;
                    }
                }
                if (deletedIndex != -1)
                {
                    for (int i = deletedIndex + 1; i < boxes.Count; i++)
                    {
                        GroupBox currenBox = boxes[i].getKey();
                        int newPointY = currenBox.Location.Y - groupBoxForDelete.Height - 10;
                        currenBox.Location = new Point(currenBox.Location.X, newPointY);
                    }
                    boxes.RemoveAt(deletedIndex);
                    this.panelOfRules.Controls.Remove(groupBoxForDelete);
                    rulesCount--;
                }
                reOderRulesNumbers();
            }
        }

        private void reOderRulesNumbers()
        {
            int counter = 1;
            foreach (GroupBoxRules item in boxes)
            {
                string text = item.getKey().Text.Split(' ')[0];
                item.getKey().Text = string.Format("{0} {1}", text, counter++);
            }
        }

        private void buttonPlusValueField_Click(object sender, EventArgs e)
        {
            Button pushedButton = (Button)sender;
            GroupBox parentKey = (GroupBox) pushedButton.Parent.Parent;
            List<GroupBox> parentValues = boxes.Where(x => x.getKey().Equals(parentKey)).First().getValues();

            // 
            // groupBoxValueField
            // 
            GroupBox groupBox = new GroupBox();
            groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox.Location = new System.Drawing.Point(parentValues.Last().Location.X, parentValues.Last().Location.Y + parentValues.Last().Height + 5);
            groupBox.Name = "groupBoxValueField";
            groupBox.Size = new System.Drawing.Size(parentValues.Last().Width, parentValues.Last().Height);
            groupBox.TabIndex = 12;
            groupBox.TabStop = false;
            groupBox.Text = "Значение параметра " + parentValues.Count;

            parentKey.Controls.Add(groupBox);
            parentValues.Add(groupBox);

            if (parentKey.Height < parentValues.Sum(x => x.Height + 5))
            {
                parentKey.Height = parentKey.Height + groupBox.Height + 5;
            }
        }
    }
}
