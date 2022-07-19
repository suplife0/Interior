using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureTest : MonoBehaviour
{
    public Dictionary<string, object> dict;
    public GameObject sampleObj;
    private void Awake()
    {
        dict = new Dictionary<string, object>();
    }
    
    private void Start()
    {
        dict.Add("ID", 1234);
        dict.Add("Name", "Hello");
        dict.Add("GObj", sampleObj);
        
        PrintDictionary();
    }

    public void PrintDictionary()
    {
        foreach (string key in dict.Keys)
        {
            Debug.Log(dict[key]);
        }
    }
}
