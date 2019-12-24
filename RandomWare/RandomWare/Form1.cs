using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomWare
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            StringBuilder text = new StringBuilder();
            text.Append(@"{\rtf1\ansi\ansicpg949\deff0\nouicompat\deflang1033\deflangfe1042{\fonttbl{\f0\fnil\fcharset129 \'b8\'bc\'c0\'ba \'b0\'ed\'b5\'f1;}}
{\*\generator Riched20 10.0.18362}\viewkind4\uc1 
\pard\sl240\slmult1\b\f0\fs20\lang1042\'b3\'bb \'c4\'c4\'c7\'bb\'c5\'cd\'bf\'a1 \'b9\'ab\'bd\'bc \'c0\'cf\'c0\'cc \'c0\'cf\'be\'ee\'b3\'b5\'b3\'aa\'bf\'e4?\par
\b0\'b4\'e7\'bd\'c5\'c0\'ba Randomware\'bf\'a1 \'b0\'a8\'bf\'b0\'b5\'c7\'be\'fa\'bd\'c0\'b4\'cf\'b4\'d9.\par
\'c1\'df\'bf\'e4\'c7\'d1 \'c6\'c4\'c0\'cf\'b5\'e9\'c0\'cc \'be\'cf\'c8\'a3\'c8\'ad\'b5\'c7\'be\'fa\'bd\'c0\'b4\'cf\'b4\'d9.\par
\'b9\'ae\'bc\'ad, \'bb\'e7\'c1\'f8, \'b5\'bf\'bf\'b5\'bb\'f3 \'b1\'e2\'c5\'b8 \'c6\'c4\'c0\'cf\'b5\'e9\'c0\'cc \'b8\'f0\'b5\'ce \'be\'cf\'c8\'a3\'c8\'ad \'b5\'c7\'be\'ee \'be\'d7\'bc\'bc\'bd\'ba \'c7\'d2 \'bc\'f6 \'be\'f8\'bd\'c0\'b4\'cf\'b4\'d9.\par
\par
\b\'c6\'c4\'c0\'cf\'c0\'bb \'ba\'b9\'c8\'a3\'c8\'ad \'c7\'d2 \'bc\'f6 \'c0\'d6\'bd\'c0\'b4\'cf\'b1\'ee?\b0\par
\'b3\'d7, \'b9\'d8 CLICK \'b9\'f6\'c6\'b0\'c0\'bb \'b4\'ad\'b7\'af \'bc\'bc \'bc\'fd\'c0\'da\'b0\'a1 \'b8\'f0\'b5\'ce \'b0\'b0\'c0\'ba \'bc\'f6\'b0\'a1 \'b3\'aa\'bf\'c0\'b8\'e9 \'b5\'cb\'b4\'cf\'b4\'d9.\lang18\par
}");
            richTextBox1.Rtf = text.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text =random.Next(1,10).ToString();
            label3.Text = random.Next(1, 10).ToString();
            label4.Text = random.Next(1, 10).ToString();

            if(label2.Text.Equals(label3.Text)&&label3.Text.Equals(label4.Text))
            {
                MessageBox.Show("세 숫자가 같습니다.");
            }
        }
    }
}
