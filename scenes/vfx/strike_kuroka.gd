extends Node2D

@onready var animator = $AnimatedSprite2D
@onready var audio = $AudioStreamPlayer

func _ready():
	animator.frame = 0
	
	if animator.sprite_frames and audio.stream:
		animator.play("frame")
		audio.play()
		
	audio.finished.connect(_on_audio_finished)

func _on_audio_finished():
	# 실행 과정이 끝났으므로 메모리에서 안전하게 제거합니다.
	queue_free()
