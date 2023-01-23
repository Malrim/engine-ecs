using Engine.ECS.Entities;
using Engine.ECS.GameSystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.ECS.GameScenes;

public abstract class GameScene
{
    public Game Game { get; private set; }

    private World _world;
    private ContentManager _content;
    private SpriteBatch _spriteBatch;
    private EntityController _entityController;
    private GameSystemsManager _gameSystemsManager;
    
    private bool _isLoaded;
    
    protected void RegisterSystems() { }
    
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
        _entityController = new EntityController();
        _gameSystemsManager = new GameSystemsManager();
        
        RegisterSystems();
        LoadContent(_content);

        _entityController.Update();
        _isLoaded = true;
    }

    internal void Unload()
    {
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

    protected abstract void BeginDraw(SpriteBatch spriteBatch, GameTime gameTime);
    protected abstract void EndDraw(SpriteBatch spriteBatch, GameTime gameTime);

    internal void Draw(GameTime gameTime)
    {
        BeginDraw(_spriteBatch, gameTime);
        _gameSystemsManager.Draw(_spriteBatch, gameTime);
        EndDraw(_spriteBatch, gameTime);
    }

    public Entity CreateEntity() => _entityController.CreateEntity(this);

    public Entity GetEntity(uint entityId) => _entityController.GetEntity(entityId);

    public void DestroyEntity(uint entityId) => _entityController.RemoveEntity(entityId);

    public void LoadOtherGameScene(GameScene gameScene) => _world.LoadGameScene(gameScene);
}