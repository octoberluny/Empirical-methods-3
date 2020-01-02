using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int HIT(string text)
        {
            int count = 0;
            Regex re = new Regex(@"\w+\s+extends\s+\w+");
            var mc = re.Matches(text);
            Hashtable hash = new Hashtable();
            foreach (Match m in mc)
            {
                string str = m.Value;
                string first = str.Substring(0, str.IndexOf("extends") - 1);
                string second = str.Substring(str.IndexOf("extends") + 8);
                hash.Add(first, second);
            }
            List<string> list = hash.Keys.Cast<string>().ToList();
            List<string> list2 = hash.Values.Cast<string>().ToList();
            list.AddRange(list2);
            for (int i = 0; i < list.Count; i++)
            {
                int counts = 0;
                string ins = list[i];
                while (true)
                {
                    if (hash.ContainsKey(ins))
                    {
                        ins = (string)hash[ins];
                        counts++;
                    }
                    else break;

                }
                if (counts > count) count = counts;
            }
            return count;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
           // open.Filter = "Java-файл (*.java) | *.java";
            open.ShowDialog();
            string path = open.FileName;
            StreamReader sr = new StreamReader(path);
            string s = sr.ReadToEnd();
            sr.Close();
            textBox1.Text = s;
            Regex re = new Regex(@"\w+\((\w*\s*\,*)*\)\w*\s*\n?\{");
            MatchCollection mc = re.Matches(s);
            int hit = HIT(s);
            textBox2.Text = "Кількість методів (NOM) - " + mc.Count.ToString();
            textBox2.Text += Environment.NewLine + "Глибина дерева наслідування (HIT) - " + hit.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}