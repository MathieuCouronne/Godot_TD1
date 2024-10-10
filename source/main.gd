extends Node

@export var mob_scene: PackedScene

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	# Charger et initialiser le Custom MainLoop en C#
	var custom_mainloop = preload("res://source/OverrideMainLoop.cs").new()
	
	# Replacer la boucle principale par votre CustomMainLoop
	#Engine.set_main_loop(custom_mainloop)
	
	$Player.start($StartPosition.position)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass



	
