using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


  public class JewelJamGameWorld: GameObjectList
{

    const int GridWidth = 5;
 const int GridHeight = 10;
 const int CellSize = 85;

    // The size of the game world, in game units.

    SpriteGameObject titleScreen, gameOverScreen, helpScreen, helpButton;
    public Point Size { get; private set; }

    JewelJam game;

 // The player’s current score.
    public int Score { get; private set; }

    JewelCart jewelCart;

    enum GameState { TitleScreen, Playing, HelpScreen, GameOver }
    GameState currentState;

    // Timers for managing the visibility of the two combo images.
    VisibilityTimer timer_double, timer_triple;


    public JewelJamGameWorld(JewelJam game)
    {

        this.game = game;
        // add the background
        SpriteGameObject background = new SpriteGameObject("spr_background");
 Size = new Point(background.Width, background.Height);
 AddChild(background);

 // add a ”playing field” parent object for the grid and all related objects
 GameObjectList playingField = new GameObjectList();
 playingField.LocalPosition = new Vector2(85, 150);
 AddChild(playingField);

 // add the grid to the playing field
 JewelGrid grid = new JewelGrid(GridWidth, GridHeight, CellSize);
 playingField.AddChild(grid);

 // add the row selector to the playing field
 playingField.AddChild(new RowSelector(grid));



        // add a background sprite for the score object
        SpriteGameObject scoreFrame = new SpriteGameObject("spr_scoreframe");
        scoreFrame.LocalPosition = new Vector2(20, 20);
        AddChild(scoreFrame);

        // add the object that displays the score
        ScoreGameObject scoreObject = new ScoreGameObject();
        scoreObject.LocalPosition = new Vector2(270, 30);
        AddChild(scoreObject);

        jewelCart = new JewelCart(new Vector2(410, 230));
        AddChild(jewelCart);

        // add the help button
        helpButton = new SpriteGameObject("spr_button_help");
        helpButton.LocalPosition = new Vector2(1270, 20);
        AddChild(helpButton);

        // add the various overlays
        titleScreen = AddOverlay("spr_title");
        gameOverScreen = AddOverlay("spr_gameover");
        helpScreen = AddOverlay("spr_frame_help");

        timer_double = AddComboImageWithTimer("spr_double");
        timer_triple = AddComboImageWithTimer("spr_triple");

        // reset some game parameters
        GoToState(GameState.TitleScreen);
    }

    public override void Update(GameTime gameTime)
    {
        if (currentState == GameState.Playing)
        {
            base.Update(gameTime);

            if (jewelCart.GlobalPosition.X > Size.X - 230)
              GoToState(GameState.GameOver);


        }
    }

    public override void HandleInput(inputHelper inputHelper)
    {
        if (currentState == GameState.Playing)
        {
            base.HandleInput(inputHelper);

            // if the player presses the Help button, go to the HelpScreen state
            if (inputHelper.MouseLeftButtonPressed() &&
            helpButton.BoundingBox.Contains(game.ScreenToWorld(inputHelper.MousePosition)))
            {
                GoToState(GameState.HelpScreen);
            }
        }

        else if (currentState == GameState.TitleScreen || currentState == GameState.GameOver)
        {
            if (inputHelper.KeyPressed(Keys.Space))
            {
                Reset();
                GoToState(GameState.Playing);
            }
        }

        else if (currentState == GameState.HelpScreen)
        {
            if (inputHelper.KeyPressed(Keys.Space))
                GoToState(GameState.Playing);
        }

    }

        public void AddScore(int points)
{
 Score += points;
        jewelCart.PushBack();
    }

 public override void Reset()
 {
 base.Reset();
Score = 0;
 }



    SpriteGameObject AddOverlay(string spriteName)
    {
        // create the object
        SpriteGameObject result = new SpriteGameObject(spriteName);
        // set the center as its origin
        result.SetOriginToCenter();

        // move the object to the center of the screen
        Vector2 worldCenter = new Vector2(Size.X / 2.0f, Size.Y / 2.0f);
        result.LocalPosition = worldCenter;

        // add the object to the list of children
        AddChild(result);

        return result;
    }

    void GoToState(GameState newState)
    {
        currentState = newState;
        titleScreen.Visible = currentState == GameState.TitleScreen;
        helpScreen.Visible = currentState == GameState.HelpScreen;
        gameOverScreen.Visible = currentState == GameState.GameOver;
    }




    VisibilityTimer AddComboImageWithTimer(string spriteName)
    {
        // create and add the image
        SpriteGameObject image = new SpriteGameObject(spriteName);
        image.Visible = false;
        image.LocalPosition = new Vector2(800, 400);
        AddChild(image);
        // create and add the timer, with that image as its target
        VisibilityTimer timer = new VisibilityTimer(image);
        AddChild(timer);
        return timer;
    }

    public void DoubleComboScored()
    {
        timer_double.StartVisible(3);
    }
    public void TripleComboScored()
    {
        timer_triple.StartVisible(3);
    }


}

