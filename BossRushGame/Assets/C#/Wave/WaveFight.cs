using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFight : WaveBase
{
    public Enemy enemy1, enemy2, enemy3, enemy4;
    public enum Enemy
    {
        None,
        SmallGuy,
        NormalGuy,
        BossMan
    }
}
