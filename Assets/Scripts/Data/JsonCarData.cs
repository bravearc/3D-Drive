using UnityEngine;

public class JsonCarData
{
    public readonly string CarName;
    public readonly int CarSpeed;
    public readonly string EngineSound;
    public readonly string DriveSound;

    public JsonCarData(string name, int sp, string en, string dr)
    {
        this.CarName = name;
        this.CarSpeed = sp;
        this.EngineSound = en;
        this.DriveSound = dr;
    }
}

public class JsonMapData
{
    public readonly string MapName;
    public readonly Vector3 Position;
    public readonly string Sound;

    public JsonMapData(string name, Vector3 position, string engine)
    {
        this.MapName = name;
        this.Position = position;
        this.Sound = engine;
    }
}
