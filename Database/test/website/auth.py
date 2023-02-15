import sqlite3
from flask import Blueprint, render_template, request, flash, redirect, url_for, jsonify
from werkzeug.security import generate_password_hash, check_password_hash
import base64

auth = Blueprint('auth', __name__)

@auth.route('/getExercises', methods=['GET', 'POST'])
def getExercises():
    if request.method == 'GET':
        conn = sqlite3.connect('PlanosUser.db')
        query = f"SELECT Exercicio.Nome, Exercicio.Tipo, Exercicio.Duracao, Exercicio.Descricao, Imagem.ImagemBinary FROM Exercicio INNER JOIN Imagem ON Exercicio.ID = Imagem.RefID_exercicio"
        result = conn.execute(query)
        result = result.fetchall()
        conn.close()
        responses = []
        for row in result:
            img_data = base64.b64encode(row[4]).decode('utf-8')            
            response = {'nome': row[0], 'tipo': row[1], 'duracao': row[2], 'descricao': row[3], 'imagem': img_data}
            responses.append(response)
        return jsonify(responses), 200
    return jsonify({'error': 'Invalid request method'}), 400

@auth.route('/getExercise/id=<id>', methods=['GET', 'POST'])
def getExercise(id):
    if request.method == 'GET':
        conn = sqlite3.connect('PlanosUser.db')
        query = f"SELECT Exercicio.Nome, Exercicio.Tipo, Exercicio.Duracao, Exercicio.Descricao, Imagem.ImagemBinary FROM Exercicio INNER JOIN Imagem ON Exercicio.ID = Imagem.RefID_exercicio WHERE Exercicio.ID = '{id}'"
        result = conn.execute(query)
        result = result.fetchall()
        conn.close()
        img_data = base64.b64encode(result[0][4]).decode('utf-8')
        response = {'nome': result[0][0], 'tipo': result[0][1], 'duracao': result[0][2], 'descricao': result[0][3], 'imagem': img_data}
        return jsonify(response), 200
    return jsonify({'error': 'Invalid request method'}), 400
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
@auth.route('/', methods=['GET','POST'])
def login():
    if request.method == 'POST':
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
    return jsonify({'error': 'Invalid request method'}), 400

@auth.route('/signup', methods=['GET' , 'POST'])
def signup():
    if request.method == 'POST':
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
    return jsonify({'error': 'Invalid request method'}), 400
@auth.route('/profile/<int:user_id>')
def profile(user_id):
    conn = sqlite3.connect('PlanosUser.db')
    query  = f"SELECT Sessao.Dia, Plano.Nome, Plano.Autor, Plano.Descricao \
                FROM Sessao INNER JOIN Plano ON Sessao.RefID_plano = Plano.ID \
                    WHERE Sessao.RefID_utilizador = '{user_id}'"
    sessao = conn.execute(query)
    dias = []
    planos = []
    responses = []
    for row in sessao:
        if row[0] not in dias:
            dias.append(row[0])
        #plan_query = f"SELECT Nome, Autor, Descricao FROM Plano WHERE ID = '{row[1]}'"
        #plan = conn.execute(plan_query)
        #plan = plan.fetchone()
        response = {'dia':row[0],'nome':row[1], 'Autor':row[2], 'descricao': row[3]}
        responses.append(response)

        plan = (row[1], row[2],row[3])
        planos.append(plan)
        
    conn.close()
    return jsonify(responses),200
    #return render_template('home.html',dias=dias,planos=planos)


