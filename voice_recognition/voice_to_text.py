import speech_recognition
import whisper

def main():
    
    model = whisper.load_model("base")
    recognizer = speech_recognition.Recognizer()
    
    while True:
        try: 
            with speech_recognition.Microphone() as mic:
                
                recognizer.adjust_for_ambient_noise(mic, duration=0.2)
                print('Listening...')
                audio = recognizer.listen(mic)
                
                with open('audio.wav', 'wb') as f:
                    f.write(audio.get_wav_data())            

                # load audio and pad/trim it to fit 30 seconds
                audio = whisper.load_audio("audio.wav")
                audio = whisper.pad_or_trim(audio)

                # make log-Mel spectrogram and move to the same device as the model
                mel = whisper.log_mel_spectrogram(audio).to(model.device)

                # decode the audio
                options = whisper.DecodingOptions()
                result = whisper.decode(model, mel, options)

                print('Text:')
                # print the recognized text
                print(result.text)
            
        except speech_recognition.UnknownValueError():
            recognizer = speech_recognition.Recognizer()
            continue

if __name__ == '__main__':
    main()
    