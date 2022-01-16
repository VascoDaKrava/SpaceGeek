using UnityEngine;
namespace GeekSpace
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BulletProvider : MonoBehaviour
    {
        private BulletModel _bulletModel;
        private Rigidbody2D _rigidbody2D;
        internal BulletModel BulletModel
        {
            get { return _bulletModel; }
            set
            {
                _bulletModel = value;
                _bulletModel.Object = gameObject;
            }
        }
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        void OnBecameInvisible()
        {
            GameEventSystem.current.GoingBeyondScreen(_bulletModel);
        }
        private void OnBecameVisible()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        void OnTriggerEnter2D()
        {
            GameEventSystem.current.GoingBeyondScreen(_bulletModel);
        }

        void ReturnToPool()
        {

        }
    }
}