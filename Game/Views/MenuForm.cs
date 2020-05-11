using System.Drawing;
using System.Windows.Forms;

namespace Game.Views
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            MinimumSize = new Size(1200, 800);
            MaximumSize = MinimumSize;
            BackgroundImage = new Bitmap(@"images\start.jpg");
            var startButton = new Button
            {
                Text = @"Начать",
                Font = new Font("Arial", 13),
                Size = new Size(120, 55),
                Location = new Point(ClientSize.Width / 2 - 150, ClientSize.Height / 2)
            };
        
            Controls.Add(startButton);
            
            startButton.Click += (sender, args) =>
            {
                new GameForm(new GameLogic.Game(), this).Show();
                Hide();
            };
            
            var settingsButton = new Button
            {
                Text = @"Настройки",
                Font = new Font("Arial", 13),
                Size = new Size(120, 55),
                Location = new Point(startButton.Right + 15, ClientSize.Height / 2)
            };
            
            Controls.Add(settingsButton);
            var historyButton = new Button
            {
                Text = @"История",
                Font = new Font("Arial", 13),
                Size = new Size(120, 55),
                Location = new Point(settingsButton.Right + 15, ClientSize.Height / 2)
            };
            
            Controls.Add(historyButton);
        }
    }
}