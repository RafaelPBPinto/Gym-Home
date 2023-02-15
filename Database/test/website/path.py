import sqlite3
from flask import Blueprint,  request, jsonify
from werkzeug.security import generate_password_hash, check_password_hash

path = Blueprint('path', __name__)

@path.route('/', methods=['POST'])
def login():
        data = request.json()
        email = data['email']
        password = data['password']
        
        conn = sqlite3.connect('PlanosUser.db')
        query = f"SELECT Passe FROM Utilizador WHERE Email = '{email}'"
        result = conn.execute(query)
        result = result.fetchone()
        
        if result:
            if check_password_hash(result[0], password):
                query = f"SELECT ID FROM Utilizador WHERE Email = '{email}'"
                ID = conn.execute(query)
                ID = ID.fetchone()
                conn.close()
                return jsonify({'success': 'Logged in successfully!'}), 200
            else:
                return jsonify({'error': 'Incorrect password, try again.'}), 400
        else:
            return jsonify({'error': 'Email does not exist.'}), 400
   

@path.route('/signup', methods=[ 'POST'])
def signup():

        data = request.json()
        email = data['email']
        username = data['username']
        password = data['password']
       
        if len(email) < 4:
            return jsonify({'error': 'Email must be greater than 3 characters.'}), 400
        elif len(username) < 3:
            return jsonify({'error': 'Username must be greater than 2 characters.'}), 400
        elif len(password) < 7:
            return jsonify({'error': 'Password must be greater than 6 characters.'}), 400
        else:
            conn = sqlite3.connect('PlanosUser.db')
            query = f"INSERT INTO Utilizador (Email, Nome, Passe) VALUES ('{email}', '{username}', '{generate_password_hash(password, method='sha256')}')"
            conn.execute(query)
            conn.commit()
            conn.close()

            return jsonify({'success': 'Account created!'}), 200

