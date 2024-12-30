using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Msg;
    public AudioSource Click;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MsgSound()
    { 
        Msg.Play();
    }
    public void ClickSound()
    {
        Click.Play();
    }
}
