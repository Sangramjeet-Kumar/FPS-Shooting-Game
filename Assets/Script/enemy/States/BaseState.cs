public abstract class BaseState
{
    public Enemy1 enemy1;
    public StateMachine stateMachine;

    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();
}