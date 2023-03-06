function validateDuration() {
    var durationInput = document.getElementById('duracao');
    var duration = parseInt(durationInput.value);
    if (isNaN(duration) || duration <= 0) {
      alert('Please enter a valid integer value for duration.');
      return false;
    }
    return true;
  }



