using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game.GameLogic;
using Game.Views;

namespace Game
{
    // public class MyForm : Form
    // {
    //     public MyForm(GameLogic.Game game)
    //     {
    //         var a = new GameForm(game);
    //         MinimumSize = new Size(1200, 800);
    //         MaximumSize = MinimumSize;
    //         BackgroundImage = new Bitmap(@"images\start.jpg");
    //         var startButton = new Button
    //         {
    //             Text = @"Начать",
    //             Font = new Font("Arial", 13),
    //             Size = new Size(100, 55),
    //             Location = new Point(ClientSize.Width / 2 - 150, ClientSize.Height / 2)
    //         };
    //
    //         Controls.Add(startButton);
    //         
    //         startButton.Click += (sender, args) => a.;
    //         
    //         var settingsButton = new Button
    //         {
    //             Text = @"Настройки",
    //             Font = new Font("Arial", 13),
    //             Size = new Size(100, 55),
    //             Location = new Point(startButton.Right + 15, ClientSize.Height / 2)
    //         };
    //         
    //         Controls.Add(settingsButton);
    //         var historyButton = new Button
    //         {
    //             Text = @"История",
    //             Font = new Font("Arial", 13),
    //             Size = new Size(100, 55),
    //             Location = new Point(settingsButton.Right + 15, ClientSize.Height / 2)
    //         };
    //         
    //         Controls.Add(historyButton);
    //     }
    // }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm(new GameLogic.Game()));
        }
    }
}