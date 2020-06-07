using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Player player;
    int enemies_killed;

    public int EnemiesKilled
    {
        get { return enemies_killed; }
        set { enemies_killed = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        enemies_killed = 0;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
