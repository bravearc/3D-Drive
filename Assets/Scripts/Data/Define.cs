using Spine;
using UnityEngine;

public static class Define
{
    public struct Path
    {
        public const string Car = "Car(Menu)/";
        public const string RaceCar = "RaceCar/";
        public const string Map = "Map/";
        public const string Sound = "Sound/";
        public const string Sprite = "Sprite/";
        public const string UI = "UI/";
    }

    public class CarData
    {
        public static JsonCarData Ambulance = new JsonCarData(
            "Ambulance_no_damage",
            100,
            "Ambulance_Engine",
            "Ambulance_Drive");

        public static JsonCarData SchoolBus = new JsonCarData(
            "School_Bus_no_damage",
            100,
            "School_Bus_Engine",
            "School_Bus_Drive");

        public static JsonCarData Truck = new JsonCarData(
            "Truck",
            300,
            "Truck_Engine",
            "Truck_Drive");

        public static JsonCarData TouristBus = new JsonCarData(
            "Tourist_Bus",
            100,
            "Tourist_Bus_Engine",
            "Tourist_Bus_Drive");        
        public static JsonCarData G7_1200 = new JsonCarData(
            "G7_1200",
            100,
            "G7_1200_Engine",
            "G7_1200_Drive");
        public static JsonCarData Mercedes_Benz = new JsonCarData(
            "Mercedes_Benz",
            100,
            "G7_1200_Engine",
            "G7_1200_Drive");
        public static JsonCarData Hovertank = new JsonCarData(
            "Hovertank",
            100,
            "G7_1200_Engine",
            "G7_1200_Drive");
    }

    public class MapData
    {
        public static JsonMapData Mountain = new JsonMapData(
            "Mountain",
            new Vector3(684.457f, 217.415f, 103.979f),
            "Mountain");

        public static JsonMapData RaceTrack = new JsonMapData(
            "RaceTrack",
            new Vector3(-462.77f, 915.19f, 444.14f),
            "RaceTrack"
            );

        public static JsonMapData Near_Geometry = new JsonMapData(
            "Near_Geometry",
            new Vector3(-567f, 0.42f, -254.7f),
            "Near_Geometry"
            );
    }
}

