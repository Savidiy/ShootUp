using MainModule;
using Zenject;

namespace Bootstrap
{
    public sealed class Bootstrapper : IInitializable
    {
        private readonly MainStateMachine _mainStateMachine;

        public Bootstrapper(MainStateMachine mainStateMachine, StartMainState startMainState,
            LevelPlayMainState levelPlayMainState, SelectControlMainState selectControlMainState)
        {
            _mainStateMachine = mainStateMachine;

            _mainStateMachine.AddState(startMainState);
            _mainStateMachine.AddState(levelPlayMainState);
            _mainStateMachine.AddState(selectControlMainState);
        }

        public void Initialize()
        {
            _mainStateMachine.EnterToState<StartMainState>();
        }
    }
}