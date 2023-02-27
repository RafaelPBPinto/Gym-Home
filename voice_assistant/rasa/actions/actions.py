# This files contains your custom actions which can be used to run
# custom Python code.
#
# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions


# This is a simple example for a custom action which utters "Hello World!"

# from typing import Any, Text, Dict, List
#
# from rasa_sdk import Action, Tracker
# from rasa_sdk.executor import CollectingDispatcher
#
#
# class ActionHelloWorld(Action):
#
#     def name(self) -> Text:
#         return "action_hello_world"
#
#     def run(self, dispatcher: CollectingDispatcher,
#             tracker: Tracker,
#             domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
#
#         dispatcher.utter_message(text="Hello World!")
#
#         return []

from typing import Any, Text, Dict, List

from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet

import paho.mqtt.publish as publish
import json

class ActionResetName(Action):

    def name(self) -> Text:
        return "action_reset_name"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        return [SlotSet("username", None)]

class ActionMostrarPlanosExercicios(Action):
    
    def name(self) -> Text:
        return "action_mostrar_planos_exercicios"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "planos_exercicios"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        
        return []
        
class ActionResetOpcao(Action):
    
    def name(self) -> Text:
        return "action_reset_opcao"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        return [SlotSet("opcao", None)]

class ActionSelecionarOpcao(Action):
    def name(self) -> Text:
        return "action_selecionar_opcao"
    
    def run(self, dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        slot_value = tracker.get_slot('opcao')
        
        msg = {"comando": "selecionar_opcao", "opcao": slot_value}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")

        return []

class ActionComecar(Action):
    
    def name(self) -> Text:
        return "action_comecar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "comecar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        
        return []

class ActionAvancar(Action):
    
    def name(self) -> Text:
        return "action_avancar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "avancar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        
        return []

class ActionVoltar(Action):
    
    def name(self) -> Text:
        return "action_voltar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "voltar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        
        return []

class ActionTerminar(Action):
    
    def name(self) -> Text:
        return "action_terminar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "terminar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        
        return []

class ActionVirarCamaraEsquerda(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_esquerda"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "esquerda"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        
        return []
        
class ActionVirarCamaraDireita(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_direita"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "direita"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        
        return []
        
class ActionVirarCamaraCima(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_cima"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "cima"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        
        return []
    
class ActionVirarCamaraBaixo(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_baixo"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        msg = {"comando": "baixo"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        
        return []