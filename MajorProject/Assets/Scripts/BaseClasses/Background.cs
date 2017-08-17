using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    public float m_chanceToSpawnEnemy;
    public float m_chanceToSpawnEvent;
    public float m_chanceToSpawnNothing;
    public static int m_cellsInScene = 3;
    public GameObject[] m_cells;
    public GameObject[] m_enemiesToSpawn;
    public GameObject[] m_eventsTospawn;

	// Use this for initialization
	void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Finds a random number within the range of 0 and all the chances together
    //depending on how high the number is changes the event that happens
    public void Initialize()
    {
        for (int i = 0; i < m_cells.Length; i++)
        {
            float fullChance = m_chanceToSpawnEnemy + m_chanceToSpawnEvent + m_chanceToSpawnNothing;
            float chance = Random.Range(0, fullChance);
            PrimitiveType prim;
            //if within the chance of spawn an enemy
            if (chance <= m_chanceToSpawnEnemy)
            {
                prim = PrimitiveType.Cube;
            }
            //if within the change of spawn event
            else if (chance <= m_chanceToSpawnEnemy + m_chanceToSpawnEvent)
            {
                prim = PrimitiveType.Sphere;
            }
            else
            {
                prim = PrimitiveType.Capsule;
            }
            GameObject temp = GameObject.CreatePrimitive(prim);
            temp.transform.parent = m_cells[i].transform;
            temp.transform.localPosition = Vector3.zero;
            //Instantiate(, m_cells[i].transform);
        }
    }
}
