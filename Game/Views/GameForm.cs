using System.Drawing;
using System.Windows.Forms;

namespace Game.Views
{
    public partial class GameForm : Form
    {
        private GameLogic.Game game;
        public GameForm(GameLogic.Game game)
        {
            this.game = game;
            MinimumSize = new Size(1200, 800);
            MaximumSize = MinimumSize;
            var table = new TableLayoutPanel();
            for (var i = 0; i < 10; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                if (i == 0 || i == 1 || i == 2)
                    table.RowStyles.Add(new RowStyle(SizeType.Percent, 34));
            }

            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var button = new Button {Dock = DockStyle.Fill};
                    table.Controls.Add(button, i, j);
                }
            }
            
            table.BorderStyle = BorderStyle.Fixed3D;
            table.Width = 1000;
            table.Height = 400;
            Controls.Add(table);
        }
    }
}