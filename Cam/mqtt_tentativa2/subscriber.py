#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
A small example subscriber
"""
import paho.mqtt.client as paho
import json


def on_message(mosq, obj, msg):
    print ("%-20s %d %s" % (msg.topic, msg.qos, msg.payload))
    mosq.publish('pong', 'ack', 0)
    message = msg.payload
    message = json.loads(message)
    print (message ['comando'])



if __name__ == '__main__':
    client = paho.Client()
    client.on_message = on_message

    #client.tls_set('root.ca', certfile='c1.crt', keyfile='c1.key')
    client.connect("127.0.0.1", 1883, 60)

    client.subscribe("comandos/voz/camara", 0)

    while client.loop() == 0:
        pass

# vi: set fileencoding=utf-8 :