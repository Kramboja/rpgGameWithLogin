using UnityEngine;
using System.Collections;

/// <summary>
/// MoveUtility is the Utility function class for Move and Rotate Functionality.
/// </summary>
public class MoveUtility
{
    // Moves by frame distance.
    public static float MoveFrame(CharacterController cc, Transform target, float moveSpeed, float turnSpeed)
    {
        Transform self = cc.transform;

        // Get target position to reach.
        Vector3 targetPos = target.position;
        targetPos.y = self.position.y;

        // Get frame position to move each frame.
        Vector3 framePos = Vector3.MoveTowards(self.position, targetPos, moveSpeed * Time.deltaTime);

        // Call Character Controller's Move function to move character.
        cc.Move((framePos - self.position) + Physics.gravity * Time.deltaTime);

        // Rotate by Frame.
        RotateToDir(self, target, turnSpeed);

        // return rest distance to target position.
        return Vector3.Distance(self.position, targetPos);
    }

    // Rotates by frame rotation.
    public static void RotateToDir(Transform self, Transform target, float turnSpeed)
    {
        // if current direction is equal to target direction then don't rotate.
        Vector3 dir = target.position - self.position;
        dir.y = 0f;

        if (dir == Vector3.zero)
            return;

        // get target rotation.
        Quaternion targetRot = Quaternion.LookRotation(dir);

        // get frame rotation.
        Quaternion framePos = Quaternion.RotateTowards(self.rotation, targetRot, turnSpeed * Time.deltaTime);

        // apply frame rotation.
        self.rotation = framePos;
    }

    // Rotate at once to target rotation.
    public static void RotateToDirBurst(Transform self, Transform target)
    {
        // if current direction is equal to target direction then don't rotate.
        Vector3 dir = target.position - self.position;
        dir.y = 0f;

        if (dir == Vector3.zero)
            return;

        // apply target rotation at once.
        self.rotation = Quaternion.LookRotation(dir);
    }
}