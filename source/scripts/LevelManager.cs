using Godot;
using System;

namespace VampireSurv.source;

public partial class LevelManager : Node
{
    // Fonction pour charger un niveau
    /*public void LoadLevel(string scenePath)
    {
        // Charger la scène en mémoire
        var newScene = (PackedScene)ResourceLoader.Load(scenePath);
        
        if (newScene == null)
        {
            GD.PrintErr($"La scène {scenePath} n'a pas pu être chargée.");
            return;
        }

        // Accéder à la scène actuelle (s'il y en a une)
        var currentScene = GetTree().CurrentScene;
        
        // Si une scène est déjà chargée, on la supprime
        if (currentScene != null)
        {
            currentScene.QueueFree();
        }

        // Instancier et ajouter la nouvelle scène
        Node sceneInstance = newScene.Instantiate();
        GetTree().Root.AddChild(sceneInstance);
        GetTree().CurrentScene = sceneInstance;
    }*/
    private SceneTree _tree;
    
    public LevelManager(SceneTree tree)
    {
        _tree = tree;
    }

    public void LoadLevel(string scenePath)
    {
        if (_tree == null)
        {
            GD.PrintErr("Le SceneTree est null.");
            return;
        }

        var newScene = (PackedScene)ResourceLoader.Load(scenePath);
        if (newScene == null)
        {
            GD.PrintErr($"La scène '{scenePath}' n'a pas pu être chargée.");
            return;
        }

        var currentScene = _tree.CurrentScene;
        if (currentScene != null)
        {
            currentScene.QueueFree();
        }

        Node sceneInstance = newScene.Instantiate();
        _tree.Root.AddChild(sceneInstance);
        _tree.CurrentScene = sceneInstance;
    }
}