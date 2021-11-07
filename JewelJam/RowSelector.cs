using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;




    class RowSelector:SpriteGameObject
    {
    int selectedRow;
    JewelGrid grid;

    public RowSelector(JewelGrid grid) : base("spr_selector_frame")
    {
        this.grid = grid;
        selectedRow = 0;
        origin = new Vector2(10, 10);
    }


    public override void HandleInput(inputHelper inputHelper)
    {
        if (inputHelper.KeyPressed(Keys.Up))
            selectedRow--;
else if (inputHelper.KeyPressed(Keys.Down))
            selectedRow++;

        if (selectedRow < 0)
            selectedRow = 0;

        selectedRow = MathHelper.Clamp(selectedRow, 0, grid.Height - 1);


        LocalPosition = grid.GetCellPosition(0, selectedRow);


        if (inputHelper.KeyPressed(Keys.Left))
            grid.ShiftRowLeft(selectedRow);
        else if (inputHelper.KeyPressed(Keys.Right))
            grid.ShiftRowRight(selectedRow);

    }
}

