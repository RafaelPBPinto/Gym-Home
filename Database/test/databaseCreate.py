#from . import db
from threading import RLock
from copy import deepcopy
from datetime import (datetime,timezone)
#import bcrypt
import sqlite3

conn = sqlite3.connect('PlanosUser.db')

query = ('''CREATE TABLE IF NOT EXISTS Utilizador 
        (ID INTEGER PRIMARY KEY,
        Email TEXT NOT NULL,
        Nome TEXT NOT NULL,
        Passe TEXT NOT NULL);''')
conn.execute(query)

query = ('''CREATE TABLE IF NOT EXISTS Plano 
        (ID INTEGER PRIMARY KEY,
        Nome TEXT NOT NULL,
        Descricao TEXT NOT NULL);''')
conn.execute(query)

query = ('''CREATE TABLE IF NOT EXISTS Exercicio 
        (ID INTEGER PRIMARY KEY,
        Nome TEXT NOT NULL,
        Tipo TEXT NOT NULL,
        Duracao TEXT NOT NULL,
        Descricao TEXT NOT NULL);''')
conn.execute(query)

query = ('''CREATE TABLE IF NOT EXISTS Imagem 
        (ID INTEGER PRIMARY KEY,
        Nome TEXT NOT NULL,
        Descricao TEXT NOT NULL,
        RefID_exercicio  INTEGER NOT NULL,
        FOREIGN KEY(RefID_exercicio) REFERENCES Exercicio(ID)
        );''')
conn.execute(query)

query = ('''CREATE TABLE IF NOT EXISTS Parentesco 
        (ID INTEGER PRIMARY KEY,
        RefID_plano INTEGER NOT NULL,
        RefID_exercicio INTEGER NOT NULL,
        FOREIGN KEY(RefID_plano) REFERENCES Plano(ID)
        FOREIGN KEY(RefID_exercicio) REFERENCES Eexercicio(ID)
        );''')
conn.execute(query)

query = ('''CREATE TABLE IF NOT EXISTS Sessao 
        (ID INTEGER PRIMARY KEY,
        RefID_utilizador INTEGER NOT NULL,
        RefID_plano INTEGER NOT NULL,
        Dia TEXT NOT NULL,
        FOREIGN KEY(RefID_plano) REFERENCES Plano(ID)
        FOREIGN KEY(RefID_utilizador) REFERENCES Utilizador(ID)
        );''')
        
conn.execute(query)
conn.close()
