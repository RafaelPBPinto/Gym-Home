#!/usr/bin/env python
# -*- coding: utf-8 -*-
""" 
Publish some messages to queue
"""
import paho.mqtt.publish as publish

msgs = [{'topic': "comandos/voz/camara", 'payload': "jump"}]
host = "localhost"

if __name__ == '__main__':
    string  = {'comando': 'direita'}
    import json
    publish.single(topic="comandos/voz/camara", payload= json.dumps(string), hostname=host)



# vi: set fileencoding=utf-8 :