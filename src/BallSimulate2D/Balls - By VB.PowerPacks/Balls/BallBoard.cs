using System.Windows.Forms;

namespace Balls
{
    public class BallBoard : Panel
    {
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer;

        public BallBoard()
        {
            shapeContainer = new Microsoft.VisualBasic.PowerPacks.ShapeContainer
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Size = this.ClientSize
            };

            this.Controls.Add(shapeContainer);
        }

        public void Add(Ball value)
        {
            shapeContainer.Shapes.Add(value);
        }
    }
}
