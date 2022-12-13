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
        HardAtt,
        Skill_1,
        Skill_2,
        Skill_3,
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
    public enum PlayerAttType
    {
        Normal,
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
}
