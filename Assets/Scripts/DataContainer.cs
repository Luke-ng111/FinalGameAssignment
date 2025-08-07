using System.Collections.Generic;
using UnityEngine;

public class DataContainer
{
    public static string Word; //sets the name of the NPC
    public static AudioClip AudioClip;

    //tracks which NPCs have been interacted with
    public static HashSet<string> interactedNPCs = new HashSet<string>();
}

