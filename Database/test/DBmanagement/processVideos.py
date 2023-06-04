import sqlite3

def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')

    insertVideoPath('Ponte', 'Ponte.mp4', 1)
    insertVideoPath('Agachamento Cadeira', 'AgachamentoCadeira.mp4', 2)
    insertVideoPath('Gémeos', 'Gemeos.mp4', 3)
    insertVideoPath('Elevação de Perna','ElevacaoDePerna.mp4', 4)
    insertVideoPath('Flexão na Parede', 'Flexaonaparede.mp4', 5)
    insertVideoPath('Abdominal', 'Abdominal.mp4', 6)
    insertVideoPath('Apoio numa Perna', 'ApoioNumaPerna.mp4', 7)
    insertVideoPath('Abdominal Crunch', 'AbdominalCrunch.mp4', 8)
    insertVideoPath('Alongamento da Curva Lateral', 'AlongamentoCurvaLateral.mp4', 9)
    insertVideoPath('Alongamento Peitoral', 'AlongamentoPeito.mp4', 10)
    insertVideoPath('Andar em Pés Seguidos', 'AndarEmPeSeguido.mp4', 11)
    insertVideoPath('Chuto Lateral', 'ChutoLateral.mp4', 12)
    insertVideoPath('Alongamento de Ombro', 'AlongamentoDeOmbro.mp4', 13)
    insertVideoPath('Alongamento Quadriceps', 'AlongamentoQuadriceps.mp4',14)
    insertVideoPath('Alongamento dos Glúteos', 'AlongamentoGluteo.mp4', 15)


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