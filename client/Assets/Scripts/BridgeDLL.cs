using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BridgeDLL : MonoBehaviour
{
    [DllImport("AI", EntryPoint = "send", CallingConvention = CallingConvention.Cdecl)]
    private static extern void SendDLL(byte[] str);

    public static string Send(string msg)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(msg);
        SendDLL(buffer);
        return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
    }
}
