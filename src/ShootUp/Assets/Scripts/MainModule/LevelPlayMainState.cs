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
        private readonly PlayerHolder _playerHolder;
        private readonly PlayerMover _playerMover;
        private readonly EnemyAttackExecutor _enemyAttackExecutor;

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, LevelHolder levelHolder,
            BorderController borderController, EnemyMover enemyMover, PlayerHolder playerHolder, PlayerMover playerMover,
            EnemyAttackExecutor enemyAttackExecutor)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _levelHolder = levelHolder;
            _borderController = borderController;
            _enemyMover = enemyMover;
            _playerHolder = playerHolder;
            _playerMover = playerMover;
            _enemyAttackExecutor = enemyAttackExecutor;
        }

        public void Enter()
        {
            _levelHolder.LoadCurrentLevel();
            _borderController.UpdateBorders();
            _playerHolder.ResetPlayer();
            _enemyAttackExecutor.Activate();
            _playerMover.Activate();
            _enemyMover.Activate();
            _levelWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _enemyAttackExecutor.Deactivate();
            _playerMover.Deactivate();
            _enemyMover.Deactivate();
            _levelWindowPresenter.HideWindow();
        }
    }
}