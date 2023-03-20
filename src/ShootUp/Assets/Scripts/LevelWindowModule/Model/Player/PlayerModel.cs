using AudioModule.Contracts;
using SettingsModule;
using UniRx;
using UnityEngine;

namespace LevelWindowModule
{
    public class PlayerModel
    {
        private readonly PlayerHierarchy _playerHierarchy;
        private readonly GameSettings _gameSettings;
        private readonly IAudioPlayer _audioPlayer;
        private float _invulnerableTimer;
        private float _blinkTimer;
        private float _shootCooldown;
        private readonly ReactiveProperty<int> _heartCount = new();

        public float PositionX { get; private set; }
        public float GetWidth => _playerHierarchy.SpriteRenderer.bounds.size.x;
        public float GetHeight => _playerHierarchy.SpriteRenderer.bounds.size.y;
        public bool IsInvulnerable => _invulnerableTimer > 0;
        public Collider2D Collider => _playerHierarchy.Collider;
        public Vector3 BulletStartPosition => _playerHierarchy.BulletStartPosition.position;
        public bool CanShoot => IsAlive && _shootCooldown <= 0;

        public IReadOnlyReactiveProperty<int> HeartCount => _heartCount;
        public bool IsAlive => _heartCount.Value > 0;

        public PlayerModel(PlayerHierarchy playerHierarchy, GameSettings gameSettings, IAudioPlayer audioPlayer)
        {
            _playerHierarchy = playerHierarchy;
            _gameSettings = gameSettings;
            _audioPlayer = audioPlayer;
        }

        public void SetPositionX(float x)
        {
            PositionX = x;

            Vector3 position = _playerHierarchy.transform.position;
            position.x = x;
            _playerHierarchy.transform.position = position;
        }

        public void SetPositionY(float y)
        {
            Vector3 position = _playerHierarchy.transform.position;
            position.y = y;
            _playerHierarchy.transform.position = position;
        }

        public void SetHeartCount(int heartCount)
        {
            if (heartCount < 0)
                heartCount = 0;

            _heartCount.Value = heartCount;
        }

        public void SetInvulnerableTimer(float duration)
        {
            _invulnerableTimer = duration;
            _blinkTimer = _gameSettings.BlinkPeriod;
        }

        public void GetHit()
        {
            int heartCount = _heartCount.Value - 1;
            SetHeartCount(heartCount);
            SetInvulnerableTimer(_gameSettings.HitInvulDuration);

            _audioPlayer.PlayOnce(heartCount == 0 ? SoundId.LoseLevel : SoundId.HeroHurt);
        }

        public void Update(float deltaTime)
        {
            if (_invulnerableTimer > 0)
            {
                _invulnerableTimer -= deltaTime;
                UpdateBlink(deltaTime);
            }

            if (_shootCooldown > 0)
                _shootCooldown -= deltaTime;
        }

        public void SetShootCooldown(float playerShootCooldown)
        {
            _shootCooldown = playerShootCooldown;
        }

        private void UpdateBlink(float deltaTime)
        {
            Color color = Color.white;
            if (_invulnerableTimer > 0)
            {
                _blinkTimer -= deltaTime;
                if (_blinkTimer < 0)
                    _blinkTimer = _gameSettings.BlinkPeriod;

                if (_blinkTimer > _gameSettings.BlinkPeriod / 2)
                    color = Color.clear;
            }

            _playerHierarchy.SpriteRenderer.color = color;
        }
    }
}