using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LeaderController : MonoBehaviour
{
    private NavMeshAgent Nav;

    public StateManager stateManager;

    private enum LeaderAIState
    {
        Charge = 0, // ���� (���� �ʵ�����)
        Retreat,    // ���� (�ʵ忡�� �ܼ� ������)
        Improve,    // ����
        Occu_Flag,  // ��� ����  
        Occu_Enemy  // ��� ���� ����
    }

    private Dictionary<LeaderAIState, IState> dicState = new Dictionary<LeaderAIState, IState>();

    private void Start()
    {
        TryGetComponent<NavMeshAgent>(out Nav);

        IState charge = new StateCharge();
        IState retreat = new StateRetreat();
        IState improve = new StateImprove();
        IState occu_Flag = new StateOccuFlag();
        IState occu_Enemy = new StateOccuEnemy();

        // Dictionary �� ����
        dicState.Add(LeaderAIState.Charge, charge);
        dicState.Add(LeaderAIState.Retreat, retreat);
        dicState.Add(LeaderAIState.Improve, improve);
        dicState.Add(LeaderAIState.Occu_Flag, occu_Flag);
        dicState.Add(LeaderAIState.Occu_Enemy, occu_Enemy);

        stateManager = new StateManager(occu_Flag);

    }

    private void Update()
    {
        stateManager.DoOperStay();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            stateManager.SetState(dicState[LeaderAIState.Charge]);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            stateManager.SetState(dicState[LeaderAIState.Retreat]);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            stateManager.SetState(dicState[LeaderAIState.Improve]);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            stateManager.SetState(dicState[LeaderAIState.Occu_Flag]);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            stateManager.SetState(dicState[LeaderAIState.Occu_Enemy]);
        }
    }
}


