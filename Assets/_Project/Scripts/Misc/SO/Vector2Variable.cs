using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vector2 Variable", menuName = "Variables/Vector2")]
public class Vector2Variable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public Vector2 Value;

    public void SetValue(Vector2 value) => Value = value;

    public void ApplyChanges(Vector2 amount) => Value += amount;
}
