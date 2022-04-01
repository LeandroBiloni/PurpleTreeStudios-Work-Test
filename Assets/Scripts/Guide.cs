using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public void CalculatePosition(Vector3 speed, float angle, float gravity, Vector3 initialPos)
    {
        var boxPos = GameManager.Instance.GetHeroBoxTransform().position;

        var time = Utilities.TimeForFinalPositionY(speed.y, angle, gravity, initialPos.y, boxPos.y);

        Debug.Log("time: " + time);

        var newPos = Vector3.zero;

        Debug.Log("init: " + initialPos.x);
        //Xf = Xi + (Vi * Cos(ang) * t)
        newPos.x = initialPos.x + (speed.x * Mathf.Cos(Mathf.Deg2Rad * angle)* time);

        newPos.y = 0;

        transform.position = newPos;

        Debug.Log("pos: " + transform.position);
    }

    public void DestroyGuide()
    {
        Destroy(gameObject);
    }
}
