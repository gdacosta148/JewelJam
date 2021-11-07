using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class SpriteGameObject:GameObject
    {
    protected Texture2D sprite;
    protected Vector2 origin;

    public SpriteGameObject(string spriteName)
    {
        sprite = ExtendedGame.AssetManager.LoadSprite(spriteName);
        origin = Vector2.Zero;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (Visible)
        {
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White,
            0, origin, 1.0f, SpriteEffects.None, 0);
        }
    }


    public int Width { get { return sprite.Width; } }

 public int Height { get { return sprite.Height; } }

/// <summary>

 public Rectangle BoundingBox
 {
 get
 {
 // get the sprite’s bounds
 Rectangle spriteBounds = sprite.Bounds;
 // add the object’s position to it as an offset
 spriteBounds.Offset(GlobalPosition - origin);
 return spriteBounds;
 }
 }


    public void SetOriginToCenter()
    {
        origin = new Vector2(Width / 2.0f, Height / 2.0f);
    }



}

