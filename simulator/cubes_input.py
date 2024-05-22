
from pynput.keyboard import Key, KeyCode,Listener

class Cube:
    def __init__(self, left, right):
        self.orientation = 0
        self.left = left
        self.right = right

    def process_input(self, key: Key | KeyCode, onOrientationChange):
        if type(key) != KeyCode:
            return
        
        orientationChanged = False
        
        if key.char == self.left:
            self.orientation += 1
            orientationChanged = True
        elif key.char == self.right:
            self.orientation -= 1
            orientationChanged = True

        if orientationChanged:
            if self.orientation <= -180:
                self.orientation = 360 + self.orientation

            if self.orientation >= 180:
                self.orientation = -360 + self.orientation
            
            print("Orientation: ", self.orientation)
            #onOrientationChange(self.orientation)



def listen_for_input(cubeInput, onOrientationChange):
    with Listener(on_press = lambda key: cubeInput.process_input(key, onOrientationChange) ) as listener:   
        listener.join()