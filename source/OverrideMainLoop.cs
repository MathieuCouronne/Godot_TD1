using Godot;
namespace VampireSurv.source;

[GlobalClass]
public partial class OverrideMainLoop : SceneTree
{
    public static OverrideMainLoop Instance;
    //public LevelManager LevelManager;
    //public SaveManager SaveManager;
    
    public override void _Initialize()
    {
        GD.Print("Hi");
        if (Instance == null)
            Instance = this;
    }

    public static OverrideMainLoop Get()
    {
        return Instance;
    }

    /*public LevelManager GetLevelManager()
    {
        return LevelManager;
    }

    public SaveManager GetSaveManager()
    {
        return saveManager;
    }*/
}