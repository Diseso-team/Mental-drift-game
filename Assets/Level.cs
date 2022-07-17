using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int ID;

    public void OnClick()
    {
        Menu._instance.currentLevelID = ID;
    }
}
