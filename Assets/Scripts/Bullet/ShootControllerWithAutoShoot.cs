using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeekSpace
{
    internal class ShootControllerWithAutoShoot : IExecute, IShootController
    {
        internal TimerSystem _timerSystem;
        internal IPool _enemyPool;
        internal Transform _startPosition;
        private Transform _startPosition2;
        internal GameObject _player;
        internal RaycastHit2D _hit;
        internal LayerMask _enemyLayerMask;
        internal AudioClip _shootAudioClip;
        internal Camera _camera;
        internal ShootControllerWithAutoShoot(TimerSystem timerSystem, IPool enemyPool, Transform startPosition, GameObject player, string enemyLayerMask, AudioClip shootAudioClip)
        {
            _startPosition = startPosition;
            _timerSystem = timerSystem;
            _enemyPool = enemyPool;
            _player = player;
            _enemyLayerMask = LayerMask.GetMask(enemyLayerMask);
            _shootAudioClip = shootAudioClip;
            _camera = Camera.main;
        }

        public virtual void GetShoot()
        {
            if (_player == null) return;
            _hit = Physics2D.Raycast(_player.transform.position, _player.transform.up, 100.0f, _enemyLayerMask);
            if (_timerSystem.CheckEvent() && _hit)
            {
                Extention.GetOrAddComponent<AudioSource>(_camera.gameObject).PlayOneShot(_shootAudioClip);
                var a = _enemyPool.Pop(_startPosition.position, _startPosition.rotation);
                a.GetComponent<Rigidbody2D>().AddForce(_startPosition.transform.up * 3);
                return;
            }
        }

        public void Execute(float deltaTime)
        {
            GetShoot();
        }
    }
}