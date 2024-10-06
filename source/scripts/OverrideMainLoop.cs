using Godot;
namespace VampireSurv.source;

[GlobalClass]
public partial class OverrideMainLoop : SceneTree
{
    private static OverrideMainLoop _instance;
    private LevelManager _levelManager;
    //public SaveManager _saveManager;
    
    public override void _Initialize()
    {
        GD.Print("Hi from Manager");
        if (_instance == null)
            _instance = this;
        if (_levelManager == null)
            _levelManager = new LevelManager(this);
    }

    public static OverrideMainLoop Get()
    {
        return _instance;
    }

    public LevelManager GetLevelManager()
    {
        return _levelManager;
    }

    /*public SaveManager GetSaveManager()
    {
        return saveManager;
    }*/
}