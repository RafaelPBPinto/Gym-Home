# Referências:
#   https://www.youtube.com/watch?v=3Mga7_8bYpw&t=113s
#   https://www.youtube.com/watch?v=F62wb_jfUUw&t=495s
#   https://www.youtube.com/watch?v=esWPb4i4vyY&list=PLtFHvora00y-LU27sZGzzpHSNSpR1pUCW&index=8

from vosk import Model, KaldiRecognizer
import pyaudio
import pyttsx3 as tts
import requests
import sys
import json
import paho.mqtt.publish as publish
from rasa_sdk import Tracker

# https://alphacephei.com/vosk/models
# Colocar o path do vosk-model
model = Model("vosk-model-small-pt-0.3") # 31MB
recognizer = KaldiRecognizer(model, 16000)

speaker = tts.init()
speaker.setProperty('rate', 150)
speaker.setProperty('voice', "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Voices\Tokens\MSTTS_V110_ptPT_Helia")

mic = pyaudio.PyAudio()
stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=8192)
stream.start_stream()

def main():
    bot_message = ""
    text = ""
    while text != "desligar":
        if text == "olá maria" or text == "hey maria" or text == "oi maria":
            if text != "" :
                try:
                    r = requests.post('http://localhost:5002/webhooks/rest/webhook', json={"message": text})
                    for i in r.json():
                        bot_message = i['text']
                    print(f"Assistente: {bot_message}")
                    write_log("Assistente: " + bot_message + "\n")
                    speak(bot_message)
                    publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "listening"}), hostname="localhost")
                    write_log("Listening..." + "\n")
                    assistant_listening_loop()
                    bot_message = ""
                    text = ""
                except:
                    error = "Rasa ainda não está pronto"
                    print(error)
                    speak(error)
        else:
            data = stream.read(4096, exception_on_overflow = False)
            if recognizer.AcceptWaveform(data):
                text = recognizer.Result()
                text = text[14:-3]
                text = text.lower()
                print(text)
                write_log("User: " + text + "\n")
                publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "legenda", "legenda": text}), hostname="localhost")     
    sys.exit(0)

def assistant_listening_loop():
    bot_message = ""
    while bot_message != "Adeus":
        data = stream.read(4096, exception_on_overflow = False)
        if recognizer.AcceptWaveform(data):
            text = recognizer.Result()
            text = text[14:-3]
            text = text.lower()
            print(text)
            write_log("User: " + text + "\n")
            publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "legenda", "legenda": text}), hostname="localhost")
            if text != "" :
                publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "no_listening"}), hostname="localhost")
                write_log("No Listening..." + "\n")
                try:
                    r = requests.post('http://localhost:5002/webhooks/rest/webhook', json={"message": text})
                    for i in r.json():
                        bot_message = i['text']
                    print(f"Assistente: {bot_message}")
                    write_log("Assistente: " + bot_message + "\n")
                    speak(bot_message)
                except:
                    error = "Rasa está offline"
                    print(error)
                    speak(error)
                    break             
                publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "listening"}), hostname="localhost")
                write_log("Listening..." + "\n")   
    publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "no_listening"}), hostname="localhost")
    write_log("No Listening..." + "\n")
    
def write_log(text):
    with open("log.txt", "a") as log:
        log.write(text)

def speak(text):
    speaker.say(text)
    speaker.runAndWait()
    
if __name__ == "__main__":
    main()  
