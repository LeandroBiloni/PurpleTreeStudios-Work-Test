using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    /// <summary>
    /// Calculates the time for the given final Y position.
    /// </summary>
    /// <param name="speedY"></param>
    /// <param name="angle"></param>
    /// <param name="gravity"></param>
    /// <param name="initialY"></param>
    /// <param name="finalY"></param>
    /// <returns></returns>
    public static float TimeForFinalPositionY(float speedY, float angle, float gravity, float initialY, float finalY)
    {
        float a = -gravity / 2;

        float b = speedY * Mathf.Sin(Mathf.Deg2Rad * angle);

        float c = initialY - finalY;

        float det = Mathf.Pow(b, 2) - 4 * a * c;

        if (det <= 0) return 0;

        float time = (-b - Mathf.Sqrt(det)) / (2 * a);

        return time;
    }

    /// <summary>
    /// Calculates the X position at the given time.
    /// </summary>
    /// <param name="initialX">Initial X axis position.</param>
    /// <param name="speedX">Initial X axis speed.</param>
    /// <param name="angle"></param>
    /// <param name="time">The time of the sought position.</param>
    /// <returns>The X position.</returns>
    public static float CalculatePositionX(float initialX, float speedX, float angle, float time)
    {
        //Xf = Xi + (Vi * Cos(ang) * t)
        return initialX + speedX * Mathf.Cos(Mathf.Deg2Rad * angle) * time;
    }

    /// <summary>
    /// Calculates the Y position at the given time.
    /// </summary>
    /// <param name="initialYPos">Initial Y axis position.</param>
    /// <param name="speedY">Initial Y axis speed.</param>
    /// <param name="angle"></param>
    /// <param name="gravity"></param>
    /// <param name="time">The time of the sought position.</param>
    /// <returns>The Y position.</returns>
    public static float CalculatePositionY(float initialYPos, float speedY, float angle, float gravity, float time)
    {
        return initialYPos + speedY * Mathf.Sin(Mathf.Deg2Rad * angle) * time + (-gravity / 2) * Mathf.Pow(time, 2);
    }

    /// <summary>
    /// Calculates the speed needed in X axis to reach final X pos at given time.
    /// </summary>
    /// <param name="finalXPos"></param>
    /// <param name="initialXPos"></param>
    /// <param name="angle"></param>
    /// <param name="time"></param>
    /// <returns>The needed X speed.</returns>
    public static float CalculateSpeedX(float finalXPos, float initialXPos, float angle, float time)
    {
        // Vxf = (Xf - Xi) / (Cos(ang) * t)
        return (finalXPos - initialXPos) / (Mathf.Cos(Mathf.Deg2Rad * angle) * time);
    }

    public static float CalculateSpeedY(float finalYPos, float initialYPos, float gravity, float angle, float time)
    {
        // Vyf = (Yf - Yi + 1/2 g * t^2) / (Sin(ang) * t)
        return (finalYPos - initialYPos + (gravity / 2) * Mathf.Pow(time, 2)) / (Mathf.Sin(Mathf.Deg2Rad * angle) * time);
    }

    
}
