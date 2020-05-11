using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Game.GameLogic;

namespace Game.Views
{
    public partial class GameForm : Form
    {
        private GameLogic.Game game;
        private Dictionary<Cell, Control> cells;
        private Dictionary<Control, Cell> buttons;
        private readonly TableLayoutPanel gameField;
        private Label sticks;
        private Button throwButton;
        private int currentStepCount;
        private Label currentFigureType;

        public GameForm(GameLogic.Game game, MenuForm menuForm)
        {
            cells = new Dictionary<Cell, Control>();
            buttons = new Dictionary<Control, Cell>();
            this.game = game;
            MinimumSize = new Size(1200, 800);
            MaximumSize = MinimumSize;
            BackColor = Color.Teal;
            gameField = new TableLayoutPanel
            {
                Location = new Point(50, 50),
                Size = new Size(1100, 300)
            };
            
            for (var i = 0; i < 10; i++)
            {
                gameField.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, gameField.Width / 10));
                if (i == 0 || i == 1 || i == 2)
                    gameField.RowStyles.Add(new RowStyle(SizeType.Percent, gameField.Height / 3));
            }
            
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var button = new Button {Dock = DockStyle.Fill, BackColor = Color.FromArgb(239, 227, 175)};
                    gameField.Controls.Add(button, i, j);
                    button.Click += (sender, args) =>
                    {
                        Figure figure;
                        if (buttons[button].State != null)
                        {
                            figure = buttons[button].State;
                            if (GameLogic.Game.CurrentPlayer.FigureInTheHouseOfBeauty != null)
                                figure = GameLogic.Game.CurrentPlayer.FigureInTheHouseOfBeauty;
                            
                            if (GameLogic.Game.CurrentPlayer.FigureInTheHouseOfWater != null)
                                figure = GameLogic.Game.CurrentPlayer.FigureInTheHouseOfWater;

                            if (figure.Type == GameLogic.Game.CurrentPlayer.OwnType &&
                                GameLogic.Game.MakeStep(currentStepCount, game.Map, figure))
                            {
                                FieldRedrawing();
                                if (!GameLogic.Game.Sticks.ExtraMove)
                                {
                                    game.ChangeCurrentPlayer();
                                    CurrentFigureTypeRedrawing();
                                }
                                throwButton.Enabled = true;
                                currentStepCount = 0;
                            }
                        }

                        if (GameLogic.Game.IsGameOver())
                        {
                            var messageBox = MessageBox.Show("Игра окончена!", "", MessageBoxButtons.YesNo);
                            if (messageBox != DialogResult.Yes)
                            {
                                menuForm.Hide();
                                Close();
                            }
                        }
                    };
                }
            }
            
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (j == 0 || j == 2)
                    {
                        cells.Add(game.Map[i + 1 + 10 * j], gameField.GetControlFromPosition(i, j));
                        buttons.Add(gameField.GetControlFromPosition(i, j), game.Map[i + 1 + 10 * j]);
                    }
                    else if (j == 1)
                    {
                        cells.Add(game.Map[20 - i], gameField.GetControlFromPosition(i, j));
                        buttons.Add(gameField.GetControlFromPosition(i, j), game.Map[20 - i]);
                    }
                }
            }

            FieldRedrawing();
            
            gameField.BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(gameField);
            
            throwButton = new Button
            {
                Text = "Throw Sticks",
                ForeColor = Color.Beige,
                Font = new Font("Arial", 13),
                Size = new Size(200, 100),
                Location = new Point(gameField.Left, gameField.Bottom + 100),
                BackColor = Color.Teal
            };

            throwButton.Click += (sender, args) =>
            {
                currentStepCount = GameLogic.Game.Sticks.Throw();
                SticksRedrawing();
                if (game.CheckAbleToMove(currentStepCount).Count == 0)
                {
                    game.ChangeCurrentPlayer();
                    CurrentFigureTypeRedrawing();
                }

                //throwButton.Enabled = false;
            };
            Controls.Add(throwButton);

            sticks = new Label
            {
                Size = new Size(400, 215),
                Location = new Point(throwButton.Right + 100, gameField.Bottom + 100),
                BackgroundImage = new Bitmap(@"images\sticks5.jpg")
            };
            Controls.Add(sticks);
            
            currentFigureType = new Label
            {
                Size = new Size(105, 95),
                BackgroundImage = new Bitmap(@"images\Coil.jpg"),
                Location = new Point(sticks.Right + 150, gameField.Bottom + 100)
            };
            Controls.Add(currentFigureType);

            FormClosing += (sender, args) =>
            {
                menuForm.Show();
                Hide();
            }; //Application.Exit();
        }

        public void CurrentFigureTypeRedrawing()
        {
            if (GameLogic.Game.CurrentPlayer.OwnType == ChipsType.Coil)
                currentFigureType.BackgroundImage = new Bitmap(@"images\Coil.jpg");
            else
                currentFigureType.BackgroundImage = new Bitmap(@"images\Cone.jpg");
        }


        private void SticksRedrawing()
        {
            sticks.BackgroundImage = new Bitmap($@"images\sticks{currentStepCount}.jpg");
        }
        
        private void FieldRedrawing()
        {
            foreach (var cell in game.Map.Skip(1))
            {
                if (cell.State != null)
                {
                    if (cell.State.Type == ChipsType.Coil)
                        cells[cell].BackgroundImage = new Bitmap(@"images\Coil.jpg");
                    else
                    {
                        cells[cell].BackgroundImage = new Bitmap(@"images\Cone.jpg");
                    }
                }

                else if(cell is HouseOfRevival)
                    cells[cell].BackgroundImage = new Bitmap(@"images\HouseOfRevival.jpg");
                
                else if(cell is HouseOfBeauty)
                    cells[cell].BackgroundImage = new Bitmap(@"images\HouseOfBeauty.jpg");
                else if(cell is HouseOfWater)
                    cells[cell].BackgroundImage = new Bitmap(@"images\HouseOfWater.jpg");
                else if(cell is HouseOfThreeTruths)
                    cells[cell].BackgroundImage = new Bitmap(@"images\HouseOfTrue.jpg");
                else if(cell is HouseOfIsidaAndNeftida)
                    cells[cell].BackgroundImage = new Bitmap(@"images\HouseOfGirls.jpg");
                else if(cell is HouseOfRaHorati)
                    cells[cell].BackgroundImage = new Bitmap(@"images\HouseOfRA.jpg");
                
                else
                    cells[cell].BackgroundImage = new Bitmap(@"images\fon.png");
            }
        }
    }
}