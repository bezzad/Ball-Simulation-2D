using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Balls
{
    public class Board : Panel
    {
        private System.Windows.Forms.Timer timer;

        public int Interval
        {
            get { return timer.Interval; }
            set { if (value > 0) timer.Interval = value; }
        }

        public Board()
        {
            timer = new Timer();
            timer.Interval = 40;
            timer.Tick += timer_Tick;

            this.BackColor = Color.White;
            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.BorderStyle = BorderStyle.Fixed3D;
        }

        void timer_Tick(object sender, System.EventArgs e)
        {
            this.ClearAll();

            foreach (Ball ball in Ball.BallCollection)
            {
                ball.MoveToNextPosition(this.ClientSize);
                DrawBall(ball);
            }
        }

        public void DrawBall(Ball ball)
        {
            using (var myBrush = new System.Drawing.SolidBrush(ball.FillColor))
            {
                using (System.Drawing.Graphics formGraphics = this.CreateGraphics())
                {
                    formGraphics.FillEllipse(myBrush, new Rectangle(ball.Location, ball.Size));
                }
            }
        }

        protected void ClearAll()
        {
            using (System.Drawing.Graphics formGraphics = this.CreateGraphics())
            {
                formGraphics.Clear(Color.White);
            }
        }

        public void Start()
        {
            if(!timer.Enabled) timer.Start();
        }

        public void Stop()
        {
            if (timer.Enabled) timer.Stop();
        }
    }
}
