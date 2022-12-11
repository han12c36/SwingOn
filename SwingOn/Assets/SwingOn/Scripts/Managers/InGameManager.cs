using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Manager<InGameManager>
{
    private Player player;
    private Vector3 playerStartPos = Vector3.zero;

    public Player GetPlayer { get { return player; } }

    private void Awake()
    {
        Player.instance.transform.position = playerStartPos;
        player = Player.instance;
    }
    private void OnEnable()
    {
    }
    private void Start()
    {
    }

    private void Update()
    {
    }
    private void OnDisable()
    {
    }
}
