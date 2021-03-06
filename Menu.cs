using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    //menu
    enum CurrentMenu
    {
        StartMenu,
        ColorMenu,
        PauseMenu,
        DeathMenu,
        None
    }

    class Menu
    {
        public CurrentMenu CurrentMenu { get { return currentMenu; } }

        CurrentMenu currentMenu = CurrentMenu.StartMenu;

        private Player player;

        private Score score;

        public Menu(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            switch (currentMenu)
            {
                case CurrentMenu.StartMenu:
                    StartMenu();
                    break;

                case CurrentMenu.ColorMenu:
                    ColorMenu();
                    break;

                case CurrentMenu.PauseMenu:
                    PauseMenu();
                    break;

                case CurrentMenu.DeathMenu:
                    DeathMenu();
                    break;

                case CurrentMenu.None:
                    if(Keyboard.GetState().IsKeyDown(Keys.P))
                        currentMenu = CurrentMenu.PauseMenu;

                    if (player.IsDead())
                    {
                        currentMenu = CurrentMenu.DeathMenu;
                    }
                    break;

                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentMenu)
            {
                case CurrentMenu.StartMenu:
                    spriteBatch.DrawString(Assets.MenuFont, "Press space to continue", new Vector2(250, 300), Color.Purple);
                    break;

                case CurrentMenu.ColorMenu:
                    spriteBatch.DrawString(Assets.MenuFont, "Press space to continue", new Vector2(250, 300), Color.Purple);
                    break;

                case CurrentMenu.PauseMenu:
                    spriteBatch.Draw(Assets.PauseScreen, new Rectangle(0, 0, 800, 480), new Color(Color.White, 1));
                    spriteBatch.DrawString(Assets.MenuFont, "Press space to continue", new Vector2(250, 300), Color.Purple);
                    break;

                case CurrentMenu.DeathMenu:
                    spriteBatch.DrawString(Assets.MenuFont, "your score is:" + Game1.HighScore.ToString(), new Vector2(100, 100), Color.Purple);

                    spriteBatch.DrawString(Assets.MenuFont, "Highscores:", new Vector2(200, 200), Color.Purple);

                    spriteBatch.DrawString(Assets.MenuFont, "Press SPACE to restart\n Press ESCAPE to exit", new Vector2(250, 300), Color.Purple);
                    break;

                case CurrentMenu.None:
                    break;

                default:
                    break;
            }
        }

        public void StartMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                currentMenu = CurrentMenu.None;
        }

        public void ColorMenu()
        {
            //if (Keyboard.GetState().IsKeyDown(Keys.Space))
                currentMenu = CurrentMenu.None;
        }
        public void PauseMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                currentMenu = CurrentMenu.None;

            //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit
        }

        public void DeathMenu()
        {
            string hscore = Game1.HighScore.ToString() + "\n";
            string[] lines = { hscore };

            for (int i = 0; i < lines.Length; i++)
            {
                if (int.Parse(hscore) > int.Parse(lines[i]))
                {
                    string temp = lines[i];

                    System.IO.File.WriteAllLines(@"./scoreFile.txt", lines);

                    lines[i + 2] = lines[i + 1];
                    lines[i + 1] = temp;
                }

                if(string.IsNullOrEmpty(lines[i]))
                {
                    System.IO.File.WriteAllLines(@"./scoreFile.txt", lines);
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                currentMenu = CurrentMenu.StartMenu;

            //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit
        }

    }
}
///I den här klassen kollar vi vilken meny vi är i och vad som sak ske om vi är i den menyn
///
///Startmenu visar bara en text att du kan spela om du klickar mellanslag
///
///Colormenu är inte klar men bör vara till för att välja färg på spelaren
///
///Pausemenu är som startmenu men öppnas om du klickar P tangenten och stängs med mellanslag
///
/// Deathmenu är inte heller klar men ska leda till en scoreboard och öppnas när man dör. Härifrån kan du spela om eller lämna.
/// 
///