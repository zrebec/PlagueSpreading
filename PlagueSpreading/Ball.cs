using System;
using System.Drawing;
using System.Security.Cryptography;

namespace PlagueSpreading
{
    class Ball
    {
        private const int ballWidth = 15;
        private const int ballHeight = 15;
        public double radius = (double)(ballWidth / 2) + (double)(ballHeight / 2);
        private int canvasWidth;
        private int canvasHeight;
        public int ballX, ballY; //Position
        public int ballVx, ballVy; //Velocity
        public bool affected = false;
        private RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        public void init(int canvasWidth, int canvasHeight)
        {
            ballVx = RandomInteger(1, 6);
            ballVy = RandomInteger(1, 6);
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            ballX = RandomInteger(0, this.canvasWidth - ballWidth);
            ballY = RandomInteger(0, this.canvasHeight - ballHeight);
            Console.WriteLine("X, Y, vX, vY: " + ballX + ", " + ballY + ", " + ballVx + ", " + ballVy);
        }

        public void update()
        {
            ballX += ballVx;
            if (ballX < 0)
            {
                ballVx = -ballVx;   
            }
            else if (ballX + ballWidth > canvasWidth)
            {
                ballVx = -ballVx;
            }

            ballY += ballVy;
            if (ballY < 0)
            {
                ballVy = -ballVy;
            }
            else if (ballY + ballHeight > canvasHeight)
            {
                ballVy = -ballVy;
            }
        }

        public void updateBall(Ball b1, Ball b2)
        {
            if (b1.ballX < b2.ballX)
            {
                ballVx = -ballVx;
            }
            else if (ballX + ballWidth > canvasWidth)
            {
                ballVx = -ballVx;
            }

            ballY += ballVy;
            if (ballY < 0)
            {
                ballVy = -ballVy;
            }
            else if (ballY + ballHeight > canvasHeight)
            {
                ballVy = -ballVy;
            }
        }

        public void paint(System.Windows.Forms.PaintEventArgs e)
        {
            if(affected)
                e.Graphics.FillEllipse(Brushes.Red, ballX, ballY, ballWidth, ballHeight);
            else
                e.Graphics.FillEllipse(Brushes.Blue, ballX, ballY, ballWidth, ballHeight);
        }

        // Return a random integer between a min and max value.
        private int RandomInteger(int min, int max)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);

                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
        }

    }
}
