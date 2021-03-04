using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance { get; private set; }

    private Dictionary<string, GameObject> assets;

    private void Awake()
    {
        if ( !instance )
        {
            instance = this;
            DontDestroyOnLoad (this);
        }
        else
            Destroy (this);

        assets = new Dictionary<string, GameObject> ();
        foreach ( var item in prefabs )
        {
            assets.Add (item.name, item);
        }
    }    


   public GameObject [] prefabs;

   
    // Methods

    public GameObject GetAssetByString(string name )
    {
        return assets [name];
    }
}


