from flask import Flask
from flask_sqlalchemy import SQLAlchemy

def create_app():
    app = Flask(__name__)
    app.config['SECRET_KEY'] = 'QWErty310'
    #app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///PlanosUser.db'

    #db = SQLAlchemy(app)

    from .auth import auth

    app.register_blueprint(auth, url_prefix='/')

    return app