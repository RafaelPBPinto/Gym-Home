import sqlite3

def processData():
    conn = sqlite3.connect('..\PlanosUser.db')
    cursor = conn.cursor()
   
    sql = "DROP TABLE IF EXISTS Imagem;"

    cursor.execute(sql)
    conn.commit()

    conn.close()
    
if __name__ == '__main__':
    processData()