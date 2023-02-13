import sqlite3
import openpyxl
import os
def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')
    name = 'exercicio.xlsx'
    plaExer = 'ExercicioPlanos.xlsx'
    plano = 'Planos.xlsx'
    sessao = 'Sessao.xlsx'
    if os.path.exists(name):
            xlsx = openpyxl.load_workbook(name)
            sheet = xlsx.active
            #dimensao = sheet.dimensions
            #values = sheet[dimensao]
            linha = sheet.rows
            for row in linha:
                nome      = row[0].value
                tipo      = row[1].value
                #duracao   = row[2].value
                descricao = row[3].value
                if nome != None:
                    conn.execute('PRAGMA foreign_keys = ON')
                    query = f"INSERT INTO Exercicio (Nome,Tipo, Descricao) VALUES ('{nome}', '{tipo}', '{descricao}')"
                    conn.execute(query)
                    conn.commit()
    ###############################################################################            
    #Planos:
    if os.path.exists(plano):
        xlsx = openpyxl.load_workbook(plano)
        sheet = xlsx.active
        linha = sheet.rows
        for row in linha:

            nome      = row[0].value
            descricao = row[1].value
            autor     = row[2].value

            if nome != None:

                query = f"INSERT INTO Plano (Nome, Descricao, Autor) VALUES ('{nome}', '{descricao}', '{autor}')"
                conn.execute(query)
                conn.commit()

    #plano de exercicios: 
    if os.path.exists(plaExer):
            xlsx = openpyxl.load_workbook(plaExer)
            sheet = xlsx.active
            linha = sheet.rows
            for row in linha:

                tipo            = row[0].value
                series          = int(row[1].value)
                repeticao       = int(row[2].value)
                duracao         = int(row[3].value)
                ordem           = int(row[4].value)
                RefID_exercicio = int(row[5].value)
                RefID_plano     = int(row[6].value)
                
                if tipo != None:
                   
                    query = f"INSERT INTO ExercicioPlano (Series,Repeticoes, Duracao, Ordem,  RefID_plano, RefID_exercicio)\
                            VALUES ('{series}', '{repeticao}', '{duracao}', '{ordem}', '{RefID_plano}', '{RefID_exercicio}')"
                    conn.execute(query)
                    conn.commit()

   
    
    #########################################################################################
    #Sessão:
    if os.path.exists(sessao):
        xlsx = openpyxl.load_workbook(sessao)
        sheet = xlsx.active
        linha = sheet.rows
        for row in linha:

            RefID_utilizador  = int(row[0].value)
            RefID_plano       = int(row[1].value)
            Dia               = row[2].value
            if Dia != None:
                query = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{RefID_utilizador}', '{RefID_plano}', '{Dia}')"
                conn.execute(query)
                conn.commit()


    #######################################################################
    #img de exer1
    photo = convert2BinData('imagem\prancha.png')
    insertImage("Ponte",photo,1)
    #img de exer2
    photo = convert2BinData('imagem\ganacadeira.png')
    insertImage('Agachamento na cadeira',photo,2)
    #img de exer3
    photo = convert2BinData('imagem\Treinocadeiraisometrica.png')
    insertImage('Agachamento Isometrica',photo,3)
    #img de exer4
    photo = convert2BinData('imagem\stiff_unilateral.png')
    insertImage('Stiff Unilateral',photo,4)
    #img de exer5
    photo = convert2BinData('imagem\Flexao_de_braco.png')
    insertImage('Flexão de Braço',photo,5)
    #img de exer6
    photo = convert2BinData('imagem\Abdominal.png')
    insertImage('Abdominal',photo,6)
    #img de exer7
    photo = convert2BinData('imagem\corda.png')
    insertImage('Pular Corda',photo,7)
    #img de exer8
    photo = convert2BinData('imagem\corrida.png')
    insertImage('Corrida Estacionário',photo,8)
    #img de exer9
    photo = convert2BinData('imagem\Triceps_no_banco.png')
    insertImage('Tríceps na Cadeira',photo,9)
    #img de exer10
    photo = convert2BinData('imagem\Bicicleta_imaginaria.png')
    insertImage('Bicicleta Imaginária',photo,10)
    #img de exer11
    photo = convert2BinData('imagem\Elevacoes.png')
    insertImage('Elevações',photo,11)
    #img de exer12
    photo = convert2BinData('imagem\Agachamentos.png')
    insertImage('Agachamento',photo,12)
    #img de exer13
    photo = convert2BinData('imagem\Quadriceps.png')
    insertImage('Quadriceps',photo,13)
    #img de exer14
    photo = convert2BinData('imagem\Spine_Twist.png')
    insertImage('Spine Twist',photo,14)
    #img de exer15
    photo = convert2BinData('imagem\Flexao_coxo_femural.png')
    insertImage('Flexão coxo-Femural',photo,15)


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