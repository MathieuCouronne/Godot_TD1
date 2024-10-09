using Godot;
using System;
namespace VampireSurv.source;

[GlobalClass]
public partial class OverrideMainLoop : SceneTree
{
    public static OverrideMainLoop Instance;
    public SaveManager SaveManager;
    //public LevelManager LevelManager;

    
    public override void _Initialize()
    {
        GD.Print("Hi");
        if (Instance == null)
            Instance = this;
    }
    
    public override bool _Process(double delta)
    {
        return  base._Process(delta);
    }

    public override void _Finalize()
    {
        GD.Print("Bye");
    }

    public static OverrideMainLoop Get()
    {
        return Instance;
    }

    //public LevelManager GetLevelManager()
    //{
     //   return LevelManager;
    //}

    public SaveManager GetSaveManager()
    {
        return SaveManager.Get();
    }
}