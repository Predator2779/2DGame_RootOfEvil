using UnityEngine;

public class RotationCommand : Command
{
    public RotationCommand(Transform RotatableObj)
    {
        _rotatableObj = RotatableObj;
    }

    private Transform _rotatableObj;

    public override void Execute()
    {

    }

    public override void ExecuteByValue(float angle)
    {
        _rotatableObj.localRotation = Quaternion.Euler(0f, angle, 0f);
    }
}