using System;
using LabyrinthEscape.GridControls;
using LabyrinthEscape.InputControls;
using UnityEngine;

namespace LabyrinthEscape.PlayerControls
{
    public delegate void OnPlayerFinishedLabyrinth();

    /// <summary>
    /// Скрипт игрока
    /// </summary>
    public class PlayerView : MonoBehaviour
    {
        #region Layout

#pragma warning disable 649

        /// <summary>
        /// Скорость бега
        /// </summary>
        [SerializeField] private float _moveSpeed;

        /// <summary>
        /// Спрайт игрока
        /// </summary>
        [SerializeField] private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Анимация движения или стояния игрока
        /// </summary>
        [SerializeField] private Animation _animation;

        [SerializeField] private PawSpawner _pawSpawner;

#pragma warning restore 649

        #endregion

        #region Fields

        public OnPlayerFinishedLabyrinth OnPlayerFinishedLabyrinth;

        /// <summary>
        /// Физика
        /// </summary>
        private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// Вертикальное вращение (поворот на 90 градусов по Z по сути, для того что бы идти вверх и вниз)
        /// </summary>
        private Quaternion _verticalRotation;

        /// <summary>
        /// true если играет анимация стояния, false - если анимация ходьбы
        /// </summary>
        private bool _isCatStay = true;

        private bool _isMovedOnce = false;

        private bool _isFinished = false;

        #endregion

        #region Methods

        /// <summary>
        /// Инициализация
        /// </summary>
        public void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _verticalRotation = Quaternion.Euler(0, 0, 90);
        }

        /// <summary>
        /// Вызывается каждый фрейм
        /// </summary>
        public void Update()
        {
            MoveByControls();
        }

        /// <summary>
        /// Заставляет ходить персонажа в соответствии с управлением
        /// </summary>
        private void MoveByControls()
        {
            if (_isFinished)
            {
                _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, Vector2.zero, 3 * Time.deltaTime);
                transform.localRotation = Quaternion.identity;
                return;
            }

            switch (InputController.Instance.CurrentMovingDirection)
            {
                case InputDirection.Up:

                    _spriteRenderer.flipX = true;
                    transform.localRotation = _verticalRotation;
                    _rigidbody2D.velocity = Vector2.up * _moveSpeed;
                    PlayWalkAnimation();

                    break;

                case InputDirection.Right:

                    _spriteRenderer.flipX = true;
                    transform.localRotation = Quaternion.identity;
                    _rigidbody2D.velocity = Vector2.right * _moveSpeed;
                    PlayWalkAnimation();

                    break;

                case InputDirection.Down:

                    _spriteRenderer.flipX = false;
                    transform.localRotation = _verticalRotation;
                    _rigidbody2D.velocity = Vector2.down * _moveSpeed;
                    PlayWalkAnimation();

                    break;

                case InputDirection.Left:

                    _spriteRenderer.flipX = false;
                    transform.localRotation = Quaternion.identity;
                    PlayWalkAnimation();
                    _rigidbody2D.velocity = Vector2.left * _moveSpeed;

                    break;

                case InputDirection.None:
                    if (_isMovedOnce)
                        PlayStayAnimation();
                    else
                        PlaySleepAnimation();
                    _spriteRenderer.flipX = false;
                    transform.localRotation = Quaternion.identity;
                    _rigidbody2D.velocity = Vector2.zero;

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "FinishCell")
                if (!_isFinished)
                {
                    _pawSpawner.Enabled = false;
                    _isFinished = true;
                    transform.localRotation = Quaternion.identity;

                    PlayYeahAnimation();

                    if (OnPlayerFinishedLabyrinth != null)
                        OnPlayerFinishedLabyrinth();
                }
        }

        /// <summary>
        /// Включение анимации стояния
        /// </summary>
        private void PlaySleepAnimation()
        {
            _animation.Play("Cat_Sleeping");
        }

        /// <summary>
        /// Включение анимации стояния
        /// </summary>
        private void PlayStayAnimation()
        {
            if (_isCatStay) return;

            _isCatStay = true;
            _animation.Play("Cat_Stay");
        }

        /// <summary>
        /// Включение анимации ходьбы
        /// </summary>
        private void PlayWalkAnimation()
        {
            if (!_isCatStay) return;

            _isMovedOnce = true;

            _isCatStay = false;
            _animation.Play("Cat_Walk");
        }

        private void PlayYeahAnimation()
        {
            _animation.Play("Cat_Yeah");
        }

        #endregion

        public void Spawn(GridCell gridCell)
        {
            _pawSpawner.Enabled = true;
            _pawSpawner.ClearCurrentPaws();

            _isFinished = false;
            transform.position = new Vector3(gridCell.PositionX, gridCell.PositionY, 0);
            _isMovedOnce = false;
        }
    }
}