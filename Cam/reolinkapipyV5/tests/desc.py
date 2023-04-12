import subprocess

def get_camera_ip():
    cmd = 'arp -a'
    output = subprocess.check_output(cmd, shell=True).decode('utf-8')

    for line in output.splitlines():
        if 'câmera' in line.lower(): # Substitua "câmera" pelo nome ou fabricante da sua câmera
            ip = line.split()[1][1:-1]
            return ip

    return None

print(get_camera_ip())
