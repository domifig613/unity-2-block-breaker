using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    //configuration parametrs
    [SerializeField] float minX = 1f;
    [SerializeField] float screenWidthInUnits = 16f;
 
    //cached references
    GameSession theGameSession;
    Ball theball;

    // Use this for initialization
    void Start () {
        theGameSession = FindObjectOfType<GameSession>();
        theball = FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
            Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPos(), minX, screenWidthInUnits - minX);

            transform.position = paddlePosition;
        
	}

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
