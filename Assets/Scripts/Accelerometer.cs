using UnityEngine;

public static class Accelerometer
{
    private static float _yaw;
    public static float Yaw {
        get { return GetYaw(); }
    }

    private static float _pitch;
    public static float Pitch {
        get { return GetPitch(); }
    }

    // Yaw is the value of the phone tilting from side to side
    private static float GetYaw()
    {
        return Input.acceleration.x;
    }

    // Pitch is the value of the phone tilting forward and backwards
    private static float GetPitch()
    {
        return Input.acceleration.z;
    }
}
