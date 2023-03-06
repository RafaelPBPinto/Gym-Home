import sqlite3

def add_exercise_to_table(nome, tipo, duracao, descricao):
    conn = sqlite3.connect('../PlanosUser.db')
    query = "INSERT INTO Exercicio (Nome, Tipo, Duracao, Descricao) VALUES (?, ?, ?, ?)"
    conn.execute(query, (nome, tipo, duracao, descricao))
    conn.commit()
    conn.close()
