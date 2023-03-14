cd /d %~dp0
start /min cmd.exe /k "venv\Scripts\activate.bat && rasa run -m models --endpoints endpoints.yml --port 5002 --credentials credentials.yml"
start /min cmd.exe /k "venv\Scripts\activate.bat && rasa run actions"
start /min cmd.exe /k "venv\Scripts\activate.bat && python assistant_loop.py"