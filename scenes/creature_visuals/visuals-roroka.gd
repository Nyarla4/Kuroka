extends AnimatedSprite2D

func _ready():
	# 애니메이션이 바뀔 때와 끝날 때 신호를 받도록 연결합니다.
	animation_changed.connect(_on_animation_changed)
	animation_finished.connect(_on_animation_finished)

# 엔진이 어떤 애니메이션을 요구하는지 훔쳐보는 디버그 함수
func _on_animation_changed():
	print("[로로카 디버그] 엔진이 실행한 애니메이션: ", animation)

# 애니메이션 1회 재생이 끝났을 때 실행되는 함수
func _on_animation_finished():
	# 엔진이 die나 faint를 요구했고, 그 재생이 끝났다면?
	if animation == "die" or animation == "faint":
		print("[로로카 디버그] 사망/기절 연기 끝! 스르륵 퇴장합니다.")
		
		# 1. 0.5초 동안 투명도(Alpha)를 0으로 서서히 줄입니다.
		var tween = create_tween()
		tween.tween_property(self, "modulate:a", 0.0, 0.5)
		
		# 2. 투명해지는 연출이 끝나면 화면(메모리)에서 완전히 삭제!
		tween.tween_callback(get_parent().queue_free)
