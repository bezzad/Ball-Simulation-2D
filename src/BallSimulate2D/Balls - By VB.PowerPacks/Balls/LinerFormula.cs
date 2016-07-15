namespace Balls
{
    /// <summary>
    /// m = _mSign = MPositionSign
    /// y = _ySign = YPositionSign
    /// 
    ///           -m   -y    |    +m   -y
    ///                      |
    ///                      |
    ///      ----------------|----------------> +X                
    ///                      |
    ///           -m   +y    |    +m   +y
    ///                      |
    ///                     +Y
    /// 
    /// Liner Formula is: 
    ///                   F(x) = (-+y)Y = (-+m)M.X + K
    ///                   M = incline of the line
    ///                   K = is const factor
    ///                   (-+y) = Sign of the F(x)
    ///                   (-+m) = Sign of the incline
    /// </summary>
    public class LinerFormula
    {
        #region Members
        public int M; // Incline of the line
        public int K; // Is const factor

        int _mSign; // Sign of the incline , -1 or +1
        int _ySign; // Sign of the F(x) , -1 or +1


        /// <summary>
        /// Is the sign of the line incline (+m) positive?
        /// </summary>
        public bool MPositiveSign
        {
            set
            {
                if (value) _mSign = +1;
                else _mSign = -1;
            }
            get
            {
                if (_mSign >= 0) return true;
                return false;
            }
        }

        /// <summary>
        /// Is the sign of the F(x) (+y) positive?
        /// </summary>
        public bool YPositiveSign
        {
            set
            {
                if (value) _ySign = +1;
                else _ySign = -1;
            }
            get
            {
                if (_ySign >= 0) return true;
                return false;
            }
        }

        #endregion


        /// <summary>
        /// Liner Formula is: 
        ///                   F(x) = (-+y)Y = (-+m)M.X + K
        ///                   M = incline of the line
        ///                   K = is const factor
        ///                   (-+y) = Sign of the F(x)
        ///                   (-+m) = Sign of the incline
        /// </summary>
        public LinerFormula(int m, int k)
        {
            this.M = m;
            this.K = k;

            YPositiveSign = true;
            MPositiveSign = true;
        }


        #region Methods

        /// <summary>
        /// Get F(x) * ySign = mSign * M * X + K
        /// </summary>
        /// <param name="x">X</param>
        /// <returns>F(x)</returns>
        public int GetFx(int x)
        {
            return ((_mSign * M * x) + K) / _ySign;
        }

        /// <summary>
        /// Get X = ((ySign * Y) - K) / (mSign * M)
        /// </summary>
        /// <param name="y">Y</param>
        /// <returns></returns>
        public int GetX(int y)
        {
            return ((_ySign * y) - K) / (_mSign * M);
        }

        #endregion
    }
}
