extends CharacterBody2D

@export var speed = 400
var screen_size

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	screen_size = get_viewport_rect().size
	hide()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	var velocity = Vector2.ZERO # The player's movement vector.
	if Input.is_action_pressed("move_right"):
		velocity.x += 1
		$AnimatedSprite2D.animation = "right"
	if Input.is_action_pressed("move_left"):
		velocity.x -= 1
		$AnimatedSprite2D.animation = "left"
	if Input.is_action_pressed("move_down"):
		velocity.y += 1
		$AnimatedSprite2D.animation = "down"
	if Input.is_action_pressed("move_up"):
		velocity.y -= 1
		$AnimatedSprite2D.animation = "up"

	if velocity.length() > 0:
		velocity = velocity.normalized() * speed
		$AnimatedSprite2D.play()
	else:
		$AnimatedSprite2D.animation = "idle_down"
		#$AnimatedSprite2D.stop()
		
	position += velocity * delta
	position = position.clamp(Vector2.ZERO, screen_size)
	move_and_slide()
	
func save():
	var save_dict = {
		"filename" : get_scene_file_path(),
		"parent" : get_parent().get_path(),
		"pos_x" : position.x, # Vector2 is not supported by JSON
		"pos_y" : position.y,
	}
	return save_dict
	
func start(pos):
	position = pos
	show()
	$CollisionShape2D.disabled = false
	$AnimatedSprite2D.animation = "idle_down"
	$AnimatedSprite2D.play()
