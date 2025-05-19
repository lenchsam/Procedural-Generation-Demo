using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//used https://www.youtube.com/watch?v=4JaHSLA2CKs for this
public class Labeller : MonoBehaviour
{
    public Vector2 Cords = new Vector2();

    void Awake()
    {
        DisplayCords();
        transform.name = Cords.ToString();
    }

    private void DisplayCords()
    {
        Cords.x = transform.position.x;
        Cords.y = transform.position.z;
    }
}
