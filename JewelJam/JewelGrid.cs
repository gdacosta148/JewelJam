using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


class JewelGrid : GameObject
{
    Jewel[,] grid;
    int   cellSize;

    public int Height { get;  private set; }
    public int Width { get; private set; }

    public JewelGrid(int width, int height, int cellSize)
    {

        Width = width;

        Height = height;
        this.cellSize = cellSize;
     
        Reset();
    }

    public override void Reset()
    {
        // initialize the grid
        grid = new Jewel[Width, Height];

        // fill the grid with random jewels
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                // add a new jewel to the grid
                AddJewel(x, y);
            }
        }
    }

    public override void HandleInput(inputHelper inputHelper)
    {
        // when the player presses the spacebar, move all jewels one row down
        if (!inputHelper.KeyPressed(Keys.Space))
            return;

        int combo = 0;

        int mid = Width / 2;
        int extraScore = 10;

        for (int y = 0; y < Height - 2; y++)
{
            if (IsValidCombination(grid[mid, y], grid[mid, y + 1], grid[mid, y + 2]))
            {
                JewelJam.GameWorld.AddScore(extraScore);
                extraScore *= 2;
                combo++;

                RemoveJewel(mid, y);
                RemoveJewel(mid, y + 1);
                RemoveJewel(mid, y + 2);

                y += 2;

            }
        }

        // play a sound, and check if a combo has been scored
        if (combo == 0)
            ExtendedGame.AssetManager.PlaySoundEffect("snd_error");
        else if (combo == 1)
            ExtendedGame.AssetManager.PlaySoundEffect("snd_single");
        else if (combo == 2)
        {
            JewelJam.GameWorld.DoubleComboScored();
            ExtendedGame.AssetManager.PlaySoundEffect("snd_double");
        }
        else if (combo == 3)
        {
            JewelJam.GameWorld.TripleComboScored();
            ExtendedGame.AssetManager.PlaySoundEffect("snd_triple");
        }

    }


    void RemoveJewel(int x, int y)
    {
        for (int row = y; row > 0; row--)
        {
            
                grid[x, row] = grid[x, row - 1];

                grid[x, row].LocalPosition = GetCellPosition(x, row);
            
        }

        AddJewel(x, 0);
    }

    void AddJewel(int x, int y)
    {
        // store the jewel in the grid
        grid[x, y] = new Jewel();

        // set the parent and position of the jewel
        grid[x, y].Parent = this;
        grid[x, y].LocalPosition = GetCellPosition(x, y);

    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Jewel jewel in grid)
            jewel.Draw(gameTime, spriteBatch);
    }


   

    public Vector2 GetCellPosition(int x, int y)
    {
        return  new Vector2(x * cellSize, y * cellSize);
    }

    public void ShiftRowLeft(int selectedRow)
    {
        // store the leftmost jewel as a backup
        Jewel first = grid[0, selectedRow];
        // replace all jewels by their right neighbor
        for (int x = 0; x < Width - 1; x++)
{
            grid[x, selectedRow]  = grid[x + 1, selectedRow];
            grid[x, selectedRow].LocalPosition = GetCellPosition(x, selectedRow);
        }
        // re−insert the old leftmost jewel on the right
        grid[Width - 1, selectedRow] = first;
        grid[Width - 1, selectedRow].LocalPosition = GetCellPosition(Width - 1, selectedRow);
    }

    public void ShiftRowRight(int selectedRow)
    {
        // store the rightmost jewel as a backup
        Jewel last = grid[Width - 1, selectedRow];

        // replace all jewels by their left neighbor
        for (int x = Width - 1; x > 0; x--)
        {
            grid[x, selectedRow] = grid[x - 1, selectedRow];
            grid[x, selectedRow].LocalPosition = GetCellPosition(x, selectedRow);
        }

        // re-insert the old rightmost jewel on the left
        grid[0, selectedRow] = last;
        last.LocalPosition = GetCellPosition(0, selectedRow);
    }


    bool IsValidCombination(Jewel a, Jewel b, Jewel c)
    {
        return IsConditionValid(a.ColorType, b.ColorType, c.ColorType)
        && IsConditionValid(a.ShapeType, b.ShapeType, c.ShapeType)
        && IsConditionValid(a.NumberType, b.NumberType, c.NumberType);
    }

    bool IsConditionValid(int a, int b, int c)
    {
        return AllEqual(a, b, c) || AllDifferent(a, b, c);
    }

    bool AllEqual(int a, int b, int c)
    {
        return a == b && b == c;
    }

    bool AllDifferent(int a, int b, int c)
    {
        return a != b && b != c && a != c;
    }

}

