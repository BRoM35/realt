using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtOn
{
    public partial class ObjectFilter : Form
    {   public string filtr;
        public Dadata.Model.Suggestion<Dadata.Model.Address>[] array;
        public ObjectFilter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConstructQuery();

            //Form ifrm = new Objects(filtr);
           // ifrm.Show(); // отображаем Form1
           // this.Close();
            // richTextBox1.Text =  dadata.SuggestAddress(textBox1.Text.ToString()).ToString();
           
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
          

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           /*  if (comboBox1.Text.ToString() != "")
            {
                var dadata = new SuggestClientDadata();
                IList<Dadata.Model.Suggestion<Dadata.Model.Address>> array = dadata.SuggestAddress(comboBox1.Text.ToString());

                comboBox1.Items.Clear();

                for (int i = 0; i < array.Count; i++)
                {
                    comboBox1.Items.Add(array[i].value);

                }
            }
            */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString() != "")
            {
                var dadata = new SuggestClientDadata();
                array = dadata.SuggestAddress(textBox1.Text.ToString()).ToArray();

                listBox1.Items.Clear();
                listBox1.Visible = true;
                for (int i = 0; i < array.Count() ; i++)
                {
                    listBox1.Items.Add(array[i].value);

                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = !listBox1.Visible;
        }
        private void ConstructQuery()
        {
            List<string> params_f = new List<string>();

            if (Renovation.CheckedIndices.Count > 0)
                params_f.Add("renovation in(" + string.Join(",", Renovation.CheckedIndices.Cast<int>().ToArray().Select((x => x + 1))) + ")");
            if (Status.CheckedIndices.Count > 0)
                params_f.Add("Objects.status in(" + string.Join(",", Status.CheckedIndices.Cast<int>().ToArray().Select((x => x + 1))) + ")");
            if (Otype.CheckedIndices.Count > 0)
                params_f.Add("Objects.type in(" + string.Join(",", Otype.CheckedIndices.Cast<int>().ToArray().Select((x => x + 1))) + ")");
            if (Wall.CheckedIndices.Count > 0)
                params_f.Add("Objects.wall in(" + string.Join(",", Wall.CheckedIndices.Cast<int>().ToArray().Select((x => x + 1))) + ")");

            filtr = "where " + string.Join(" and ", params_f.ToArray());
            richTextBox1.Text = filtr;
        }
            private void ObjectFilter_Load(object sender, EventArgs e)
        {

          
            for (int i = 1; i < 4; i++) //ремонт
            {
                Renovation.Items.Add(Tools.GetDescription((Object.Renovation)i));
            }

            for (int i = 1; i < 5; i++) //стены
            {
                Wall.Items.Add(Tools.GetDescription((Object.Wall)i));
            }

            for (int i = 1; i <3; i++) //сТАТус
            {
                Status.Items.Add(Tools.GetDescription((Object.Status)i));
            }
            for (int i = 1; i < 5; i++) //сТАТус
            {
                Otype.Items.Add(Tools.GetDescription((Object.OType)i));
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  richTextBox1.Clear();
            // foreach (var index in Renovation.CheckedIndices)
            // Renovation.CheckedIndices;
           

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form ifrm = new Objects();
            ifrm.Show(); // отображаем Form1
           // this.Close(); // закрываем Form2 (this - текущая форма)
                          // не используйте данный способ, правильный ниже
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new SuggestClientDadata().AddDB(array[0]);
        }
    }
}
