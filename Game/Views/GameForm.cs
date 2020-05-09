using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Game.GameLogic;

namespace Game.Views
{
    public partial class GameForm : Form
    {
        private GameLogic.Game game;
        private Dictionary<Control, Cell> cells;
        private TableLayoutPanel table;
        public GameForm(GameLogic.Game game)
        {
            cells = new Dictionary<Control, Cell>();
            this.game = game;
            MinimumSize = new Size(1200, 800);
            MaximumSize = MinimumSize;
            table = new TableLayoutPanel();
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
                    var button = new Button {Dock = DockStyle.Fill, BackColor = Color.Black};
                    table.Controls.Add(button, i, j);
                    
                    
                     button.Click += (sender, args) =>
                     {
                        if (GameLogic.Game.MakeStep(3, game.Map, cells[button].State));
                            Redrawing(new Point(0, 0), new Point(1, 1));
                     };
                    
                    //table.Controls[i + j * 10]
                    //table.Controls[19 - i]
                }
            }
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (j == 0 || j == 2)
                        cells.Add(table.GetControlFromPosition(i, j), game.Map[i + 1 + 10 * j]);
                    else if (j == 1)
                        cells.Add(table.GetControlFromPosition(9 - i, j), game.Map[20 - i]);
                }
            }

            for (var i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    table.GetControlFromPosition(i, 0).BackgroundImage = new Bitmap(@"images\Cone.jpg");
                }
                else
                {
                    table.GetControlFromPosition(i, 0).BackgroundImage = new Bitmap(@"images\Coil.jpg");
                }
            }
            
            table.GetControlFromPosition(9, 1).BackgroundImage = new Bitmap(@"images\Cone.jpg");
            table.GetControlFromPosition(8, 1).BackgroundImage = new Bitmap(@"images\Coil.jpg");
            table.GetControlFromPosition(7, 1).BackgroundImage = new Bitmap(@"images\Cone.jpg");
            table.GetControlFromPosition(6, 1).BackgroundImage = new Bitmap(@"images\Coil.jpg");

            
            table.BorderStyle = BorderStyle.Fixed3D;
            table.Width = 1100;
            table.Height = 400;
            Controls.Add(table);
        }

        
        
        private void Redrawing(Point first, Point second)
        {
            table.GetControlFromPosition(second.X, second.Y).BackgroundImage = 
                table.GetControlFromPosition(first.X, first.Y).BackgroundImage;
        }
    }
}