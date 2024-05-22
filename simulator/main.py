
import cubes_client
import cubes_input

client = cubes_client.connect_mqtt()




cube1 = cubes_input.Cube(id="steuer",left='a', right='d')
cube2 = cubes_input.Cube(id="test2",left='f', right='h')

cubes_input.listen_for_input([cube1, cube2], lambda cube: cubes_client.publishAppState(client, cube))
