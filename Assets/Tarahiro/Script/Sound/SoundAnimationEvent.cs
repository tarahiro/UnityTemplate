using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UnityEngine;

public class SoundAnimationEvent:MonoBehaviour
{
    public void PlaySE(string seLabel)
    {
        SoundManagerSe.PlaySE(seLabel);
    }
}
