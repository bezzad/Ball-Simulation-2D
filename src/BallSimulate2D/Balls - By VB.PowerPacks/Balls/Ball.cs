using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.PowerPacks;

namespace Balls
{
    public class Ball : Microsoft.VisualBasic.PowerPacks.OvalShape
    {
        #region Constructors
        public Ball() : this(new Speed(1, 1)) { }

        public Ball(Speed moveSpeed)
        {
            var rand = new Random();

            BallColor = Color.FromArgb(255, rand.Next(0, 210), rand.Next(0, 210), rand.Next(0, 210));

            ID = BallCollection.Count;

            Radius = this.Size.Height / 2;

            this.Speed = moveSpeed;

            Ball.BallCollection.Add(this);
        }

        #endregion

        #region Fields ------------------------------------------------

        private Color color;

        public static Color CollidColor = Color.Aqua;
        public static List<Ball> BallCollection = new List<Ball>();
        public static int Padding = 1;

        public Speed Speed;

        public int Radius
        {
            get { return Size.Height / 2; }
            set { Size = new Size(value * 2, value * 2); }
        }

        public int ID;

        public Color BallColor
        {
            set
            {
                this.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque;
                this.BackColor = value;
                this.BorderColor = value;
                color = value;
            }

            get { return color; }
        }

        //public LinerFormula MovementFormul;

        /// <summary>
        /// When you create sprites, 
        /// the top left part is set to the origin (0, 0) and the bottom right is (width, height). 
        /// Here, the circles we have created are centered on the sprite.
        /// 
        /// 
        ///                            Bad                                         Good
        ///                  ._____________________                        _____________________
        ///                 /|                     | |                    |          |          | |
        ///                / |                     | |                    |          |          | |
        /// Location (0, 0)  |                     | |                    |          |          | |
        ///                  |                     | | height             |----------.----------| | height
        ///                  |                     | |                    |         /|\         | |
        ///                  |                     | |                    | Position | (0, 0)   | |
        ///                  |_____________________| |                    |__________|__________| |
        ///                  <------- width ------->                      <------- width ------->
        /// 
        ///  Position = Point( Location.X + Width/2 , Location.Y + Height/2 )
        /// </summary>
        public Point Position
        {
            set { this.Location = new Point(value.X - this.Size.Width / 2, value.Y - this.Size.Height / 2); }

            get { return new Point(this.Location.X + this.Size.Width / 2, this.Location.Y + this.Size.Height / 2); }
        }

        #endregion ----------------------------------------------------

        #region Methods -----------------------------------------------

        /// <summary>
        /// Run from every balls to check collision for all ball
        /// </summary>
        /// <param name="parentBoard"></param>
        public void CalculateByAllBallCollision()
        {
            foreach (Ball b2 in Ball.BallCollection)
            {
                if (b2.ID != this.ID)
                {
                    if (CheckBallCollidedBy(b2))
                    {
                        CalculateNewVelocities(b2);
                    }
                }
            }
        }

        /// <summary>
        /// Below is a very rudimentary simulation of balls bouncing around a screen. 
        /// If they touch the edge of the screen, they bounce back.
        /// </summary>
        /// <param name="border">Screen Size</param>
        /// <returns>Return collided to edge or not</returns>
        public void CalcBoardCollision(Size border)
        {
            if (this.Position.X - Radius <= Padding && this.Speed.X < 0)
                this.Speed.X *= -1;

            if (this.Position.X + Radius >= border.Width - Padding && this.Speed.X > 0)
                this.Speed.X *= -1;

            if (this.Position.Y - Radius <= Padding && this.Speed.Y < 0)
                this.Speed.Y *= -1;

            if (this.Position.Y + Radius >= border.Height - Padding && this.Speed.Y > 0)
                this.Speed.Y *= -1;

        }

        private void CalculateNewVelocities(Ball secondBall)
        {
            var mass1 = this.Radius;
            var mass2 = secondBall.Radius;

            var velX1 = this.Speed.X;
            var velX2 = secondBall.Speed.X;
            var velY1 = this.Speed.Y;
            var velY2 = secondBall.Speed.Y;

            var newVelX1 = (velX1 * (mass1 - mass2) + (2 * mass2 * velX2)) / (mass1 + mass2);
            var newVelX2 = (velX2 * (mass2 - mass1) + (2 * mass1 * velX1)) / (mass1 + mass2);
            var newVelY1 = (velY1 * (mass1 - mass2) + (2 * mass2 * velY2)) / (mass1 + mass2);
            var newVelY2 = (velY2 * (mass2 - mass1) + (2 * mass1 * velY1)) / (mass1 + mass2);

            this.Speed.X = newVelX1;
            this.Speed.Y = newVelY1;
            secondBall.Speed.X = newVelX2;
            secondBall.Speed.Y = newVelY2;

            this.Position = new Point(this.Position.X + (int)newVelX1,
                                      this.Position.Y + (int)newVelY1);

            secondBall.Position = new Point(secondBall.Position.X + (int)newVelX2,
                                            secondBall.Position.Y + (int)newVelY2);
        }

        /// <summary>
        /// This uses the radii of the balls to 
        /// give us the true x and y co-ordinates of the collision point, 
        /// represented by the blue dot.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="parentBoard"></param>
        private void CreateDotCollisonPoint(Point point, BallBoard parentBoard)
        {
            Ball dot = new Ball();
            dot.BallColor = Color.Blue;
            dot.Radius = 4;
            dot.Position = point;

            parentBoard.Add(dot);
            Ball.BallCollection.Remove(dot);

        }

        /// <summary>
        /// check if our balls are near each other. 
        /// There are a few ways to do this, 
        /// but because we want to be good little programmers, 
        /// we're going to start off with an AABB check.
        /// 
        /// AABB stands for axis-aligned bounding box, 
        /// and refers to a rectangle drawn to fit tightly around an object, 
        /// aligned so that its sides are parallel to the axes.
        /// 
        /// An AABB collision check does not check whether the circles overlap each other, 
        /// but it does let us know if they are near each other. 
        /// Since our game only uses four objects, 
        /// this is not necessary, 
        /// but if we were running a simulation with 10,000 objects, 
        /// then doing this little optimization would save us many CPU cycles.
        ///                                                 
        ///                   ___________________ 
        ///                  |                   |
        ///                  |                   |
        ///                  |         .         |
        ///                  |                   |
        ///         _________|_                  |
        ///        |         |_|_________________|
        ///        |     .     |
        ///        |           |
        ///        |___________|
        /// 
        /// This should be fairly straightforward: 
        /// we're establishing bounding boxes the size of each ball's diameter squared.
        /// </summary>
        /// <param name="secondBall">Give me second ball to check overlapping by this ball</param>
        /// <returns>Return true if this ball overlapping by second ball's</returns>
        public bool CheckBallOverlapping(Ball secondBall)
        {
            if (this.Position.X + this.Radius + secondBall.Radius > secondBall.Position.X
             && this.Position.X < secondBall.Position.X + this.Radius + secondBall.Radius
             && this.Position.Y + this.Radius + secondBall.Radius > secondBall.Position.Y
             && this.Position.Y < secondBall.Position.Y + this.Radius + secondBall.Radius)
            {
                // Here, a “collision” has occurred - or rather, 
                // the two AABBs are overlapping, which means the circles are close to each other, 
                // and potentially colliding. Once we know the balls are in proximity.

                return true;
            }

            return false;
        }

        /// <summary>
        /// Once we know the balls are in proximity, 
        /// we can be a little bit more complex. Using trigonometry, 
        /// we can determine the distance between the two points:
        ///                          
        ///                 _____________ 
        ///                |             |
        ///                |             |
        ///                |      .      |
        ///                |    ∕ |      |
        ///         _______|_c∕   |b     |
        ///        |       |∕|____|______|
        ///        |      ∕  |    |
        ///        |    ∕____|____|
        ///        |         | a 
        ///        |_________|
        /// 
        /// Here, we're using Pythagoras' theorem, a^2 + b^2 = c^2, 
        /// to figure out the distance between the two circles' centers.
        /// 
        /// We don't immediately know the lengths of a and b, 
        /// but we do know the co-ordinates of each ball, so it's trivial to work out:
        /// 
        ///        a = firstBall.x – secondBall.x;
        ///        b = firstBall.y – secondBall.y;
        /// 
        /// We then solve for c with a bit of algebraic rearrangement: 
        /// 
        ///        c = Math.Sqrt(a^2 + b^2) 
        /// 
        /// We then check this value against the sum of the radii of the two circles:
        /// 
        ///       if ( c <= firstBall.radius + secondBall.radius)   balls have collided
        ///       
        /// </summary>
        /// <param name="secondBall">Give me second ball to check collided by this ball</param>
        /// <returns>Return true if this ball collided by second ball's</returns>
        public bool CheckBallCollidedBy(Ball secondBall)
        {
            double distance = Math.Sqrt(
            ((this.Position.X - secondBall.Position.X) * (this.Position.X - secondBall.Position.X)) +
            ((this.Position.Y - secondBall.Position.Y) * (this.Position.Y - secondBall.Position.Y)));

            if (distance <= this.Radius + secondBall.Radius)
            {
                //balls have collided
                return true;
            }

            return false;
        }


        /// <summary>
        /// It can sometimes be handy to work out the point at which two balls have collided. 
        /// If you want to, for example, 
        /// add a particle effect (perhaps a little explosion), 
        /// or you're creating some sort of guideline for a snooker game, 
        /// then it can be useful to know the collision point.
        /// </summary>
        /// <param name="secondBall">Give me second ball who collided by this ball</param>
        /// <returns>Return Collided Points of this ball and second ball's</returns>
        public Point CalcCollisionPoint(Ball secondBall)
        {
            // The formula we want to use is slightly more complicated, 
            // but works for balls of all sizes:
            //           
            //          Ball_1.(x1, y1)
            //                |\ 
            //                | \r1
            //             b1?|  \
            //                |___.Collided Point (xc=?, yc=?)
            //                |a1? \ 
            //             b2?|     \r2
            //                |______.Ball_2 (x2, y2)
            //                  a2?
            //              /    
            //             / 
            //   
            //   [1]*    a2 = |x2-x1|    
            // 
            //   [2]*    b1+b2 = |y2-y1|
            //    
            //   [3]*    Theorem of Thales:  r1/(r1+r2) = b1/(b1+b2) = a1/a2
            //  
            //   [3] Then have ==>
            //                     b1 = r1*(b1+b2) / (r1+r2)   ==[2]==>   b1 = r1*|y2-y1| / (r1+r2)
            // 
            //                     a1 = r1*a2 / (r1+r2)        ==[1]==>   a1 = r1*|x2-x1| / (r1+r2)
            //
            // 
            //   Now, if x1 > x2 :  xc = x1 - a1
            //        else       :  xc = x1 + a1
            //
            //        if y1 > y2 :  yc = y1 - b1
            //        else       :  yc = y1 + b1
            //              
            //
            //  b1 =      r1     *         |                   y2 -              y1| / (     r1     +            r2    )
            //int b1 = this.Radius * Math.Abs(secondBall.Position.Y - this.Position.Y) / (this.Radius + secondBall.Radius);
            //
            //  a1 =      r1     *         |                   x2 -              x1| / (     r1     +            r2    )
            //int a1 = this.Radius * Math.Abs(secondBall.Position.X - this.Position.X) / (this.Radius + secondBall.Radius);
            //
            //
            //int xc; // Collision Point x
            //int yc; // Collision Point y
            //
            //if (this.Position.X > secondBall.Position.X) xc = this.Position.X - a1;
            //else xc = this.Position.X + a1;

            //if (this.Position.Y > secondBall.Position.Y) yc = this.Position.Y - b1;
            //else yc = this.Position.Y + b1;

            int collisionPointX =
                (((this.Position.X * secondBall.Radius) + (secondBall.Position.X * this.Radius))
                 / (this.Radius + secondBall.Radius));

            int collisionPointY =
                (((this.Position.Y * secondBall.Radius) + (secondBall.Position.Y * this.Radius))
                 / (this.Radius + secondBall.Radius));




            return new Point(collisionPointX, collisionPointY); //new Point(xc, yc);
        }

        public Point GetNextPosition()
        {
            return new Point((int)(Position.X + Speed.X), (int)(Position.Y + Speed.Y));
        }

        public void MoveToNextPosition(Size border)
        {
            this.Position = GetNextPosition();  // Set new position

            CalcBoardCollision(border);         // Check Collision the ball by board corners

            CalculateByAllBallCollision();
        }

        #endregion ----------------------------------------------
    }


    public class Speed
    {
        public double X;
        public double Y;

        public Speed(double speedX, double speedY)
        {
            X = speedX;
            Y = speedY;
        }
    }
}
