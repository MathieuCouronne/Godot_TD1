namespace VampireSurv.source;
using Godot;


public partial class SaveManager: Node
{
    public static SaveManager Instance { get; private set; }
    
    public override void _Ready()
    {
        GD.Print("SaveManager Ready");
        if (Instance == null)
            Instance = this;
    }
    
    public static SaveManager Get()
    {
        return Instance;
    }
    
    public void SaveGame(string path)
    {
        using var saveFile = FileAccess.Open(path, FileAccess.ModeFlags.Write);

        var tree = OverrideMainLoop.Get();
        var saveNodes = tree.GetNodesInGroup("Save");
        GD.Print("Saving game...", tree.HasGroup("Save"));
        GD.Print("Saving game...", saveNodes);

        foreach (Node saveNode in saveNodes)
        {
            if (string.IsNullOrEmpty(saveNode.SceneFilePath))
            {
                GD.Print($"Save node '{saveNode.Name}' is not an instanced scene, skipped");
                continue;
            }

            if (!saveNode.HasMethod("save"))
            {
                GD.Print($"Save node '{saveNode.Name}' is missing a save() function, skipped");
                continue;
            }
            GD.Print("Saving no...");
            var nodeData = saveNode.Call("save");

            var jsonString = Json.Stringify(nodeData);

            saveFile.StoreLine(jsonString);
        }
    }
    
public void LoadGame(string path)
{
    if (!FileAccess.FileExists(path))
    {
        return;
    }

    var tree = OverrideMainLoop.Get();
    var saveNodes = tree.GetNodesInGroup("Save");
    
    foreach (Node saveNode in saveNodes)
    {
        saveNode.QueueFree();
    }

    using var saveFile = FileAccess.Open(path, FileAccess.ModeFlags.Read);

    while (saveFile.GetPosition() < saveFile.GetLength())
    {
        var jsonString = saveFile.GetLine();

        var json = new Json();
        var parseResult = json.Parse(jsonString);
        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
            continue;
        }

        var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);
        var newObjectScene = GD.Load<PackedScene>(nodeData["filename"].ToString());
        var newObject = newObjectScene.Instantiate<Node>();
        GetNode(nodeData["parent"].ToString()).AddChild(newObject);
        newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["pos_x"], (float)nodeData["pos_y"]));
        newObject.AddToGroup("Save");
        newObject.Call("start", new Vector2((float)nodeData["pos_x"], (float)nodeData["pos_y"]));

    }
}
}