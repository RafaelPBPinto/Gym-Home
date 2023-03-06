import sqlite3
import openpyxl
import os
def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')
    video = convert2BinData('video\Ponte.mp4')
    insertVideo('Ponte', video, 1)
    video = convert2BinData('video\AgachamentoCadeira.mp4')
    insertVideo('Agachamento Cadeira', video, 2)
    video = convert2BinData('video\Gemeos.mp4')
    insertVideo('Gémeos', video, 3)
    video = convert2BinData('video\ElevacaoDePerna.mp4')
    insertVideo('Elevação de Perna', video, 4)
    video = convert2BinData('video\Flexaonaparede.mp4')
    insertVideo('Flexão na Parede', video, 5)
    video = convert2BinData('video\Abdominal.mp4')
    insertVideo('Abdominal', video, 6)
    video = convert2BinData('video\ApoioNumaPerna.mp4')
    insertVideo('Abdominal', video, 7)
    video = convert2BinData('video\Pesseguidos.mp4')
    insertVideo('Pes Seguidos', video, 8)
    video = convert2BinData('video\AlongamentoCurvaLateral.mp4')
    insertVideo('Alongamento da Curva Lateral', video, 9)
    video = convert2BinData('video\AlongamentoPeito.mp4')
    insertVideo('Alongamento Peitoral', video, 10)
    video = convert2BinData('video\AndarEmPeSeguido.mp4')
    insertVideo('Andar em Pés Seguidos', video, 11)
    video = convert2BinData('video\ChutoLateral.mp4')
    insertVideo('Chuto Lateral', video, 12)
    video = convert2BinData('video\AlongamentoDeOmbro.mp4')
    insertVideo('Alongamento de Ombro', video, 13)
    video = convert2BinData('video\AlongamentoQuadriceps.mp4')
    insertVideo('Alongamento Quadriceps', video, 14)
    video = convert2BinData('video\AlongamentoAbdominal.mp4')
    insertVideo('Alongamento Abdominal', video, 15)


def convert2BinData(filename):
        with open(filename, 'rb') as f:
            binData = f.read()
        return binData

def insertVideo(nome,vd,id):
        conn = sqlite3.connect('..\PlanosUser.db')
        conn.execute('PRAGMA foreign_keys = ON')
       
        query = """ INSERT INTO Video (Nome, VideoBinary,RefID_exercicio) VALUES (?, ?, ?)"""
        data_tuple = (nome,vd,id)
        conn.execute(query,data_tuple)
        conn.commit()
        conn.close()

if __name__ == '__main__':
    processData()