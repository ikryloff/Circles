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
           // DontDestroyOnLoad (this);
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

    [SerializeField]
    private Texture2D [] activeIcons = new Texture2D [77];
    [SerializeField]
    private Texture2D [] unactiveIcons = new Texture2D [77];
    [SerializeField]
    private Texture2D [] schemas = new Texture2D [77];
    [SerializeField]
    private Texture2D testIcon;

    // Methods

    public GameObject GetAssetByString( string name )
    {
        return assets [name];
    }

    public Texture2D GetActiveIconByID( int id )
    {
        if ( activeIcons [id] )
            return activeIcons [id];
        else
            return testIcon;
    }

    public Texture2D GetUnActiveIconByID( int id )
    {
        if ( unactiveIcons [id] )
            return unactiveIcons [id];
        else
            return testIcon;
    }

    public Texture2D GetSchemaByID( int id )
    {
        if ( schemas [id] )
            return schemas [id];
        else
            return testIcon;
    }

    public Texture2D GetEmptyIcon()
    {
        return testIcon;
    }

}


