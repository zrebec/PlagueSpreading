using System;
using System.Windows.Forms;

namespace PlagueSpreading
{
    public partial class Form1 : Form
    {
        readonly Ball[] b = new Ball[20];
        private const int FPS = 30;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelStatus.Text = "I'm begining";
            // Pick a random start position and velocity
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new Ball();
                b[i].init(ClientSize.Width, ClientSize.Height);
                labelStatus.Text = "Initialization of ball " + i + " is done";
            }

            this.SetStyle(
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint |
             ControlStyles.OptimizedDoubleBuffer,
             true);
            this.UpdateStyles();
            timer1.Interval = (int) Math.Ceiling( (double) 1000 / FPS);
        }

        private double distance(Ball b1, Ball b2)
        {
            double x = (double) b1.ballX - b2.ballX;
            double y = (double)b1.ballY - b2.ballY;
            //Console.WriteLine("x, y: " + x + ", " + y);
            return Math.Sqrt(x * x + y * y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    if (j != i)
                    {
                        if (distance(b[i], b[j]) <= b[i].radius)
                        {
                            //TODO Think about path collision update. It is right to change vector only for i ball?
                            //In fact, both balls change the directrion
                            b[i].ballVx = -b[i].ballVx;
                            //b[j].ballVy = -b[j].ballVy;
                            b[j].ballVy = -b[j].ballVy;

                            // Set affected status
                            if (b[i].affected == true && b[j].affected == false)
                            {
                                labelStatus.Text = "Patient " + i + " infected person " + j;
                                b[j].affected = true;
                            }
                            else if (b[j].affected == true && b[i].affected == false)
                            {
                                labelStatus.Text = "Patient " + j + " infected person " + i;
                                b[i].affected = true;
                            }
                                
                        }
                    }
                }
                b[i].update();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);
            for (int i = 0; i < b.Length; i++)
            {
                if (i == 0) b[i].affected = true;
                b[i].paint(e);
                
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < b.Length; i++)
            {
                b[i].init(ClientSize.Width, ClientSize.Height);
            }
        }
    }
}
