import random


broker = 'localhost'
port = 1883
topic = "puzzleCubes/test/app/state"
client_id = f'simulator-{random.randint(0, 1000)}'

# cubes specific
app_type = "HelloCubesAppState, Assembly-CSharp"
app_name = "hello-cubes"
app_version = "0.1"
app_cube_id = client_id