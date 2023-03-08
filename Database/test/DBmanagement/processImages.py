import sqlite3
def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')    

#######################################################################
    #img de exer1
    photo = convert2BinData('imagem\ponte.jpg') 
    insertImage("Ponte",photo,1)
    #img de exer2
    photo = convert2BinData('imagem\AgachamentoCadeira.jpg') 
    insertImage('Agachamento na cadeira',photo,2)
    #img de exer3
    photo = convert2BinData('imagem\Gemeos.jpg') 
    insertImage('Gemeos',photo,3)
    #img de exer4
    photo = convert2BinData('imagem\legExtension.jpg') 
    insertImage('Leg Extension',photo,4)
    #img de exer5
    photo = convert2BinData('imagem\FlexaoParede.jpg') 
    insertImage('Flexão contra a parede',photo,5)
    # photo = convert2BinData('imagem\FlexaoParede1.jpg') 
    # insertImage('Flexão contra a parede',photo,5)
    # photo = convert2BinData('imagem\FlexaoParede2.jpg') 
    # insertImage('Flexão contra a parede',photo,5)
    #img de exer6
    photo = convert2BinData('imagem\Abdominal.png')  
    insertImage('Abdominal',photo,6)
    #img de exer7
    photo = convert2BinData('imagem\singleLegStand.jpg') 
    insertImage('Apoio numa perna',photo,7)
    #img de exer8
    photo = convert2BinData('imagem\TandemStanding.jpg') 
    insertImage('Pés seguidos',photo,8)
    #img de exer9
    photo = convert2BinData('imagem\sideBendStretch.jpg') 
    insertImage('Alongamento curva lateral',photo,9)
    #img de exer10
    photo = convert2BinData('imagem\AlongamentoPeito.jpg') 
    insertImage('Alongamento peito',photo,10)
    #img de exer11
    photo = convert2BinData('imagem\TandemWalking.png')
    insertImage('Andar em pés seguidos',photo,11)
    #img de exer12
    photo = convert2BinData('imagem\ChutoLateral.jpg') 
    insertImage('Chuto lateral',photo,12)
    #img de exer13
    photo = convert2BinData('imagem\AlongamentoOmbro.jpg') 
    insertImage('Alongamento ombro',photo,13)
    #img de exer14
    photo = convert2BinData('imagem\AlongamentoQuadricep.jpg') 
    insertImage('Alongamento quadríceps',photo,14)
    #img de exer15
    photo = convert2BinData('imagem\AlongamentoAbdominal.jpg') 
    insertImage('Alongamento abdominal',photo,15)


def convert2BinData(filename):
    with open(filename, 'rb') as f:
        binData = f.read()
    return binData

def insertImage(nome,img,id):
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')
    #query = f"INSERT INTO Imagem (Nome,ImagemBinary,RefID_exercicio) VALUES ('{nome}','{img}','{id}')"
    query = """ INSERT INTO Imagem (Nome, ImagemBinary,RefID_exercicio) VALUES (?, ?, ?)"""
    data_tuple = (nome,img,id)
    conn.execute(query,data_tuple)
    conn.commit()
    conn.close()

if __name__ == '__main__':
    processData()