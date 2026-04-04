extends Node2D

@onready var animator = $AnimatedSprite2D
@onready var audio = $AudioStreamPlayer

func _ready():
	animator.frame = 0
	
	if animator.sprite_frames and audio.stream:
		animator.play("frame")
		audio.play()
		
	# 애니메이션 종료 시 숨기기
	animator.animation_finished.connect(func(): animator.hide())
	
	# 씬 삭제 기준을 오디오 종료로
	audio.finished.connect(_on_audio_finished)

func _on_audio_finished():
	queue_free()
