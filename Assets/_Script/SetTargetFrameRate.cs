﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour {

    void Awake  ()
    {
        Application.targetFrameRate = 30;
    }
}
