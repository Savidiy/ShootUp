using Savidiy.Utils.StateMachine;
using StartWindowModule.Contracts;

namespace MainModule
{
    public sealed class SelectControlMainState : IState, IStateWithExit, IMainState
    {
        private readonly ISelectControlsWindowPresenter _selectControlsWindowPresenter;

        public SelectControlMainState(ISelectControlsWindowPresenter selectControlsWindowPresenter)
        {
            _selectControlsWindowPresenter = selectControlsWindowPresenter;
        }
        
        public void Enter()
        {
            _selectControlsWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _selectControlsWindowPresenter.HideWindow();
        }
    }
}