using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData" , menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    // [Min(1)]
    [Range(1 , 20)]
    [Header("移動速度")]
    public float moveSpeed = 3f;
}