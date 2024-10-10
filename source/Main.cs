using Godot;
using System;

namespace VampireSurv.source;
public partial class Main : Node
{
	// Equivalent de la méthode _ready() en GDScript

		
	public override void _Ready()
	{
		// Accéder à un noeud enfant par son nom et appeler une méthode dessus
		var player = GetNode<Node>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition").Position;
		// Appeler la méthode "start" sur le player
		player.Call("start", startPosition);
		OverrideMainLoop loop = OverrideMainLoop.Get();
		SaveManager saveManager = loop.GetSaveManager();
		saveManager.LoadGame("savegame.json");
		
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			OverrideMainLoop loop = OverrideMainLoop.Get();
			SaveManager saveManager = loop.GetSaveManager();
			saveManager.SaveGame("savegame.json");
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
}
