using Godot;

public partial class SceneService : Service
{
    private Node _sceneContainerNode;
    private SceneTransition _transitionNode;
    private Node _currentScene;
    public Vector2I playerInitialNewSceneCoords { get; private set; }

    public void SetSceneContainerNode(Node sceneConteiner)
    {
        _sceneContainerNode = sceneConteiner;
    }
    public void SetTransitionNode(SceneTransition transitionNode)
    {
        _transitionNode = transitionNode;
    }

    public void LoadInitialScene(PackedScene initialScene)
    {
        StartSceneChange(initialScene);
    }
    public void StartSceneChange(PackedScene scene)
    {
        CallDeferred(nameof(LoadSceneDeferred), scene);
        GameServices.Current.Audio.StopMusic(); 
    }
    private async void LoadSceneDeferred(PackedScene scene) // lógica de StartSceneChange "empacotada" para poder usar na função CallDeferred
    {
        if (_currentScene != null)
        {

            Tween fadeOut = _transitionNode.FadeOut();
            FreezeSceneProcess(true); // Para o processamento do sceneContainer

            fadeOut.Finished += () =>
            {
                _currentScene.QueueFree();
                ContinueSceneChange(scene);
            };
            return;
        }
        ContinueSceneChange(scene);
    }
    private void ContinueSceneChange(PackedScene scene)
    {
        _currentScene = scene.Instantiate<Node>();
        _sceneContainerNode.AddChild(_currentScene);
    }
    public void FinishSceneChange()
    {
        // Volta o processamento do SceneContainer para o padrão
        FreezeSceneProcess(false);
        Tween fadeIn = _transitionNode.FadeIn();
    }

    public void SetPlayerInitialNewSceneCoords(Vector2I coords)
    {
        playerInitialNewSceneCoords = coords;
    }
    public void QuitGame() // TEMPORARY POSITION !!! In the future, the objetive is change this function for a "global game state manager"
    {
        Tween fadeOut = _transitionNode.FadeOut();
        fadeOut.Finished += () => // save logic is going to be here
        {
            GetTree().Quit();
        };
    }
    public void FreezeSceneProcess(bool turnOn)
    {
        if (turnOn)
        {
            _sceneContainerNode.ProcessMode = Node.ProcessModeEnum.Disabled;
            return;
        }
        _sceneContainerNode.ProcessMode = Node.ProcessModeEnum.Inherit;
    }
}
