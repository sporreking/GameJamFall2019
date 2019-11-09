using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [TextArea]
    public string Message;

    private Canvas c;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponentInChildren<Canvas>();
        GetComponentInChildren<Text>().text = Message;
    }

    private void Update()
    {
        if (c.enabled && Input.GetButtonDown("Fire1"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Freeze(false);
            c.enabled = false;
        }
    }

    public void Open()
    {
        c.enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Freeze(true);
    }
}
