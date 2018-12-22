using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hud : MonoBehaviour
{
    //private enum Color { red, green, yellow, blue};

    [SerializeField]
    private TextMeshProUGUI selectColor, cool, left;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int temp = Player.instance.selectTrap;
        string text;
        switch (temp)
        {
            case 0:
                text = "red";
                break;
            case 1:
                text = "green";
                break;
            case 2:
                text = "yellow";
                break;
            case 3:
                text = "blue";
                break;
            default:
                text = "";
                break;
        }
        selectColor.text = text;

        cool.text = (Player.instance.cooling) ? "cooling" : "ready";
        left.text = GameManager.instance.left.ToString();
	}
}
