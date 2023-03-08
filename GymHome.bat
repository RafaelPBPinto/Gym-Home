cd /d %~dp0
start cmd.exe /k "cd voice_assistant\rasa && test_voice.bat"
start cmd.exe /k "cd Database\test && python main.py"
start cmd.exe /k "cd Cam\reolinkapipyV5\tests && python myTest.py"
start cmd.exe /k "cd UI\exe && GymHome.exe"
