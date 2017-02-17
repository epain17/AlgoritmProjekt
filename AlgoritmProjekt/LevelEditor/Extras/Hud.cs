using AlgoritmProjekt.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Extras
{
    class Hud
    {
        Texture2D texture;
        SpriteFont font;

        Vector2 position, textPos;
        List<string> hudTitles = new List<string>();

        List<string> setGridButtons = new List<string>();

        List<string> navigateToolButtons = new List<string>();

        List<string> categories = new List<string>();

        List<string> characters = new List<string>();
        List<string> environment = new List<string>();
        List<string> weapons = new List<string>();

        int screenWidth;
        int screenHeight;
        public int selected = -1;
        Color color;

        public Hud(Texture2D texture, SpriteFont font, Vector2 position, int screenWidth, int screenHeight)
        {
            this.texture = texture;
            this.font = font;
            this.position = position;
            this.textPos = position;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            hudTitles.Add("Set Grid");
            hudTitles.Add("Tools");
            hudTitles.Add("Category");
            hudTitles.Add("Objects");

            setGridButtons.Add("Columns: ");
            setGridButtons.Add("Rows: ");

            navigateToolButtons.Add("Add Tiles");
            navigateToolButtons.Add("Remove Tile");
            navigateToolButtons.Add("Save Level");

            categories.Add("Characters");
            categories.Add("Environment");
            categories.Add("Weapons");

            characters.Add("Player");
            //characters.Add("Enemy");
            characters.Add("Spawner");

            environment.Add("Wall");
            environment.Add("Tele");

            weapons.Add("Pistol");
        }

        public void Update(Vector2 cameraPos)
        {
            position = new Vector2(0 - cameraPos.X, 600 - texture.Height - cameraPos.Y);
            if (Game1.navigateTabs == Game1.NavigationTabs.ChooseTool)
                ManipulateSelected(navigateToolButtons);
            else if (Game1.navigateTabs == Game1.NavigationTabs.ChooseCategory)
                ManipulateSelected(categories);
            else if (Game1.navigateTabs == Game1.NavigationTabs.ChooseObject)
            {
                if (Game1.chooseCategory == Game1.ChooseCategory.Characters)
                    ManipulateSelected(characters);
                else if(Game1.chooseCategory == Game1.ChooseCategory.Environment)
                    ManipulateSelected(environment);
                else if(Game1.chooseCategory == Game1.ChooseCategory.Weapons)
                    ManipulateSelected(weapons);

            }
        }

        private void ManipulateSelected(List<string> list)
        {
            if (Game1.navigateTabs == Game1.NavigationTabs.ChooseTool ||
                Game1.navigateTabs == Game1.NavigationTabs.ChooseCategory ||
                Game1.navigateTabs == Game1.NavigationTabs.ChooseObject)
            {
                if (selected == -1)
                    selected = 0;
                if (KeyMouseReader.KeyPressed(Keys.W) && selected > 0)
                    selected--;
                if (KeyMouseReader.KeyPressed(Keys.S) && selected < list.Count - 1)
                    selected++;
            }
            else
                selected = -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, position, Color.DarkSlateGray);
            DrawTitles(spriteBatch);
            DrawSetGridButtons(spriteBatch);
            DrawToolButtons(spriteBatch);
            DrawCategoryButtons(spriteBatch);
            DrawObjectList(spriteBatch);
        }

        private void DrawObjectList(SpriteBatch spriteBatch)
        {
            if (Game1.navigateTabs == Game1.NavigationTabs.ChooseObject)
            {
                switch (Game1.chooseCategory)
                {
                    case Game1.ChooseCategory.Characters:
                        spriteBatch.DrawString(font, characters[selected], new Vector2(position.X + 450, position.Y + (font.MeasureString(categories[selected]).Y * (1 + 1))), color);
                        break;
                    case Game1.ChooseCategory.Environment:
                        spriteBatch.DrawString(font, environment[selected], new Vector2(position.X + 450, position.Y + (font.MeasureString(environment[selected]).Y * (1 + 1))), color);
                        break;
                    case Game1.ChooseCategory.Weapons:
                        spriteBatch.DrawString(font, weapons[selected], new Vector2(position.X + 450, position.Y + (font.MeasureString(weapons[selected]).Y * (1 + 1))), color);
                        break;
                }
            }
        }

        private void DrawCategoryButtons(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (Game1.navigateTabs == Game1.NavigationTabs.ChooseCategory)
                    color = (i == selected) ? Color.White : Color.Gray;
                else
                    color = Color.Gray;
                spriteBatch.DrawString(font, categories[i], new Vector2(position.X + 300, position.Y + (font.MeasureString(categories[i]).Y * (i + 1))), color);
            }
        }

        private void DrawToolButtons(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < navigateToolButtons.Count; i++)
            {
                if (Game1.navigateTabs == Game1.NavigationTabs.ChooseTool)
                    color = (i == selected) ? Color.White : Color.Gray;
                else
                    color = Color.Gray;
                spriteBatch.DrawString(font, navigateToolButtons[i], new Vector2(position.X + 150, position.Y + (font.MeasureString(navigateToolButtons[i]).Y * (i + 1))), color);
            }
        }

        private void DrawSetGridButtons(SpriteBatch spriteBatch)
        {
            color = (Game1.navigateTabs == Game1.NavigationTabs.SetGrid) ? Color.LightGreen : Color.Gray;
            for (int i = 0; i < setGridButtons.Count; i++)
            {
                int index = 0;
                if (setGridButtons[i] == "Columns: ")
                    index = Constants.columns;
                else if (setGridButtons[i] == "Rows: ")
                    index = Constants.rows;
                spriteBatch.DrawString(font, setGridButtons[i] + index, new Vector2(position.X + 5, position.Y + (font.MeasureString(setGridButtons[i]).Y * (i + 1))), Color.Gray);
            }
        }

        private void DrawTitles(SpriteBatch spriteBatch)
        {
            switch (Game1.navigateTabs)
            {
                case Game1.NavigationTabs.SetGrid:
                    for (int i = 0; i < hudTitles.Count; i++)
                    {
                        color = (hudTitles[i] == "Set Grid") ? Color.Lime : Color.Green;
                        spriteBatch.DrawString(font, hudTitles[i], new Vector2(position.X + (150 * i), position.Y + (font.MeasureString(hudTitles[0]).Y * 0)), color);
                    }
                    break;
                case Game1.NavigationTabs.ChooseTool:
                    for (int i = 0; i < hudTitles.Count; i++)
                    {
                        color = (hudTitles[i] == "Tools") ? Color.Lime : Color.Green;
                        spriteBatch.DrawString(font, hudTitles[i], new Vector2(position.X + (150 * i), position.Y + (font.MeasureString(hudTitles[0]).Y * 0)), color);
                    }
                    break;
                case Game1.NavigationTabs.ChooseCategory:
                    for (int i = 0; i < hudTitles.Count; i++)
                    {
                        color = (hudTitles[i] == "Category") ? Color.Lime : Color.Green;
                        spriteBatch.DrawString(font, hudTitles[i], new Vector2(position.X + (150 * i), position.Y + (font.MeasureString(hudTitles[0]).Y * 0)), color);
                    }
                    break;
                case Game1.NavigationTabs.ChooseObject:
                    for (int i = 0; i < hudTitles.Count; i++)
                    {
                        color = (hudTitles[i] == "Objects") ? Color.Lime : Color.Green;
                        spriteBatch.DrawString(font, hudTitles[i], new Vector2(position.X + (150 * i), position.Y + (font.MeasureString(hudTitles[0]).Y * 0)), color);
                    }
                    break;
            }
        }
    }
}
