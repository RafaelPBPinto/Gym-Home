import sqlite3

# Open a connection to the database
conn = sqlite3.connect('exercicios.db')

# Get the name of the exercise from the user
name = input("Enter the name of the exercise: ")

# Get the description of the exercise from the user
description = input("Enter the description of the exercise: ")

# Get the path to the image from the user
image_path = input("Enter the path to the image: ")

# Open the image file in binary mode
with open(image_path, 'rb') as f:
    # Read the contents of the file into a binary string
    image_data = f.read()

# Get the name of the author from the user
author = input("Enter the name of the author: ")

# Get the duration of the exercise from the user
duration = input("Enter the duration of the exercise: ")

# Use a parameterized query to insert the data into the table
query = 'INSERT INTO exercises (name, descricao, imagem, autor, duracao) VALUES (?, ?, ?, ?, ?)'
conn.execute(query, (name, description, image_data, author, duration))

# Commit the transaction
conn.commit()

# Close the connection
conn.close()
