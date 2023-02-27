import os
import unittest
import time
import keyboard
from configparser import RawConfigParser
from reolinkapi import Camera
from reolinkapi.mixins.ptz import PtzAPIMixin

def read_config(props_path: str) -> dict:
    config = RawConfigParser()
    assert os.path.exists(props_path), f"Path does not exist: {props_path}"
    config.read(props_path)
    return config


class TestCamera(unittest.TestCase, PtzAPIMixin):
    @classmethod
    def setUpClass(cls) -> None:
        cls.config = read_config(os.path.abspath('/mnt/c/Users/repol/Documents/github/Gym-Home/Cam/reolinkapipyV5/secrets.cfg'))
    # Atualizar o caminho, CORRIGIR ESTE PEDAÇO DE CODIGO, QUANDO TIVER TUDO A FUNFAR
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

            

if __name__ == '__main__':
    unittest.main()
