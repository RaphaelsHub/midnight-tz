using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statistic", menuName = "playersStatistic")]
public class StorDataAboutKills : ScriptableObject
{
    // Properties with public get and public set
    public int Minutes { get; set; }
    public int Kills { get; set; }
    public int HeadShots { get; set; }
    public int Waves { get; set; }
}
