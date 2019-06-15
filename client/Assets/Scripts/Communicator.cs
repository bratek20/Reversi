using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communicator : MonoBehaviour
{
    public class MoveData
    {
        public int I;
        public int J;

        public bool IsPass()
        {
            return I == -1 && J == -1;
        }
    }

    public static MoveData CalcOptimalMove(string boardSnapshot, ColorState myColor, int recurenceDeep)
    {
        string ans = BridgeDLL.Send(boardSnapshot + "#" + myColor.ToAIString() + "#" + recurenceDeep);
        string[] splitAns = ans.Split('#');
        return new MoveData
        {
            I = int.Parse(splitAns[0]),
            J = int.Parse(splitAns[1])
        };
    }
}
