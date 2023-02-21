import sqlite3
from flask import Blueprint,  request, jsonify
from werkzeug.security import generate_password_hash, check_password_hash

path = Blueprint('path', __name__)

"""
@auth.route('/', methods=['GET', 'POST'])
def login():
    if request.method == 'POST':
        email = request.form.get('email')
        password = request.form.get('password')
        
        conn = sqlite3.connect('PlanosUser.db')
        query = f"SELECT Passe FROM Utilizador WHERE Email = '{email}'"
        result = conn.execute(query)
        result = result.fetchone()
        #conn.close()
        
        if result:
            if check_password_hash(result[0], password):
                flash('Logged in successfully!', category='success')
                query = f"SELECT ID FROM Utilizador WHERE Email = '{email}'"
                ID = conn.execute(query)
                ID = ID.fetchone()
                conn.close()
                return redirect(url_for('auth.profile', user_id=ID[0]))
            else:
                flash('Incorrect password, try again.', category='error')
        else:
            flash('Email does not exist.', category='error')

    return render_template('login.html')

@auth.route('/signup', methods=['GET', 'POST'])
def signup():
    if request.method == 'POST':
        email = request.form.get('email')
        username = request.form.get('username')
        password = request.form.get('password')
        
        if len(email) < 4:
            flash('Email must be greater than 3 characters.', category='error') 
        elif len(username) < 3:
            flash('Username must be greater than 2 characters.', category='error')
        elif len(password) < 7:
            flash('Password must be greater than 6 characters.', category='error')
        else:
            conn = sqlite3.connect('PlanosUser.db')
            query = f"INSERT INTO Utilizador (Email, Nome, Passe) VALUES ('{email}', '{username}', '{generate_password_hash(password, method='sha256')}')"
            conn.execute(query)
            conn.commit()
            conn.close()

            flash('Account created!', category='success')
            
            return redirect('/')
        

    return render_template('register.html')
"""