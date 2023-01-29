from flask import Blueprint, render_template

auth = Blueprint('auth', __name__)

@auth.route('/')
def login():
    return render_template('login.html')

@auth.route('/signup')
def signup():
    return render_template('register.html')

@auth.route('/profile')
def profile():
    return render_template('home.html')


