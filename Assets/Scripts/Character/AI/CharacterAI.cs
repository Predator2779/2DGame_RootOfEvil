using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAI : MonoBehaviour
{
    protected Character _character;

    protected virtual void Awake() => _character = GetComponent<Character>();

    protected virtual void Update()
    {

    }

    protected virtual void Moving()
    {

    }
}