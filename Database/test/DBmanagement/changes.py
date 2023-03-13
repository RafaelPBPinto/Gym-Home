import sqlite3

# --------------
# ONLY FOR TESTS
# --------------
#
# --------------
# ONLY FOR TESTS
# --------------

def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')  
    cursor = conn.cursor()

# change Nome, and Descricao of exercicio with ID = 15
    # query = """ UPDATE Exercicio
    #             SET Nome = 'Alongamento dos glúteos', Descricao = 'Sentado numa cadeira, levante uma perna e segure-a com as mãos por forma a colocá-la e a segurá-la em cima da perna oposta. Por fim incline-se para a frente para sentir o alongamento. Repita o mesmo procedimento com a outra perna.'
    #             WHERE ID = 15
    #         """

# change Nome and ImagemBinary of exercicio with ID = 8 in table Imagem
    # query = """ UPDATE Imagem
    #             SET ImagemBinary = ?
    #             WHERE RefID_exercicio = 8
    #         """
    # image = convert2BinData('imagem\AbdominalCrunch.png') 

    #cursor.execute(query,(image,))

# change Nome and VideoName of exercicio with ID = 8 in table Video
    query = """ UPDATE Exercicio
                SET Nome = 'Flexão contra a parede'
                WHERE ID = 5
            """

    cursor.execute(query)
    conn.commit()

    conn.close()


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

# --------------
# ONLY FOR TESTS
# --------------
#
# --------------
# ONLY FOR TESTS
# --------------