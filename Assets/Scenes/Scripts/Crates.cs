using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
