using System;
using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;

public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}

public class AIBehaviours
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}


public class Unit : MonoBehaviour
{
    AIBehaviours stateBehaviours = new AIBehaviours();

    void Start()
    {
        stateBehaviours.ChangeState(new TestState(this));
    }

    private void Update()
    {
        stateBehaviours.Update();
    }
}

public class TestState : IState
{
    Unit owner;
 
    public TestState(Unit owner) { this.owner = owner; }
 
    public void Enter()
    {
        Debug.Log("entering test state");
    }
 
    public void Execute()
    {
        Debug.Log("updating test state");
    }
 
    public void Exit()
    {
        Debug.Log("exiting test state");
    }
}

