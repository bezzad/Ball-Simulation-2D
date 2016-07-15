using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Balls
{
    public partial class MainForm : Form
    {
        private const int _Max_X_Speed = 50;
        private const int _Max_Y_Speed = 50;
        private Random rand;

        public MainForm()
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            InitializeComponent();

            rand = new Random();

            SpeedBar.ValueChanged += (source, ea) => { board.Interval = 100 - SpeedBar.Value; };
        }

        private void btnNewObj_Click(object sender, EventArgs e)
        {
            board.Start();

            var ball = new Ball(new Speed(rand.Next(0, _Max_X_Speed), rand.Next(0, _Max_Y_Speed)));
            ball.Radius = rand.Next(1, 60);
            int x = rand.Next(ball.Radius + _Max_X_Speed, ball.Radius + _Max_X_Speed + 20);
            int y = rand.Next(ball.Radius + _Max_Y_Speed, ball.Radius + _Max_Y_Speed + 20);
            ball.Position = new Point(x, y);


            this.board.DrawBall(ball);

            this.lblCount.Text = Ball.BallCollection.Count.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            board.Stop();
        }

    }
}
