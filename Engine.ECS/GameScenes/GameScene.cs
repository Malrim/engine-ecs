using Engine.ECS.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.GameScenes;

public abstract class GameScene
{
    public Game Game { get; private set; }
    public EntityController EntityController { get; private set; }

    private World _world;
    private ContentManager _content;
    private SpriteBatch _spriteBatch;
    
    private bool _isLoaded;
    
    protected virtual void LoadContent(ContentManager content) { }
    
    internal void Load(World world, Game game)
    {
        if (_isLoaded)
        {
            throw new Exception("Game scene is already loaded!");
        }

        Game = game;
        _world = world;
        _content = new ContentManager(game.Content.ServiceProvider, "Content");
        _spriteBatch = new SpriteBatch(game.GraphicsDevice);
        EntityController = new EntityController(this);
        
        LoadContent(_content);

        EntityController.Update();
        _isLoaded = true;
    }

    internal void Unload()
    {
        EntityController = null;
        _content.Unload();
        _content.Dispose();
        _content = null;
        _spriteBatch.Dispose();
        _spriteBatch = null;
        Game = null;
        _world = null;
        
        _isLoaded = false;
    }
    
    internal void Update(GameTime gameTime)
    {
        EntityController.Update();
    }

    protected abstract void BeginDraw(SpriteBatch spriteBatch, GameTime gameTime);
    protected abstract void EndDraw(SpriteBatch spriteBatch, GameTime gameTime);

    internal void Draw(GameTime gameTime)
    {
        BeginDraw(_spriteBatch, gameTime);
        EndDraw(_spriteBatch, gameTime);
    }

    public void LoadOtherGameScene(GameScene gameScene) => _world.LoadGameScene(gameScene);
}