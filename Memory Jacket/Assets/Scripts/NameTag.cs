using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    Vector2 resolution, resolutionInWorldUnits = new Vector2(17.8f, 10);

    // Start is called before the first frame update
    void Start()
    {
        resolution = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        transform.position = Input.mousePosition / resolution * resolutionInWorldUnits;
    }
}
