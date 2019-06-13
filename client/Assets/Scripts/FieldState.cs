public enum FieldState
{
    EMPTY,
    SELECTABLE,
    WHITE,
    BLACK
}

public static class FieldStateUtils
{
    public static bool IsColor(this FieldState type)
    {
        return type == FieldState.BLACK || type == FieldState.WHITE;
    }
}

