using Engine.Entities;
using Engine.GameSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.GameScenes;

public abstract class GameScene
{
    public Game Game { get; private set; }
    
    protected SpriteSortMode SpriteSortMode { get; set; } = SpriteSortMode.Deferred;
    protected SamplerState SamplerState  { get; set; }
    protected BlendState BlendState  { get; set; }
    protected Color SceneColor { get; set; } = Color.CornflowerBlue;

    private World _world;
    private ContentManager _content;
    private SpriteBatch _spriteBatch;
    private EntityController _entityController;
    private GameSystemsManager _gameSystemsManager;
    
    private bool _isLoaded;
    
    protected virtual void SetUp() { }
    
    protected virtual void RegisterSystems() { }
    
    protected virtual void LoadContent() { }
    
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
        _entityController = new EntityController();
        _gameSystemsManager = new GameSystemsManager();
        
        SetUp();
        RegisterSystems();
        LoadContent();

        _entityController.Update();
        _isLoaded = true;
    }

    internal void Unload()
    {
        _gameSystemsManager = null;
        _entityController = null;
        _content.Unload();
        _content.Dispose();
        _content = null;
        _spriteBatch.Dispose();
        _spriteBatch = null;
        Game = null;
        _world = null;
        
        _isLoaded = false;
    }

    protected void RegisterSystem(GameSystem gameSystem) =>
        _gameSystemsManager.RegisterSystem(gameSystem, this, _entityController);
    
    internal void Update(GameTime gameTime)
    {
        _entityController.Update();
        _gameSystemsManager.Update(gameTime);
    }

    internal void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(SceneColor);
        _spriteBatch.Begin(SpriteSortMode, BlendState, SamplerState);
        _gameSystemsManager.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();
    }

    public void AddEntity(Action<Entity, ContentManager> setUpEntity) => setUpEntity(_entityController.CreateEntity(this), _content);

    public Entity GetEntity(uint entityId) => _entityController.GetEntity(entityId);

    public void DestroyEntity(uint entityId) => _entityController.RemoveEntity(entityId);

    public void LoadGameScene(GameScene gameScene) => _world.LoadGameScene(gameScene);
}