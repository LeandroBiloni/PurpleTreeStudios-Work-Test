using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySO", menuName = "Create Difficulty Setting")]
public class DifficultySO : ScriptableObject
{
    [Header("General")]
    public string difficultyName;
    public Color buttonColor;

    [Header("World")]
    [Min(0)]
    public float gravity;

    [Header("Gameplay")]
    [Min(0)]
    public float gameTime;
    public int rocksNeededForCoin;
    [Min(0)]
    public float coinsDuration;

    [Header("Hero")]
    public float heroMaxMoveSpeed;
    public float heroAcceleration;
    public float heroBreakStrength;

    [Space]
    [Min(0)]
    public float boxVerticalMultiplier;
    [Min(0)]
    public float boxHorizontalMultiplier;

    [Header("Launcher")]
    public float launcherThrowAngleMin;
    public float launcherThrowAngleMax;
    public float launcherThrowSpeedMin;
    public float launcherThrowSpeedMax; 
    [Min(0)]
    public float launcherThrowSpacingMin;
    [Min(0)]
    public float launcherThrowSpacingMax;
}
