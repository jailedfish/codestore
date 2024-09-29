using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;


public class Player {
    public Vector3 pos;
    public List<bool> Doors { get; private set; }
    public int health;
    
    

    public Player(Vector3 position, HealthManager health, List<bool> doors) {
        Player player = this;
        player.pos = position;
        
        player.health = health.health;
        
        player.Doors = doors;
    }
    
    public Player(Vector3 position, HealthManager health) {
        Player player = this;
        player.pos = position;
        
        player.health = health.health;
        
        player.Doors = new List<bool>();
    }
    
    public Player () {
        Player player = this;
        player.pos = Vector3.zero;
        player.health = 100;
        
        player.Doors = new List<bool>();
    }
    
    public void SetDoor(int door, bool state) {
        if (door < 0 | door > Doors.Count)
        {
            return; 
        }

        Doors[door] = state;
    }

    public string ToStirng() {
        return $"Position: {pos.ToString()}, \nHP: {health}/100, \nDoors: {Doors}";
    }
}

public class SavesManager {
    public static void SaveGame(Player player) {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Player));
        if (!Directory.Exists("saves"))
        {
            Directory.CreateDirectory("saves");
        }

        if (File.Exists("./saves/save.xml"))
        {
            File.Delete("./saves/save.xml");
        } 
        StreamWriter writer = new StreamWriter("./saves/save.xml");
        xmlSerializer.Serialize(writer.BaseStream, player);
        writer.Flush();
        writer.Close();
    }
    public static Player LoadGame() {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Player));
        return (Player)xmlSerializer.Deserialize(new StreamReader("./saves/save.xml"));
    }
}
