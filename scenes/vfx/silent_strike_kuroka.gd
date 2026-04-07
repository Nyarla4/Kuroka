extends Node2D

@onready var animator = $AnimatedSprite2D

func _ready():
	animator.frame = 0
	
	if animator.sprite_frames:
		animator.play("frame")
		
	animator.animation_finished.connect(queue_free)
