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
            Application.Run(new MenuForm());
        }
    }
}