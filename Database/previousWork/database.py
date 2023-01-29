import sqlite3

# Connect to the database
conn = sqlite3.connect('exercicios.db')

# Create a cursor
cursor = conn.cursor()

# Create a table
cursor.execute('''CREATE TABLE exercises (id INTEGER PRIMARY KEY, name TEXT, descricao TEXT, imagem BLOB, autor TEXT, duracao TEXT)''')

# Commit the transaction
conn.commit()

# Close the connection
conn.close()