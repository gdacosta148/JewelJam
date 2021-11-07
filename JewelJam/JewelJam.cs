using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// This is the main type for your game.
/// </summary>
public class JewelJam : ExtendedGame
{ 
 

  




    Vector2 GridOffset = new Vector2(85, 150);



    public JewelJam()
        {
       




        IsMouseVisible = true;

     
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        base.LoadContent();

        // initialize the game world
        gameWorld = new JewelJamGameWorld(this);

        // to re-scale the game world to the screen size, we need to set the FullScreen property again
        worldSize = GameWorld.Size;

        FullScreen = false;
    }


         public static JewelJamGameWorld GameWorld
    {
        get { return (JewelJamGameWorld)gameWorld; }
    }

    // TODO: use this.Content to load your game content here
}












