using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelScript : MonoBehaviour {

    [SerializeField] int brekableBlocks;

    Scene_Loader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<Scene_Loader>();
    }
    public void CountBlocks()
    {
        brekableBlocks++;
    }

    public void BlockDestroyed()
    {
        brekableBlocks--;
        if (brekableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }
}
