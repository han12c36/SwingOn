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
    
    
}

public abstract class MyFunc : MonoBehaviour
{
    //public void Parabolic(GameObject obj,GameObject target, float initialVelocity)
    //{
    //    Vector3 startPos = obj.transform.position;
    //    Vector3 endPos = target.transform.position;
    //    float gravity = Physics.gravity.magnitude;
    //    float moveReach = Vector3.Distance(endPos,startPos);
    //    float initialAngle =  Mathf.Asin(moveReach * gravity / Mathf.Pow(initialVelocity, 2)) / 2;
    //    float MaxHeight = Mathf.Pow(initialAngle, 2) / 2 * gravity;
    //    initialVelocity = initialVelocity * Mathf.Sin(initialAngle * Mathf.Deg2Rad);
    //
    //    Vector3 HorizontalDir = endPos - startPos;
    //
    //    CoroutineHelper.StartCorotine(ParabolicMotion(obj,MaxHeight,initialVelocity, initialAngle,endPos));
    //}
    //
    //public static IEnumerator ParabolicMotion(GameObject obj,float gravity,float initialVelocity, float initialAngle,Vector3 endPos)
    //{
    //    float timer = 0.0f;
    //    while (true)
    //    {
    //        timer += Time.deltaTime;
    //        obj.transform.position.y += (initialVelocity * Mathf.Sin(initialAngle) * timer) - (0.5f * gravity * Mathf.Pow(gravity,2));
    //        obj.transform.position.x += HorizontalDir.x * initialVelocity * timer;
    //        obj.transform.position.z += HorizontalDir.z * initialVelocity * timer;
    //
    //        if (obj.transform.position == endPos) break;
    //        yield return null;
    //    }
    //}
}
