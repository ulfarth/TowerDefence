using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public bool gameOver = false;
    public Text healthLabel;
    public Text goldLabel;
    public Text waveLabel;

    public GameObject[] healthIndicator;
    public GameObject[] nextWaveLabels;

    private int _gold;
    private int _wave;
    private int _health;


    public int Wave
    {
        get
        {
            return _wave;
        }
        set
        {
            _wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
           // waveLabel.text = "WAVE " + (_wave + 1);
        }
    }

    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
           // goldLabel.GetComponent<Text>().text = "GOLD: " + _gold;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = 1000;
        Wave = 0;
        // Health = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
