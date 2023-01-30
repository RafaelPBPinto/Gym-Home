import sqlite3
import openpyxl
import os
def processData():
    conn = sqlite3.connect('PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')
    name = 'exercicio.xlsx'
    plaExer = 'ExercicioPlanos.xlsx'
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
                    
                    query = f"INSERT INTO Exercicio (Nome,Tipo, Descricao) VALUES ('{nome}', '{tipo}', '{descricao}')"
                    conn.execute(query)
                    conn.commit()

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
                   
                    query = f"INSERT INTO ExercicioPlano (Series,Repeticoes Duracao, Ordem,  RefID_plano, RefID_exercicio)\
                            VALUES ('{series}', '{repeticao}', '{duracao}', '{ordem}', '{RefID_plano}', '{RefID_exercicio}')"
                    conn.execute(query)
                    conn.commit()

    ###############################################################################            
    #Planos:
    #1
    autor = "Fisio. José Silva"
    nome = 'Força'
    descricao = 'Ajuda os diferentes músculos do seu corpo\n para se tornar mais forte e mais poderoso'
    query = f"INSERT INTO Plano (Nome, Descricao, Autor) VALUES ('{nome}', '{descricao}', '{autor}')"
    conn.execute(query)
    conn.commit()
    #2
    nome = 'Flexibilidade'
    descricao = 'Assim como os alongamentos, os exercícios de flexibilidade\n são técnicas utilizadas para aumentar a extensão do tecido conjuntivo muscular.'
    query = f"INSERT INTO Plano (Nome, Descricao, Autor) VALUES ('{nome}', '{descricao}', '{autor}')"
    conn.execute(query)
    conn.commit()
    #3
    nome = 'Resistencia'
    descricao = 'Quando fazemos exercícios, nossos músculos precisam de mais energia do que quando descansamos.'
    query = f"INSERT INTO Plano (Nome, Descricao, Autor) VALUES ('{nome}', '{descricao}', '{autor}')"
    conn.execute(query)
    conn.commit()
    
    #########################################################################################
    #Sessão:
    dia1 = 'Segunda'
    dia2 = 'Quarta'
    dia3 = 'Sexta'
    #Três users para teste, cada user vai ter o mesmo plano mais vamos alternar o dia
    #user 1:
    query1 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{1}', '{1}', '{dia1}')"
    query2 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{1}', '{2}', '{dia2}')"
    query3 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{1}', '{3}', '{dia3}')"
    conn.execute(query1)
    conn.execute(query2)
    conn.execute(query3)
    conn.commit()
    
    #user 2:
    query1 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{2}', '{3}', '{dia1}')"
    query2 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{2}', '{1}', '{dia2}')"
    query3 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{2}', '{2}', '{dia3}')"
    conn.execute(query1)
    conn.execute(query2)
    conn.execute(query3)
    conn.commit()

    #user 3:
    query1 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{3}', '{2}', '{dia1}')"
    query2 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{3}', '{3}', '{dia2}')"
    query3 = f"INSERT INTO Sessao (RefID_utilizador, RefID_plano, Dia) VALUES ('{3}', '{1}', '{dia3}')"
    conn.execute(query1)
    conn.execute(query2)
    conn.execute(query3)
    conn.commit()
    conn.close()

    #######################################################################
    #img de exer1
    photo = convert2BinData('imagem\prancha.png')
    insertImage('Ponte',photo,1)
    #img de exer2
    photo = convert2BinData('imagem\agachamentonacadeira.jpg')
    insertImage('Agachamento na cadeira',photo,2)
    #img de exer3
    photo = convert2BinData('imagem\treinocadeiraisometrica.jpg')
    insertImage('Agachamento Isometrica',photo,3)
    #img de exer4
    photo = convert2BinData('imagem\stiff_unilateral.jpg')
    insertImage('Stiff Unilateral',photo,4)
    #img de exer5
    photo = convert2BinData('imagem\Flexao_de_braco.png')
    insertImage('Flexão de Braço',photo,5)
    #img de exer6
    photo = convert2BinData('imagem\Abdominal.png')
    insertImage('Abdominal',photo,6)
    #img de exer7
    photo = convert2BinData('imagem\corda.jpg')
    insertImage('Pular Corda',photo,7)
    #img de exer8
    photo = convert2BinData('imagem\corrida.jpg')
    insertImage('Corrida Estacionário',photo,8)
    #img de exer9
    photo = convert2BinData('imagem\Triceps_no_banco.jpg')
    insertImage('Tríceps na Cadeira',photo,9)
    #img de exer10
    photo = convert2BinData('imagem\Bicicleta_imaginaria.png')
    insertImage('Bicicleta Imaginária',photo,10)
    #img de exer11
    photo = convert2BinData('imagem\Elevacoes.jpg')
    insertImage('Elevações',photo,11)
    #img de exer12
    photo = convert2BinData('imagem\Agachamentos.jpg')
    insertImage('Agachamento',photo,12)
    #img de exer13
    photo = convert2BinData('imagem\Quadriceps.jpg')
    insertImage('Quadriceps',photo,13)
    #img de exer14
    photo = convert2BinData('imagem\Spine_Twist.jpg')
    insertImage('Spine Twist',photo,14)
    #img de exer15
    photo = convert2BinData('imagem\Flexao_coxo_femural.jpg')
    insertImage('Flexão coxo-Femural',photo,15)


def convert2BinData(filename):
    with open(filename, 'rb') as f:
        binData = f.read()
    return binData

def insertImage(nome,img,id):
    conn = sqlite3.connect('PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')
    query = f"INSERT INTO Imagem (Nome,Imagem,RefID_exercicio) VALUES ('{nome}', '{img}','{id}')"
    conn.execute(query)
    conn.commit()
    conn.close()

if __name__ == '__main__':
    processData()