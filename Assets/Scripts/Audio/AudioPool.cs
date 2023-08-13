using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    public static AudioPool inst;
    private void Awake()
    {
        if (inst != null)
        {
            //If more than one instance found, destroy the latest
            Destroy(gameObject);
        }

        inst = this;
    }
    
    public enum TerainValue{Dirty,Grass,Gravel,Leaves,Metal,Mud,Rock,Sand,Snow,Tile,Water,Wood}

    public TerainValue terainValue;
    public EventReference footSteps;
    public EventReference jump;
    


}
