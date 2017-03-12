using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManagerBehaviour : MonoBehaviour {

    public Text waveRef, goldRef;
    private int wave,gold;
    public bool gameOver = false;


	// Use this for initialization
	void Start () {
        wave = 0;
        gold = 1500;
        setWaveCount();
	}
	
	// Update is called once per frame
	void Update () {
	}
    void setWaveCount()
    {
        waveRef.text = "Wave: "+ wave;
    }
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            goldRef.GetComponent<Text>().text = "Gold: " + gold;
        }
    }
}
