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
    cursor = conn.cursor()
   
    query = "DROP TABLE IF EXISTS Imagem;"

    cursor.execute(query)
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