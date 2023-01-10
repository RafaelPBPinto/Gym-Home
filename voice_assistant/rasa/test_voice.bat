cd /d %~dp0
start cmd.exe /k "venv\Scripts\activate.bat && rasa run -m models --endpoints endpoints.yml --port 5002 --credentials credentials.yml"
start cmd.exe /k "venv\Scripts\activate.bat && rasa run actions"
start cmd.exe /k "venv\Scripts\activate.bat && python assistant_loop.py"