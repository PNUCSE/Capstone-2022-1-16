import os

terminal_command = f"python modified_detect.py --weights runs_fix/train/exp/weights/best.pt --conf 0.1 --source image/test.jpg"

os.system(terminal_command)