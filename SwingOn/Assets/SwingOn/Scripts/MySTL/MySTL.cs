using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enums
{
    public enum UnitNameTable
    {
        None,
        Player,
        Enemy1,
        Enemy2,
        Enemy3,
        Enemy_MB,
        Enemy_B,
        End
    }
    public enum EnemyType
    {
        Normal,
        Hard,
        MiniBoss,
        Boss,
    }

    //public enum PlayerStates
    //{
    //    None,
    //    Idle,
    //    Move,
    //    Att,
    //    Death,
    //    End
    //}
    public enum PlayerActions
    {
        None,
        NormalAtt,
        SpeedAtt,
        HardAtt,
        Skill_1,
        Skill_2,
        Skill_3,
        Damaged,
        Death,
        End
    }
    public enum Enemy_1Actions
    {
        Idle,
        Trace,
        Att_1,
        Att_2,
        Damaged,
        Death,
        End
    }
    public enum Enemy_1EggActions
    {
        Idle,
        Damaged,
        Death,
        End
    }
    public enum Enemy_2Actions
    {
        Ready,
        Idle,
        Trace,
        MeleeAtt,
        LongAtt,
        RangeAtt,
        Damaged,
        Death,
        End
    }
    public enum Enemy_3Actions
    {
        Ready,
        Idle,
        Trace,
        MeleeAtt,
        Explosion,
        Damaged,
        Death,
        End
    }
    public enum Enemy_4Actions
    {
        Ready,
        Idle,
        Walk,
        Run,
        MeleeAtt,
        RangeAtt,
        DashAtt,
        Damaged,
        Death,
        End
    }

    public enum PlayerAttType
    {
        Normal,
        Speed,
        Hard,
        End
    }
    


}

namespace Structs
{
    [System.Serializable]
    public struct Status
    {
        public Enums.UnitNameTable unitName;

        public int maxHp;
        public int curHp;
        public int preHp;

        public float AttRange;
        public float Speed;
    }

    [System.Serializable]
    public struct UnitComponents
    {
        public Rigidbody rigid;
        public Collider collider;
        public Animator aniCtrl;
    }
}

public static class MySTL
{

    public static Vector3 RandomVec(Vector3 originPos,float range)
    {
        float range_X, range_Z;
        range_X = Random.Range((range / 2) * -1, range / 2);
        range_Z = Random.Range((range / 2) * -1, range / 2);
        Vector3 randomPos = new Vector3(range_X, 0f, range_Z);

        Vector3 RandomPos = originPos + randomPos;
        return RandomPos;
    }
    public static Vector3 RandomVec(Vector3 originPos, float minRange,float maxRange)
    {
        float range_X, range_Z;
        range_X = Random.Range((minRange / 2) * -1, minRange / 2);
        range_Z = Random.Range((minRange / 2) * -1, minRange / 2);
        Vector3 randomPos = new Vector3(range_X, 0f, range_Z);

        Vector3 RandomPos = originPos + randomPos;
        return RandomPos;
    }

    public static int Think(float[] probs)
    {
        float total = 0;
        foreach (float elem in probs) { total += elem; }
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i]) return i;
            else randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }
}

