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
        private readonly PlayerShooter _playerShooter;
        private readonly BulletMover _bulletMover;
        private readonly BulletAtBorderKiller _bulletAtBorderKiller;
        private readonly BulletAtEnemyChecker _bulletAtEnemyChecker;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly BulletHolder _bulletHolder;
        private readonly EnemyAtLivesKiller _enemyAtLivesKiller;

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, LevelHolder levelHolder,
            BorderController borderController, EnemyMover enemyMover, PlayerHolder playerHolder, PlayerMover playerMover,
            EnemyAttackExecutor enemyAttackExecutor, PlayerShooter playerShooter, BulletMover bulletMover,
            BulletAtBorderKiller bulletAtBorderKiller, BulletAtEnemyChecker bulletAtEnemyChecker, EnemiesHolder enemiesHolder,
            BulletHolder bulletHolder, EnemyAtLivesKiller enemyAtLivesKiller)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _levelHolder = levelHolder;
            _borderController = borderController;
            _enemyMover = enemyMover;
            _playerHolder = playerHolder;
            _playerMover = playerMover;
            _enemyAttackExecutor = enemyAttackExecutor;
            _playerShooter = playerShooter;
            _bulletMover = bulletMover;
            _bulletAtBorderKiller = bulletAtBorderKiller;
            _bulletAtEnemyChecker = bulletAtEnemyChecker;
            _enemiesHolder = enemiesHolder;
            _bulletHolder = bulletHolder;
            _enemyAtLivesKiller = enemyAtLivesKiller;
        }

        public void Enter()
        {
            _borderController.UpdateBorders();

            _levelHolder.LoadCurrentLevel();
            _playerHolder.ResetPlayer();
            _enemyAttackExecutor.Activate();
            _playerMover.Activate();
            _playerShooter.Activate();
            _bulletMover.Activate();
            _enemyMover.Activate();
            _bulletAtBorderKiller.Activate();
            _bulletAtEnemyChecker.Activate();
            _enemyAtLivesKiller.Activate();

            _levelWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _levelWindowPresenter.HideWindow();

            _enemyAttackExecutor.Deactivate();
            _playerMover.Deactivate();
            _playerShooter.Deactivate();
            _enemyMover.Deactivate();
            _bulletMover.Deactivate();
            _bulletAtBorderKiller.Deactivate();
            _bulletAtEnemyChecker.Deactivate();
            _enemyAtLivesKiller.Deactivate();

            _enemiesHolder.Clear();
            _bulletHolder.Clear();
        }
    }
}