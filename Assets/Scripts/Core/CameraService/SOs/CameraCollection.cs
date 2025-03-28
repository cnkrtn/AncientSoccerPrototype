using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Collection")]
public class CameraCollection : ScriptableObject
{
    public List<CameraPrefabEntry> entries;
}