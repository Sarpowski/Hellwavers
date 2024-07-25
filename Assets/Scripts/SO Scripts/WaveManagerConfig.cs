using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveManagerConfig",menuName = "ScriptableObjects/WaveManagerConfig", order =2)]
public class WaveManagerConfig : ScriptableObject
{
    public WaveConfig[] waves;
}
