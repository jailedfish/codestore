
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JetBrains.Annotations;
using UnityEngine;

public class Pair<TFirst, TSecond> {
    public TFirst first;
    public TSecond second;
    public Pair(TFirst first, TSecond second) {
        this.first = first;
        this.second = second;
    }
}

public class Key {
    public GameObject KeyObject;
    public string Info;
    public string[] CanOpen;
    public int id;


    public bool PickedUp = false;


    public Key(GameObject key, string info, string[] canOpen) {
        this.PickedUp = false;
        this.KeyObject = key;
        this.Info = info;
        this.CanOpen = canOpen;
    }
}