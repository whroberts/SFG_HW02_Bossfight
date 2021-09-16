using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

}
