from flask import Flask
from flask_sqlalchemy import SQLAlchemy

def create_app():
    app = Flask(__name__)
    app.config['SECRET_KEY'] = 'QWErty310'

    from .api import api

    app.register_blueprint(api, url_prefix='/')

    return app