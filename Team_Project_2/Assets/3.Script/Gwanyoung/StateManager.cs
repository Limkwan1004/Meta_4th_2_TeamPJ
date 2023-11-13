using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    public IState CurrentState { get; private set; }

    public StateManager(IState defaultState)
    {
        CurrentState = defaultState;
    }

    // ���� �ٲ� �� ����
    public void SetState(IState state)
    {
        CurrentState.OperExit();

        CurrentState = state;

        CurrentState.OperEnter();
    }

    // ������Ʈ �� �޼���
    public void DoOperStay()
    {
        CurrentState.OperStay(); 
    }
}

public interface IState
{
    void OperEnter();    // ���°� ���۵� ��
    void OperStay();     // ���� ���� ���� ��
    void OperExit();     // ���°� ���� ��
}