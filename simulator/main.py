
import cubes_client
import cubes_input

client = cubes_client.connect_mqtt()



cubes_client.publishAppState(client, 32.3)

cube1 = cubes_input.Cube(left='a', right='d')
cubes_input.listen_for_input(cube1, lambda x: cubes_client.publishAppState(client, x))
