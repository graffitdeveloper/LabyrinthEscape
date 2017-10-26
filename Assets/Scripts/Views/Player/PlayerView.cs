using System;
using LabyrinthEscape.InputControls;
using UnityEngine;

namespace LabyrinthEscape.PlayerControls
{
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

#pragma warning restore 649

        #endregion

        #region Fields

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

                    PlayStayAnimation();
                    _spriteRenderer.flipX = false;
                    transform.localRotation = Quaternion.identity;
                    _rigidbody2D.velocity = Vector2.zero;

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
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

            _isCatStay = false;
            _animation.Play("Cat_Walk");
        }

        #endregion
    }
}