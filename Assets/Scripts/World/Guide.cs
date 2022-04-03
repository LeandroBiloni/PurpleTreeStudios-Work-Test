using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public void CalculatePosition(Vector3 speed, float angle, float gravity, Vector3 initialPos)
    {
        var boxPos = GameManager.Instance.GetHeroBoxTransform().position;

        var time = Utilities.TimeForFinalPositionY(speed.y, angle, gravity, initialPos.y, boxPos.y);

        var newPos = Vector3.zero;

        newPos.x = Utilities.CalculatePositionX(initialPos.x, speed.x, angle, time);

        newPos.y = 0;

        transform.position = newPos;
    }

    public void DestroyGuide()
    {
        Destroy(gameObject);
    }
}
