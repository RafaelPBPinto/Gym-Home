# Referências:
#   https://www.youtube.com/watch?v=3Mga7_8bYpw&t=113s
#   https://www.youtube.com/watch?v=F62wb_jfUUw&t=495s

from vosk import Model, KaldiRecognizer
import pyaudio
import pyttsx3 as tts
import json
import unidecode
import string
from random import randrange

def main():
    # https://alphacephei.com/vosk/models
    # Colocar o path do vosk-model-small-pt-0.3
    model = Model("vosk-model-small-pt-0.3")
    recognizer = KaldiRecognizer(model, 16000)

    with open('intents.json') as json_data:
        intents = json.load(json_data)

    speaker = tts.init()
    speaker.setProperty('rate', 150)
    speaker.setProperty('voice', "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\Voices\Tokens\MSTTS_V110_ptPT_Helia")
    print("Assistente: Olá, eu sou a Maria. Como posso ajudar?")
    speaker.say("Olá, eu sou a Maria. Como posso ajudar?")
    speaker.runAndWait()

    mic = pyaudio.PyAudio()
    stream = mic.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True, frames_per_buffer=8192)
    stream.start_stream()

    while True:
        data = stream.read(4096, exception_on_overflow = False)
        if recognizer.AcceptWaveform(data):
            text = recognizer.Result()
            text = text[14:-3]
            recognize = False
            print(text)
            text = normalize_text(text)
            if text == "stop" or text == "parar" or text == "sair" or text == "desligar":
                print("Assistente: Adeus!")
                speaker.say("adeus")
                speaker.runAndWait()
                speaker.stop()
                break
            else:
                for tag in intents['intents']:
                    for pattern in tag['patterns']:
                        pattern = normalize_text(pattern)
                        if pattern in text:
                            response = tag['responses'][randrange(len(tag['responses']))]
                            print("Assistente: ", response)
                            response = normalize_text(response)
                            speaker.say(response)
                            speaker.runAndWait()
                            recognize = True
                            break
                if not recognize:
                    print("Assistente: Não percebi, desculpa.")
                    speaker.say("não percebi, desculpa.")
                    speaker.runAndWait()
                    recognize = False

def normalize_text(text):
    text = text.lower()
    text = unidecode.unidecode(text)
    for character in string.punctuation:
        text = text.replace(character, '')
    return text

if __name__ == "__main__":
    main()  
