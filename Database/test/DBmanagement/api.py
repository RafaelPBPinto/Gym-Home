import sqlite3
from flask import Blueprint, render_template, request, flash, redirect, send_file, url_for, jsonify
from werkzeug.security import generate_password_hash, check_password_hash
import base64
import os

api = Blueprint('api', __name__)

@api.route('/addExercise2', methods=['POST'])
def add_exercise():
    if request.method == 'POST':
        exercise_data = request.get_json()

        nome = exercise_data.get('nome')
        tipo = exercise_data.get('tipo')
        duracao = exercise_data.get('duracao')
        descricao = exercise_data.get('descricao')

        conn = sqlite3.connect('PlanosUser.db')
        query = "INSERT INTO Exercicio (Nome, Tipo, Duracao, Descricao) VALUES (?, ?, ?, ?)"
        conn.execute(query, (nome, tipo, duracao, descricao))
        conn.commit()
        conn.close()

        return jsonify({'message': 'Exercise added successfully'})
    else:
        return jsonify({'error': 'Invalid request method'}), 400

@api.route('/addExercise', methods=['GET', 'POST'])
def addExercise():
    alert_message = ''
    if request.method == 'POST':
        nome = request.form.get('nome')
        tipo = request.form.get('tipo')
        duracao = request.form.get('duracao')
        descricao = request.form.get('descricao')
                

        if 'imagem' in request.files:
            # get the image file from the form and read it as binary
            imagem = request.files.get('imagem').read()
            if not imagem:
                return jsonify({'error': 'No image found'}), 400

        if 'video' in request.files:
            video_file = request.files.get('video')
            if not video_file:
                return jsonify({'error': 'No video found'}), 400
            #video_path = os.path.join('video', video_file.filename)
            video_file.save(os.path.join('DBmanagement', 'video', video_file.filename))

        conn = sqlite3.connect('PlanosUser.db')
        query = f"INSERT INTO Exercicio (Nome, Tipo, Duracao, Descricao) VALUES ('{nome}', '{tipo}', '{duracao}', '{descricao}')"
        conn.execute(query)

        refID_exercicio = conn.execute("SELECT MAX(ID) FROM Exercicio").fetchone()[0]
        if imagem:
            query = f"INSERT INTO Imagem (Nome, ImagemBinary, RefID_exercicio) VALUES (?, ?, ?)"
            conn.execute(query, (nome, imagem, refID_exercicio))

        if video_file:
            query = f"INSERT INTO Video (Nome, VideoName, RefID_exercicio) VALUES (?, ?, ?)"
            conn.execute(query, (nome, video_file.filename, refID_exercicio))

        conn.commit()
        conn.close()
        alert_message = 'Exercício adicionado com sucesso!'
        #return jsonify({'message': 'Exercício adicionado com sucesso!'})
        #return redirect(url_for('api.getExercises'))
    return render_template('addExercise.html', alert_message=alert_message)

@api.route('/removeExercise', methods=['GET','POST'])
def removeExercise():
    alert_message = ''
    if request.method == 'POST':
        nome = request.form.get('nome')

        conn = sqlite3.connect('PlanosUser.db')
        query = "SELECT * FROM Exercicio WHERE Nome = ?"
        result = conn.execute(query, (nome,)).fetchone()

        if result is not None:
            RefID_exercicio = result[0]
            print(RefID_exercicio)
            query = "DELETE FROM Exercicio WHERE Nome = ?"
            conn.execute(query, (nome,))

            query = "DELETE FROM Imagem WHERE RefID_exercicio = ?"
            conn.execute(query, (RefID_exercicio,))

            query = "SELECT VideoName FROM Video WHERE RefID_exercicio = ?"
            video_name = conn.execute(query, (RefID_exercicio,)).fetchone()[0]
            os.remove(os.path.join('DBmanagement', 'video', video_name))

            query = "DELETE FROM Video WHERE RefID_exercicio = ?"
            conn.execute(query, (RefID_exercicio,))
            
            conn.commit()
            conn.close()
            alert_message = 'Exercício removido com sucesso!'
            #return jsonify({'message': 'Exercise removed successfully'})
        else:
            alert_message = 'Exercício não encontrado...'
            conn.close()
            #return jsonify({'message': 'Exercise not found'})
    return render_template('removeExercise.html', alert_message=alert_message)


@api.route('/getExercises', methods=['GET', 'POST'])
def getExercises():
    if request.method == 'GET':
        conn = sqlite3.connect('PlanosUser.db')
        query = f"SELECT Exercicio.Nome, Exercicio.Tipo, Exercicio.Duracao, Exercicio.Descricao, Imagem.ImagemBinary, Video.ID FROM Exercicio INNER JOIN Imagem ON Exercicio.ID = Imagem.RefID_exercicio INNER JOIN Video ON Exercicio.ID = Video.RefID_exercicio"
        result = conn.execute(query)
        # result = result.fetchall()
        
        responses = []
        
        row = result.fetchone()  
        while row is not None:
            if row[4] is not None:
                img_data = base64.b64encode(row[4]).decode('utf-8')            
            response = {'nome': row[0], 'tipo': row[1], 'duracao': row[2], 'descricao': row[3], 'videoID': row[5], 'imagem': img_data}
            responses.append(response)
            row = result.fetchone()

        conn.close()
        return jsonify(responses), 200
    return jsonify({'error': 'Invalid request method'}), 400

# Buscar todos os excs com ID igual ao do plano (da sessao)

@api.route('/getExercise/id=<id>', methods=['GET', 'POST'])
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

@api.route('/login', methods=['GET','POST'])
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

@api.route('/signup', methods=['GET' , 'POST'])
def signup():
    if request.method == 'POST':
        #data = request.json()
        data = request.get_json()
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

# Aqui temos a ir buscar as sessoes de cada utilizador

@api.route('/profile/user_Id=<user_id>')
def profile(user_id):
    conn = sqlite3.connect('PlanosUser.db')
    query  = f"SELECT Sessao.Dia, Plano.Nome, Plano.Autor, Plano.Descricao \
                FROM Sessao INNER JOIN Plano ON Sessao.RefID_plano = Plano.ID \
                    WHERE Sessao.RefID_utilizador = '{user_id}'"
    sessao = conn.execute(query)
    #dias = []
    #planos = []
    responses = []
    for row in sessao:
        #if row[0] not in dias:
        #    dias.append(row[0])
        response = {'dia':row[0],'nome':row[1], 'Autor':row[2], 'descricao': row[3]}
        responses.append(response)

        #plan = (row[1], row[2],row[3])
        #planos.append(plan)
        
    conn.close()
    return jsonify(responses),200
    #return render_template('home.html',dias=dias,planos=planos)

@api.route('/addPlano', methods =['GET' , 'POST'])
def addPlano():
    alert_message = ''
    if request.method == 'POST':
        nome = request.form.get('nome')
        autor = request.form.get('autor')
        descricao = request.form.get('descricao')

        conn = sqlite3.connect('PlanosUser.db')
        query = f"INSERT INTO Plano (Nome, Autor, Descricao) VALUES ('{nome}', '{autor}', '{descricao}')"
        conn.execute(query)
        conn.commit()
        conn.close()
        alert_message = 'Plano adicionado com sucesso!'
    return render_template('addPlano.html', alert_message=alert_message)

@api.route('/addSessao', methods =['GET' , 'POST'])
def addSessao():
    alert_message = ''
    if request.method == 'POST':
        conn = sqlite3.connect('PlanosUser.db')
        dia = request.form.get('dia')
        ref_user = request.form.get('ref_user')
        ref_plano = conn.execute("SELECT MAX(ID) FROM Plano").fetchone()[0]
    
        query = f"INSERT INTO Sessao (Dia,  RefID_utilizador,  RefID_plano) VALUES ('{dia}', '{ref_user}', '{ref_plano}')"
        conn.execute(query)
        conn.commit()
        conn.close()
        alert_message = 'Sessão adicionado com sucesso!'
    return render_template('addSessao.html', alert_message=alert_message)

@api.route('/profileComplete/user_id=<user_id>', methods = ['GET', 'POST'])
def getMySesson(user_id):
    conn = sqlite3.connect('PlanosUser.db')
    query  = f"\
    SELECT Sessao.Dia, Plano.Nome, Plano.Autor, Plano.Descricao,\
        ExercicioPlano.Series, ExercicioPlano.Repeticoes, ExercicioPlano.Ordem,\
        Exercicio.Nome, Exercicio.Tipo, Exercicio.Descricao, Imagem.Nome, Imagem.ImagemBinary,\
        Video.ID\
    FROM Sessao INNER JOIN Plano ON Sessao.RefID_plano = Plano.ID\
        INNER JOIN ExercicioPlano ON ExercicioPlano.RefID_plano = Plano.ID \
        INNER JOIN Exercicio ON Exercicio.ID =  ExercicioPlano.RefID_exercicio\
        INNER JOIN Imagem ON Imagem.RefID_exercicio = Exercicio.ID\
        INNER JOIN Video ON Video.RefID_exercicio = Exercicio.ID\
        WHERE Sessao.RefID_utilizador = '{user_id}'\
    "

    responses = []
    sessao = conn.execute(query)
    plansName = []
    # get all exercises for each plan in the session
    for row in sessao:
        if row[1] not in plansName:
            plansName.append(row[1])
            if row[11] is not None:
                img_data = base64.b64encode(row[11]).decode('utf-8')
            exerData = { 'series': row[4], 'repeticoes': row[5], 'ordem': row[6], 'nome': row[7], 'tipo': row[8], 'descricao': row[9], 'videoID':row[12], 'imagem': img_data}
            response = {'dia':row[0],'nome':row[1], 'Autor':row[2], 'descricao': row[3], 'exercicios': [exerData]}
            responses.append(response)
        else:
            if row[11] is not None:
                img_data = base64.b64encode(row[11]).decode('utf-8')
            #video_data = base64.b64encode(row[13]).decode('utf-8')
            exerData = { 'series': row[4], 'repeticoes': row[5], 'ordem': row[6], 'nome': row[7], 'tipo': row[8], 'descricao': row[9], 'videoID':row[12],'imagem': img_data}
            for plan in responses:
                if plan['nome'] == row[1]:
                    plan['exercicios'].append(exerData)
                    break   
    conn.close()
    return jsonify(responses),200

@api.route('/getVideo/video_id=<video_id>', methods = ['GET', 'POST'])
def getVideo(video_id):
    conn = sqlite3.connect('PlanosUser.db')
    query  = f"SELECT Video.VideoName FROM Video WHERE Video.ID = '{video_id}'"
    sessao = conn.execute(query)
    # with the name of the video get the binary data on the folder video and return it with json
    for row in sessao:
        video = row[0]
        break
    conn.close()
    return send_file(f'video/{video}', mimetype='video/mp4')
    



@api.route('/getPlanos', methods = ['GET', 'POST'])
def getPlanos():
    conn = sqlite3.connect('PlanosUser.db')
    query  = f"\
    SELECT Plano.Nome, Plano.Autor, Plano.Descricao,\
        ExercicioPlano.Series, ExercicioPlano.Repeticoes, ExercicioPlano.Ordem,\
        Exercicio.Nome, Exercicio.Tipo, Exercicio.Descricao, Imagem.ImagemBinary\
    FROM Plano\
        INNER JOIN ExercicioPlano ON ExercicioPlano.RefID_plano = Plano.ID \
        INNER JOIN Exercicio ON Exercicio.ID =  ExercicioPlano.RefID_exercicio\
        INNER JOIN Imagem ON Imagem.RefID_exercicio = Exercicio.ID\
    "
    planos = conn.execute(query)
    responses = []
    plansNames = []
    for row in planos:
        if(row[0] not in plansNames):
            plansNames.append(row[0])
            exerData = []
            img_data = base64.b64encode(row[9]).decode('utf-8')
            exerData.append({'series': row[3], 'repeticoes':row[4],'ordem':row[5],\
                        'nome':row[6], 'tipo':row[7], 'descricao':row[8]#,\
                        #'imagem':img_data
                       })
            response = {'nome':row[0], 'Autor':row[1], 'descricao': row[2], 'exercicio':exerData}
            responses.append(response)
        else:
            img_data = base64.b64encode(row[9]).decode('utf-8')
            exerData = {'series': row[3], 'repeticoes':row[4],'ordem':row[5],\
                        'nome':row[6], 'tipo':row[7], 'descricao':row[8]#,\
                        #'imagem':img_data
                       }
            response['exercicio'].append(exerData)

    conn.close()
    return jsonify(responses),200


@api.route('/', methods = ['GET', 'POST'])
def home():
    return render_template("index.html")