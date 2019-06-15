public enum Color
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

    public static Color ToColor(this FieldState state)
    {
        return (Color)state;
    }

    public static void ResetSelect(ref this FieldState state)
    {
        if(state == FieldState.SELECTABLE)
        {
            state = FieldState.EMPTY;
        }
    }
}

