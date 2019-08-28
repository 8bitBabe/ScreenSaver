using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class FormScSaver : Form
    {
        List<Image> BGImages = new List<Image>();
        List<BritPic> BritPics = new List<BritPic>();
        Random rand = new Random();

        class BritPic
        {
            public int PicNum;
            public int x;
            public int y;
            public float speed;
        } //details for position and speed of the pictures 

        public FormScSaver()
        {
            InitializeComponent();
        }

        private void FormScSaver_KeyDown(object sender, KeyEventArgs e)
        {
            //while ScSaver is running, if a key is pressed it closes
            Close();
        }

        private void FormScSaver_Load(object sender, EventArgs e)
        {
            //
            string[] images = System.IO.Directory.GetFiles("pics");

            foreach (string image in images)
            {
                //creates new bitmap image from the pics folder and adds it to the array
                BGImages.Add(new Bitmap(image));
            }

            for (int i = 0;  i < 50; ++i)
            {
                //it'll keep adding images randomly to the screen till it reaches 50
                BritPic mp = new BritPic();
                mp.PicNum = i % BGImages.Count;
                mp.x = rand.Next(0, Width);
                mp.y = rand.Next(0, Height);
                mp.speed = rand.Next(100, 300) / 100.0f;

                BritPics.Add(mp);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void FormScSaver_Paint(object sender, PaintEventArgs e)
        {
            foreach (BritPic bp in BritPics)
            {
                //works positioning 
                e.Graphics.DrawImage(BGImages[bp.PicNum], bp.x, bp.y);
                bp.x -= 2;

                //wrapping effect?
                if (bp.x < -250)
                {
                    bp.x = Width + rand.Next(20, 100);
                }
            }
        }
    }
}
