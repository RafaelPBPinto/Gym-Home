cd /d %~dp0
start /min cmd.exe /k "cd voice_assistant\rasa && test_voice.bat"
start /min cmd.exe /k "cd Database\test && python main.py"
start /min cmd.exe /k "cd Cam\reolinkapipyV5\tests && python myTest.py"
start /min cmd.exe /k "cd UI\exe && GymHome.exe"
