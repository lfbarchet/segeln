
from pynput.keyboard import Key, KeyCode,Listener

class Cube:
    def __init__(self, id, left, right):
        self.orientation = 0
        self.id = id
        self.left = left
        self.right = right

    def process_input(self, key: Key | KeyCode, onChange):
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
            onChange(cube=self)



def listen_for_input(cubes: list, onChange):
    def on_key_press(key):
        for cube in cubes:
            cube.process_input(key, onChange)

    with Listener(on_press = on_key_press ) as listener:   
        listener.join()