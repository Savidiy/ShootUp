using LevelWindowModule;
using LevelWindowModule.Contracts;
using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LevelPlayMainState : IState, IStateWithExit, IMainState
    {
        private readonly ILevelWindowPresenter _levelWindowPresenter;
        private readonly LevelHolder _levelHolder;
        private readonly BorderController _borderController;
        private readonly EnemyMover _enemyMover;

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, LevelHolder levelHolder,
            BorderController borderController, EnemyMover enemyMover)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _levelHolder = levelHolder;
            _borderController = borderController;
            _enemyMover = enemyMover;
        }

        public void Enter()
        {
            _levelHolder.LoadCurrentLevel();
            _borderController.UpdateBorders();
            _enemyMover.Activate();
            _levelWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _enemyMover.Deactivate();
            _levelWindowPresenter.HideWindow();
        }
    }
}