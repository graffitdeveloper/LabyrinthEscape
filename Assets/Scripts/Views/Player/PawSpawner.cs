using LabyrinthEscape.InputControls;
using UnityEngine;

namespace LabyrinthEscape.PlayerControls
{
    /// <summary>
    /// Скрипт игрока
    /// </summary>
    public class PawSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _pawPrefab;

        [SerializeField] private float _pawSpawnTickTime;

        [SerializeField] private float _pawRotation;

        [SerializeField] private float _pawsWidth;

        [SerializeField] private float _pawsRandomizePosition;


        [SerializeField] private Transform _pawsContainer;

        [SerializeField] private ParticleSystem _dustParticles;

        private float _pawSpawnCurrentTime;
        private bool _isLeftPaw;

        private void Awake()
        {
            _pawSpawnCurrentTime = _pawSpawnTickTime;
        }

        public void Update()
        {
            _pawSpawnCurrentTime -= Time.deltaTime;
            if (_pawSpawnCurrentTime <= 0)
            {
                SpawnPaw();
                _pawSpawnCurrentTime = _pawSpawnTickTime;
            }
        }

        private void SpawnPaw()
        {
            if (InputController.Instance.CurrentMovingDirection == InputDirection.None)
            {
                if (_dustParticles.isPlaying)
                    _dustParticles.Stop();

                return;
            }

            if (!_dustParticles.isPlaying)
                _dustParticles.Play();

            var pawRotation = Quaternion.identity;
            var pawPosition = Vector3.zero;
            var pawOffset = _isLeftPaw ? _pawsWidth : -_pawsWidth;

            switch (InputController.Instance.CurrentMovingDirection)
            {
                case InputDirection.Up:
                {
                    pawRotation = Quaternion.Euler(0, 0, _isLeftPaw ? -_pawRotation : _pawRotation);

                    pawPosition = new Vector3(
                        transform.position.x + pawOffset,
                        transform.position.y,
                        transform.position.z);

                    break;
                }
                case InputDirection.Right:
                {
                    pawRotation = Quaternion.Euler(0, 0, -90) *
                                  Quaternion.Euler(0, 0, _isLeftPaw ? _pawRotation : -_pawRotation);

                    pawPosition = new Vector3(
                        transform.position.x,
                        transform.position.y + pawOffset,
                        transform.position.z);

                    break;
                }
                case InputDirection.Down:
                {
                    pawRotation = Quaternion.Euler(0, 0, 180) *
                                  Quaternion.Euler(0, 0, _isLeftPaw ? _pawRotation : -_pawRotation);

                    pawPosition = new Vector3(
                        transform.position.x + pawOffset,
                        transform.position.y,
                        transform.position.z);

                    break;
                }
                case InputDirection.Left:
                {
                    pawRotation = Quaternion.Euler(0, 0, 90) *
                                  Quaternion.Euler(0, 0, _isLeftPaw ? -_pawRotation : _pawRotation);

                    pawPosition = new Vector3(
                        transform.position.x,
                        transform.position.y + pawOffset,
                        transform.position.z);

                    break;
                }
            }

            pawPosition += new Vector3(
                Random.Range(-_pawsRandomizePosition, _pawsRandomizePosition),
                Random.Range(-_pawsRandomizePosition, _pawsRandomizePosition), 0);

            Instantiate(_pawPrefab, pawPosition, pawRotation, _pawsContainer);
            _isLeftPaw = !_isLeftPaw;
        }
    }
}