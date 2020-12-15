public class TextTipManager
{

public static int GetSwingStrickResultType(float angle)
    {
        float newAngle = Mathf.RoundToInt(angle * 10) / 10f;
        if (newAngle >= 90)
        {
            newAngle = newAngle - 360;
        }
        float absAngle = Mathf.Abs(newAngle);

        if (absAngle <= PhysicsUtil.perfect_strick_angle_limit)
        {
            return ComState.strick_type_perfect;
        }
        else if (absAngle <= PhysicsUtil.great_strick_angle_limit)
        {
            return ComState.strick_type_great;
        }
        else if (absAngle <= PhysicsUtil.good_strick_angle_limit)
        {
            return ComState.strick_type_good;
        }
        else if (absAngle <= PhysicsUtil.normal_strick_angle_limit)
        {
            return ComState.strick_type_normal;
        }
        else if (newAngle > 0)
        {
            return ComState.strick_type_hook;
        }
        else
        {
            return ComState.strick_type_slice;
        }
    }

    public static int GetPushStrickResultType(float fixedAngle, float rotationAngle)
    {
        rotationAngle = Mathf.RoundToInt(rotationAngle * 10) / 10f;

        if (fixedAngle >= 90)
        {
            fixedAngle = fixedAngle - 360;
        }

        if (rotationAngle >= 90)
        {
            rotationAngle = rotationAngle - 360;
        }

        float absAngle = Mathf.Abs(fixedAngle - rotationAngle);

        if (absAngle <= PhysicsUtil.perfect_strick_angle_limit)
        {
            return ComState.strick_type_perfect;
        }
        else if (absAngle <= PhysicsUtil.great_strick_angle_limit)
        {
            return ComState.strick_type_great;
        }
        else if (absAngle <= PhysicsUtil.good_strick_angle_limit)
        {
            return ComState.strick_type_good;
        }
        else
        {
            return ComState.strick_type_normal;
        }
    }
}