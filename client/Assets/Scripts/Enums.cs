using UnityEngine;

public enum ColorState
{
    BLACK,
    WHITE
}

public enum FieldState
{
    BLACK,
    WHITE,
    EMPTY,
    SELECTABLE
}

public static class EnumsUtils
{
    public static bool IsColor(this FieldState state)
    {
        return state == FieldState.BLACK || state == FieldState.WHITE;
    }

    public static ColorState ToColor(this FieldState state)
    {
        return (ColorState)state;
    }

    public static void ResetSelect(ref this FieldState state)
    {
        if(state == FieldState.SELECTABLE)
        {
            state = FieldState.EMPTY;
        }
    }

    public static FieldState ToFieldState(this ColorState color)
    {
        return (FieldState)color;
    }

    public static ColorState Other(this ColorState color)
    {
        return (ColorState)(((int)color + 1) % 2);
    }

    public static Color ToUnityColor(this ColorState color)
    {
        return color == ColorState.BLACK ? Color.black : Color.white;
    }

    public static ColorState RandomColor() 
    {
        return (ColorState)UnityEngine.Random.Range(0, 2);
    }

    public static string ToAIString(this ColorState color)
    {
        return color == ColorState.BLACK ? "B" : "W";
    }

}

