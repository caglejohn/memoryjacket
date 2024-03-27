using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class NameTag : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {

        Debug.Log(Input.mousePosition);
    }
}
