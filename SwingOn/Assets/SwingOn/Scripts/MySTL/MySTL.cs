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
}

namespace Structs
{
    [System.Serializable]
    public struct Status
    {
        public Enums.UnitNameTable unitName;

        public int maxHp;
        public int curHp;
    }

    [System.Serializable]
    public struct Components
    {
        public Rigidbody rigid;
        public Collider collider;
        public Animator aniCtrl;
    }

}





public class MySTL : MonoBehaviour
{
}
