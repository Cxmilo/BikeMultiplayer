using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarRotation : MonoBehaviour {

    void Update()
    {
        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * Time.deltaTime * 60f, Space.World);
    }
}
