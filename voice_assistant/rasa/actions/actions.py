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
from rasa_sdk.events import SlotSet, UserUtteranceReverted

import paho.mqtt.publish as publish
import json
import unidecode

log = open("log.txt", "a")

class ActionDefaultFallback(Action):
    """Executes the fallback action and goes back to the previous state
    of the dialogue"""

    def name(self) -> Text:
        return "action_default_fallback"

    async def run(
        self,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> List[Dict[Text, Any]]:
        log.write("Actions: " + "No_understand: " + "enter\n")
        print(tracker.latest_message["intent"].get("confidence"))
        if tracker.latest_message["intent"].get("confidence") > 0.8:
            dispatcher.utter_message(response="utter_default")
        
        publish.single(topic="comandos/voz/UI", payload=json.dumps({"comando": "no_understand"}), hostname="localhost")
        log.write("Actions: " + "No_understand: " + "exit\n")
        
        # Revert user message which led to fallback.
        return [UserUtteranceReverted()]

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
        
        log.write("Actions: " + "Mostrar_planos_exercicios: " + "enter\n")
        msg = {"comando": "selecionar_opcao", "opcao": "planos_exercicios"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Mostrar_planos_exercicios: " + "exit\n")
        
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

        log.write("Actions: " + "Selecionar_opcao: " + "enter\n")
        options = ["um", "dois", "tres", "quatro", "cinco", "seis", "sete", "oito", "nove", "dez"]
        slot_value = tracker.get_slot('opcao')
        slot_value = slot_value.lower()
        slot_value = unidecode.unidecode(slot_value)
        
        for word in slot_value.split():
            if word in options:
                slot_value = word
                break
        
        
        msg = {"comando": "selecionar_opcao", "opcao": slot_value}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Selecionar_opcao: " + "exit\n")
        
        return []

class ActionComecar(Action):
    
    def name(self) -> Text:
        return "action_comecar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Comecar: " + "enter\n")
        msg = {"comando": "comecar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Comecar: " + "exit\n")
        
        return []

class ActionAvancar(Action):
    
    def name(self) -> Text:
        return "action_avancar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Avancar: " + "enter\n")
        msg = {"comando": "avancar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Avancar: " + "exit\n")
        
        return []

class ActionVoltar(Action):
    
    def name(self) -> Text:
        return "action_voltar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Voltar: " + "enter\n")
        msg = {"comando": "voltar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Voltar: " + "exit\n")
        
        return []

class ActionTerminar(Action):
    
    def name(self) -> Text:
        return "action_terminar"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Terminar: " + "enter\n")
        msg = {"comando": "terminar"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Terminar: " + "exit\n")
        
        return []

class ActionVirarCamaraEsquerda(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_esquerda"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Virar_camara_esquerda: " + "enter\n")  
        msg = {"comando": "esquerda"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Virar_camara_esquerda: " + "exit\n")
        
        return []
        
class ActionVirarCamaraDireita(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_direita"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Virar_camara_direita: " + "enter\n")
        msg = {"comando": "direita"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Virar_camara_direita: " + "exit\n")
        
        return []
        
class ActionVirarCamaraCima(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_cima"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Virar_camara_cima: " + "enter\n")
        msg = {"comando": "cima"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Virar_camara_cima: " + "exit\n")
        
        return []
    
class ActionVirarCamaraBaixo(Action):
    
    def name(self) -> Text:
        return "action_virar_camara_baixo"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Virar_camara_baixo: " + "enter\n")
        msg = {"comando": "baixo"}
        publish.single(topic="comandos/voz/camara", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Virar_camara_baixo: " + "exit\n")
        
        return []

class ActionScrollUp(Action):
    
    def name(self) -> Text:
        return "action_scroll_up"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Scroll_up: " + "enter\n")
        msg = {"comando": "scroll_up"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Scroll_up: " + "exit\n")
        
        return []

class ActionScrollDown(Action):
    
    def name(self) -> Text:
        return "action_scroll_down"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Scroll_down: " + "enter\n")
        msg = {"comando": "scroll_down"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Scroll_down: " + "exit\n")
        
        return []

class ActionMostrarTodosExercicios(Action):
    
    def name(self) -> Text:
        return "action_mostrar_todos_exercicios"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Mostrar_todos_exercicios: " + "enter\n")
        msg = {"comando": "selecionar_opcao", "opcao": "todos_exercicios"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Mostrar_todos_exercicios: " + "exit\n")
        
        return []

class ActionPaginaInicial(Action):
    
    def name(self) -> Text:
        return "action_pagina_inicial"
    
    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        log.write("Actions: " + "Pagina_inicial: " + "enter\n")
        msg = {"comando": "sair"}
        publish.single(topic="comandos/voz/UI", payload=json.dumps(msg), hostname="localhost")
        log.write("Actions: " + "Pagina_inicial: " + "exit\n")
        
        return []
    