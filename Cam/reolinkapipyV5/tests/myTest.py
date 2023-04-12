import os
import unittest
import paho.mqtt.client as paho
import time
import json
from configparser import RawConfigParser
from reolinkapi import Camera
from reolinkapi.mixins.ptz import PtzAPIMixin

def read_config(props_path: str) -> dict:
    config = RawConfigParser()
    assert os.path.exists(props_path), f"Path does not exist: {props_path}"
    config.read(props_path)
    return config

class TestCamera(unittest.TestCase, PtzAPIMixin):

    def __init__(self):
        self.setUpClass()
        self.setUp()

    def cam(self):
        return self.cam

    def setUpClass(cls) -> None:
        cls.config = read_config('../secrets.cfg')

    def setUp(self) -> None:
        self.cam = Camera(self.config.get('camera', 'ip'), self.config.get('camera', 'username'), self.config.get('camera', 'password'))

    def test_camera(self):
        """Test that camera connects and gets a token"""
        self.assertTrue(self.cam.ip == self.config.get('camera', 'ip'))
        self.assertTrue(self.cam.token != '')
        print("fez o test Camera") 
   
    def test_move_camera(self):
        """Test that camera moves"""
    
        while True:
            # aguarda entrada do usuário
            command = input("Digite um comando (a, d, w, s, " ", exit): ")

            # verifica qual comando foi digitado
            if command == 'a':
                self.cam.move_left()
            elif command == 'd':
                self.cam.move_right()
            elif command == 'w':
                self.cam.move_up()
            elif command == 's':
                self.cam.move_down()
            elif command == ' ':
                self.cam.stop_ptz()
            elif command == 'exit':
                # encerra o loop
                break
            else:
                # comando inválido
                print("Comando inválido")       

def on_message(mosq, obj, msg):
        camar = TestCamera()
        
        print ("%-20s %d %s" % (msg.topic, msg.qos, msg.payload))
        mosq.publish('pong', 'ack', 0)
        message = msg.payload
        message = json.loads(message)
        print (message ['comando'])

        if message['comando'] == 'esquerda':
            camar.cam.move_left()
            time.sleep(1)
            camar.cam.stop_ptz()
        elif message['comando'] == 'direita':
            camar.cam.move_right()
            time.sleep(1)
            camar.cam.stop_ptz()
        elif message['comando'] == 'cima':
            camar.cam.move_up()
            # time.sleep(1)
            # camar.cam.stop_ptz()
        elif message['comando'] == 'baixo':
            camar.cam.move_down()
            # time.sleep(1)
            # camar.cam.stop_ptz()
        elif message['comando'] == 'auto':
            camar.cam.auto_movement()

        # elif message['comando'] == 'parar':
        #     camar.cam.stop_ptz()
        else:
            # comando inválido
            print("Comando inválido")

if __name__ == '__main__':
    
    client = paho.Client()
    client.on_message = on_message
    #client.tls_set('root.ca', certfile='c1.crt', keyfile='c1.key')
    client.connect("127.0.0.1", 1883, 60)

    client.subscribe("comandos/voz/camara", 0)

    while client.loop() == 0:
        pass

    unittest.main()
    