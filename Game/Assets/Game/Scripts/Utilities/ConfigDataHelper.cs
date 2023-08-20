
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public static class ConfigDataHelper 
{
    private static GameConfig _gameConfig = null;
    public static GameConfig GameConfig
    {
        get
        {
            if (_gameConfig == null)
            {
                TextAsset textAsset = Resources.Load<TextAsset>("Config/GameConfig");
                _gameConfig = JsonConvert.DeserializeObject<GameConfig>(textAsset.text);
            }    
               
            return _gameConfig;
        }
    }



    public static Dictionary<ResourceType, float> GetDefaultResource()
    {
        return null;
    }
}
