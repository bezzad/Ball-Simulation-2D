using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.VisualBasic.PowerPacks;

namespace Balls
{
    public partial class MainForm : Form
    {
        private const int _Max_X_Speed = 50;
        private const int _Max_Y_Speed = 50;
        private Random rand;

        private System.Windows.Forms.Timer timer;

        public MainForm()
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            InitializeComponent();

            rand = new Random();

            SpeedBar.ValueChanged += (source, ea) => { timer.Interval = 100 - SpeedBar.Value; };

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 40;
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            foreach (Ball ball in Ball.BallCollection)
            {
                ball.MoveToNextPosition(board.ClientSize);
            }
        }

        private void btnNewObj_Click(object sender, EventArgs e)
        {
            if (!timer.Enabled) timer.Start();

            var ball = new Ball(new Speed(rand.Next(0, _Max_X_Speed), rand.Next(0, _Max_Y_Speed)));
            ball.Radius = rand.Next(1, 100);
            int x = rand.Next(ball.Radius + _Max_X_Speed, ball.Radius + _Max_X_Speed + 20);
            int y = rand.Next(ball.Radius + _Max_Y_Speed, ball.Radius + _Max_Y_Speed + 20);
            ball.Position = new Point(x, y);


            this.board.Add(ball);

            this.lblCount.Text = Ball.BallCollection.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}
