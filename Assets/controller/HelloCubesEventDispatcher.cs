using System.Collections;
using System.Collections.Generic;
using MQTTnet;
using Newtonsoft.Json;
using PuzzleCubes.Communication;
using PuzzleCubes.Controller;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class HelloCubesEventDispatcher : EventDispatcher
{
   public HelloCubesEvent helloCubesEvent;

   public const string helloCubesRequestTopic =  "puzzleCubes/app/helloCubes";                          // from server 
   public string helloCubesResponseTopic(string cubeId) =>  $"puzzleCubes/{cubeId}/app/helloCubes";                 // to server
   public string rotationTopic = "puzzleCubes/DESKTOP-7I21F8H/app/state";

   public GameObject objectToRotate;

    

    protected override void Initialize()
    {
        base.Initialize();
        objectToRotate = GameObject.Find("zeiger");

      


        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(helloCubesRequestTopic).Build() ,HandleHelloCubes);
        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(rotationTopic).Build() ,HandleRotationMessage);
    }

    // SAMPLE HANDLER FOR MQTT MESSAGE
    public void HandleHelloCubes(MqttApplicationMessage msg, IList<string> wildcardItem){
         var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
        var result = JsonConvert.DeserializeObject<HelloCubes>(data);
        helloCubesEvent.Invoke( result);
    }

    private void HandleRotationMessage(MqttApplicationMessage message, object obj)
    {
        try
        {
            string payload = System.Text.Encoding.UTF8.GetString(message.Payload);
            JObject json = JObject.Parse(payload);
            float rotationValue = json["orientation"].Value<float>();
            
            rotationValue = Mathf.Clamp(rotationValue, 0, 360);
            objectToRotate.transform.rotation = Quaternion.Euler(0, rotationValue, 0);
            Debug.Log("Rotated object to " + rotationValue + " degrees.");
        }
        catch 
        {
            Debug.LogError("Failed to process rotation message: ");
        }
    }


    public void DispatchHelloCubes(HelloCubes hc)
    {
        Debug.Log("DispatchHelloCubes");       
        var json = JsonConvert.SerializeObject(hc, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Objects
        });
        // this.SendZmq(json, true);  
        var msg = new MqttApplicationMessage();
        msg.Topic = helloCubesResponseTopic(AppController.Instance.state.CubeId);
        msg.Payload = System.Text.Encoding.UTF8.GetBytes(json);
        msg.MessageExpiryInterval = 3600;
        
        
        this.mqttCommunication.Send(msg); 
        
        
    }


}
