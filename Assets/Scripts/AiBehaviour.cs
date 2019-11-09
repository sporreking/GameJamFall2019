using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIBehaviour : ScriptableObject
{
    public abstract void Execute(GameObject gameObject);
    public abstract void DrawDebug(GameObject gameObject);
    public abstract void StartBehaviour(GameObject gameObject);
}

