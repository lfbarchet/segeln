
import constants

from paho.mqtt import client as mqtt_client
import datetime

def connect_mqtt():
    print("Connecting to MQTT Broker with host: ", constants.broker, " and port: ", constants.port)

    def on_connect(client, userdata, flags, rc):
    # For paho-mqtt 2.0.0, you need to add the properties parameter.
    # def on_connect(client, userdata, flags, rc, properties):
        if rc == 0:
            print("Connected to MQTT Broker!")
        else:
            print("Failed to connect, return code %d\n", rc)
    # Set Connecting Client ID  
    client = mqtt_client.Client(client_id=constants.client_id, callback_api_version=mqtt_client.CallbackAPIVersion.VERSION2)

    # client.username_pw_set(username, password)
    client.on_connect = on_connect
    client.connect(constants.broker, constants.port)
    return client


def publishAppState(client, cube):
    msg = {
        "$type": constants.app_type,
        "orientation": str(cube.orientation),
        "appName": constants.app_name,
        "appVersion": constants.app_version,
        "processId": 6833,
        "cubeId": cube.id,
        "isRunning": "true",
        "volume": 0.0,
        "mqttConnected": "true",
        "timestamp": datetime.datetime.now(tz=datetime.timezone.utc).isoformat()
    }

    print("Publishing message: ", msg)
    client.publish(constants.topic, str(msg))
