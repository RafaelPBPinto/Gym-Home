# Referências:
#   https://www.youtube.com/watch?v=3Mga7_8bYpw&t=113s
#   https://www.youtube.com/watch?v=F62wb_jfUUw&t=495s
#   https://www.youtube.com/watch?v=esWPb4i4vyY&list=PLtFHvora00y-LU27sZGzzpHSNSpR1pUCW&index=8

from vosk import Model, KaldiRecognizer
import pyaudio
import pyttsx3 as tts
import requests
import sys

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
    while bot_message != "Adeus":
        if text == "olá maria" or text == "hey maria" or text == "oi maria":
            print(f"Assistente: {bot_message}")
            speaker.say(bot_message)
            speaker.runAndWait()
            assistant_listening_loop()
            bot_message = ""
            text = ""
        else:
            data = stream.read(4096, exception_on_overflow = False)
            if recognizer.AcceptWaveform(data):
                text = recognizer.Result()
                text = text[14:-3]
                text = text.lower()
                print(text)
                if text != "" :
                    r = requests.post('http://localhost:5002/webhooks/rest/webhook', json={"message": text})
                    for i in r.json():
                        bot_message = i['text']
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
            if text != "" :
                r = requests.post('http://localhost:5002/webhooks/rest/webhook', json={"message": text})
                for i in r.json():
                    bot_message = i['text']
                    print(f"Assistente: {bot_message}")
                    speaker.say(bot_message)
                    speaker.runAndWait()
    
if __name__ == "__main__":
    main()  
