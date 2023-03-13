import sqlite3
import openpyxl
import os
def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')

    # video = convert2BinData('video\Ponte.mp4')
    # insertVideo('Ponte', video, 1)
    #insertVideoPath('Ponte', 'C:/Users/hbilab/Desktop/video/Ponte.mp4', 1)
    insertVideoPath('Ponte', 'Ponte.mp4', 1)

    # video = convert2BinData('video\AgachamentoCadeira.mp4')
    # insertVideo('Agachamento Cadeira', video, 2)
    #insertVideoPath('Agachamento Cadeira', 'C:/Users/hbilab/Desktop/video/AgachamentoCadeira.mp4', 2)
    insertVideoPath('Agachamento Cadeira', 'AgachamentoCadeira.mp4', 2)

    # video = convert2BinData('video\Gemeos.mp4')
    # insertVideo('Gémeos', video, 3)
    # insertVideoPath('Gémeos', 'C:/Users/hbilab/Desktop/video/Gemeos.mp4', 3)
    insertVideoPath('Gémeos', 'Gemeos.mp4', 3)

    # video = convert2BinData('video\ElevacaoDePerna.mp4')
    # insertVideo('Elevação de Perna', video, 4)
    #insertVideoPath('Elevação de Perna', 'C:/Users/hbilab/Desktop/video/ElevacaoDePerna.mp4', 4)
    insertVideoPath('Elevação de Perna','ElevacaoDePerna.mp4', 4)

    # video = convert2BinData('video\Flexaonaparede.mp4')
    # insertVideo('Flexão na Parede', video, 5)
    #insertVideoPath('Flexão na Parede', 'C:/Users/hbilab/Desktop/video/Flexaonaparede.mp4', 5)
    insertVideoPath('Flexão na Parede', 'Flexaonaparede.mp4', 5)

    # video = convert2BinData('video\Abdominal.mp4')
    # insertVideo('Abdominal', video, 6)
    #insertVideoPath('Abdominal', 'C:/Users/hbilab/Desktop/video/Abdominal.mp4', 6)
    insertVideoPath('Abdominal', 'Abdominal.mp4', 6)

    # video = convert2BinData('video\ApoioNumaPerna.mp4')
    # insertVideo('Abdominal', video, 7)
    #insertVideoPath('Apoio numa Perna', 'C:/Users/hbilab/Desktop/video/ApoioNumaPerna.mp4', 7)
    insertVideoPath('Apoio numa Perna', 'ApoioNumaPerna.mp4', 7)

    # video = convert2BinData('video\Pesseguidos.mp4')
    # insertVideo('Pes Seguidos', video, 8)
    #insertVideoPath('Pés Seguidos', 'C:/Users/hbilab/Desktop/video/Pesseguidos.mp4', 8)
    insertVideoPath('Abdominal Crunch', 'AbdominalCrunch.mp4', 8)

    # video = convert2BinData('video\AlongamentoCurvaLateral.mp4')
    # insertVideo('Alongamento da Curva Lateral', video, 9)
    #insertVideoPath('Alongamento da Curva Lateral', 'C:/Users/hbilab/Desktop/video/AlongamentoCurvaLateral.mp4', 9)
    insertVideoPath('Alongamento da Curva Lateral', 'AlongamentoCurvaLateral.mp4', 9)

    # video = convert2BinData('video\AlongamentoPeito.mp4')
    # insertVideo('Alongamento Peitoral', video, 10)
    #insertVideoPath('Alongamento Peitoral', 'C:/Users/hbilab/Desktop/video/AlongamentoPeito.mp4', 10)
    insertVideoPath('Alongamento Peitoral', 'AlongamentoPeito.mp4', 10)

    # video = convert2BinData('video\AndarEmPeSeguido.mp4')
    # insertVideo('Andar em Pés Seguidos', video, 11)
#     insertVideoPath('Andar em Pés Seguidos', 'C:/Users/hbilab/Desktop/video/AndarEmPeSeguido.mp4', 11)
    insertVideoPath('Andar em Pés Seguidos', 'AndarEmPeSeguido.mp4', 11)

    # video = convert2BinData('video\ChutoLateral.mp4')
    # insertVideo('Chuto Lateral', video, 12)
    #insertVideoPath('Chuto Lateral', 'C:/Users/hbilab/Desktop/video/ChutoLateral.mp4', 12)
    insertVideoPath('Chuto Lateral', 'ChutoLateral.mp4', 12)

    # video = convert2BinData('video\AlongamentoDeOmbro.mp4')
    # insertVideo('Alongamento de Ombro', video, 13)
   # insertVideoPath('Alongamento de Ombro', 'C:/Users/hbilab/Desktop/video/AlongamentoDeOmbro.mp4', 13)
    insertVideoPath('Alongamento de Ombro', 'AlongamentoDeOmbro.mp4', 13)

    # video = convert2BinData('video\AlongamentoQuadriceps.mp4')
    # insertVideo('Alongamento Quadriceps', video, 14)
    #insertVideoPath('Alongamento Quadriceps', 'C:/Users/hbilab/Desktop/video/AlongamentoQuadriceps.mp4',14)
    insertVideoPath('Alongamento Quadriceps', 'AlongamentoQuadriceps.mp4',14)

    # video = convert2BinData('video\AlongamentoAbdominal.mp4')
    # insertVideo('Alongamento Abdominal', video, 15)
    #insertVideoPath('Alongamento abdominal', 'C:/Users/hbilab/Desktop/video/AlongamentoAbdominal.mp4', 15)
    insertVideoPath('Alongamento dos Glúteos', 'AlongamentoGluteo.mp4', 15)



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

def insertVideoPath(nome,path,id):
        conn = sqlite3.connect('..\PlanosUser.db')
        conn.execute('PRAGMA foreign_keys = ON')
       
        query = """ INSERT INTO Video (Nome, VideoName,RefID_exercicio) VALUES (?, ?, ?)"""
        data_tuple = (nome,path,id)
        conn.execute(query,data_tuple)
        conn.commit()
        conn.close()

if __name__ == '__main__':
    processData()