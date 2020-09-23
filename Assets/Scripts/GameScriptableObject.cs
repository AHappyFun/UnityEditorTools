using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameScriptableObject : ScriptableObject
{

    [SerializeField]
    public List<Enemy> enemies;

    [System.Serializable]
    public class Enemy
    {
        public int id;
        public string name;
    }
}
