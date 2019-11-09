using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{


    public AIBehaviour CurrentBehaviour;

 

    void Start()
    {
        
        SetBehaviour(CurrentBehaviour);
    }

    public void SetBehaviour(AIBehaviour beh)
    {
        CurrentBehaviour = beh;
        beh.StartBehaviour(this.gameObject);
    }

    private void Update()
    {
        CurrentBehaviour.Execute(this.gameObject);
    }

}
