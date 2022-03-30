using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Action OnRockReachedGoal = () => { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Rock"))
        {
            OnRockReachedGoal?.Invoke();
        }
    }
}
