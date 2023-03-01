import sqlite3
import openpyxl
import os
def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    conn.execute('PRAGMA foreign_keys = ON')

    # add Videos to each exercise
    listaExer = 'exercicio.xlsx'
    if os.path.exists(listaExer):
        xlsx = openpyxl.load_workbook(listaExer)
        sheet = xlsx.active
        linha = sheet.rows
        for row in linha:
            if row[2].value != None:    # Eu acho que alguns erros de None acontecem porque se adiciona uma linha em branco no final do ficheiro excel
                nome      = row[0].value
                tipo      = row[1].value
                duracao   = int(row[2].value)
                descricao = row[3].value
                if nome != None:
                    conn.execute('PRAGMA foreign_keys = ON')
                    query = f"INSERT INTO Exercicio (Nome,Tipo,Duracao,Descricao) VALUES ('{nome}', '{tipo}', '{duracao}', '{descricao}')"
                    conn.execute(query)
                    conn.commit()
                    # add videos to each exercise
                    query = f"SELECT ID FROM Exercicio WHERE Nome = '{nome}'"
                    cursor = conn.execute(query)
                    for row in cursor:
                        id_exercicio = row[0]
                    # add videos to each exercise
                    query = f"INSERT INTO Video (Nome, VideoBinary, RefID_exercicio) VALUES ('{nome}', NULL, '{id_exercicio}')"
                    conn.execute(query)
                    conn.commit()
                    # add images to each exercise
                    query = f"INSERT INTO Imagem (Nome, ImagemBinary, RefID_exercicio) VALUES ('{nome}', NULL, '{id_exercicio}')"
                    conn.execute(query)
                    conn.commit()
                    