from logging import raiseExceptions
import socket, threading
from PIL import Image, ImageFile
import io
import time
#ImageFile.LOAD_TRUNCATED_IMAGES = True
class ServerSocket:
    
    def __init__(self, ip, port):
        self.TCP_IP = ip
        self.TCP_PORT = port
        self.socketOpen()
        self.receiveThread = threading.Thread(target=self.receiveImages)
        self.receiveThread.start()

    def socketClose(self):
        self.sock.close()
        print(u'Server socket [ TCP_IP: ' + self.TCP_IP + ', TCP_PORT: ' + str(self.TCP_PORT) + ' ] is close')

    def socketOpen(self):
        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.bind((self.TCP_IP, self.TCP_PORT))
        self.sock.listen(1)
        print(u'Server socket [ TCP_IP: ' + self.TCP_IP + ', TCP_PORT: ' + str(self.TCP_PORT) + ' ] is open')
        self.conn, self.addr = self.sock.accept()
        print(u'Server socket [ TCP_IP: ' + self.TCP_IP + ', TCP_PORT: ' + str(self.TCP_PORT) + ' ] is connected with client')

    def receiveImages(self):
        i=0
        while True:
            try:
                length=self.conn.recv(4)
                int_length=int.from_bytes(length,byteorder='big')
                print(f"length : {int_length}")
                if int_length == 0 :
                    break
                if int_length <10000 or int_length >200000:
                    self.conn.recv(int_length)
                    raise Exception("Error")
                
                byte_image = self.conn.recv(int_length)
                count=len(byte_image)
                while count < int_length:
                    byte_image += self.conn.recv(int_length - count)
                    count = len(byte_image)
                
                print(f'count: {count} length: {int_length}')
                data_io=io.BytesIO(byte_image)
                image=Image.open(data_io)
                deg_image=image.transpose(Image.ROTATE_90)
                flip_image=deg_image.transpose(Image.FLIP_LEFT_RIGHT)
                flip_image.save('yolov7-main/image/test.jpg') 
                
            except Exception as e:
                print(e)
                

def main():
    server = ServerSocket('192.168.231.196', 50000)

if __name__ == "__main__":
    main() 