using System.Windows.Forms;

namespace Game.Views
{
    public partial class GameForm : Form
    {
        private GameLogic.Game game;
        public GameForm(GameLogic.Game game)
        {
            this.game = game;
        }
    }
    
    
    
}