using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class AsyncSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Task.Run(() => FirstTestAsync());
        Debug.Log("Next Run");
    }

    async Task FirstTestAsync()
    {
        await Task.Run(() =>
        {
            Debug.Log("First Async");
        });
        
        await Task.Delay(3000);
        
        await SecondTestAsync();
    }

    async Task SecondTestAsync()
    {
        await Task.Delay(1);

        Debug.Log("Second Async");
    }
}
