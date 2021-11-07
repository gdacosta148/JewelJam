using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class GameObjectList : GameObject
{

    List<GameObject> children;

    public GameObjectList()
    {
        children = new List<GameObject>();
    }

    public void AddChild(GameObject obj)
    {
        obj.Parent = this;
        children.Add(obj);
    }

    public override void HandleInput(inputHelper inputHelper)
    {
        foreach (GameObject obj in children)
            obj.HandleInput(inputHelper);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (GameObject obj in children)
            obj.Update(gameTime);
    }

    /// <summary>
    /// Performs the Draw method for all game objects in this GameObjectList.
    /// </summary>
    /// <param name="gameTime">An object containing information about the time that has passed in the game.</param>
    /// <param name="spriteBatch">A sprite batch object used for drawing sprites.</param>
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject obj in children)
            obj.Draw(gameTime, spriteBatch);
    }

    /// <summary>
    /// Performs the Reset method for all game objects in this GameObjectList.
    /// </summary>
    public override void Reset()
    {
        foreach (GameObject obj in children)
            obj.Reset();
    }


}

