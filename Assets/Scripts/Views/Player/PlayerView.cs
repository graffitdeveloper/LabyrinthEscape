using System;
using Assets.Scripts.Views;
using LabyrinthEscape.GameManagerControls;
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

        /// <summary>
        /// Скорость бега
        /// </summary>
        [SerializeField]
        private GameObject _salute;

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
            if (GameManager.Instance.IsGamePaused)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _pawSpawner.Enabled = false;
                return;
            }

            if (GameManager.Instance.IsGameFinished)
            {
                _rigidbody2D.velocity = Vector2.Lerp(_rigidbody2D.velocity, Vector2.zero, 3 * Time.deltaTime);
                transform.localRotation = Quaternion.identity;
                return;
            }

            if(!_pawSpawner.Enabled)
                _pawSpawner.Enabled = true;

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
                    if (GameManager.Instance.IsGameStarted)
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
                if (!GameManager.Instance.IsGameFinished)
                {
                    _pawSpawner.Enabled = false;
                    GameManager.Instance.IsGameFinished = true;
                    transform.localRotation = Quaternion.identity;

                    PlayYeahAnimation();

                    _salute.SetActive(true);

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

            if(!GameManager.Instance.IsGameStarted)
                SoundManagerView.Instance.PlayRandomActionMusic();

            GameManager.Instance.IsGameStarted = true;

            _isCatStay = false;
            _animation.Play("Cat_Walk");
        }

        private void PlayYeahAnimation()
        {
            _animation.Play("Cat_Yeah");
            SoundManagerView.Instance.PlayCongratsEffect();
        }

        #endregion

        public void Spawn(GridCell gridCell)
        {
            _pawSpawner.Enabled = true;
            _pawSpawner.ClearCurrentPaws();
            _salute.SetActive(false);
            SoundManagerView.Instance.PlayCricket();

            GameManager.Instance.IsGamePaused = false;
            GameManager.Instance.IsGameFinished = false;
            GameManager.Instance.IsGameStarted = false;

            transform.position = new Vector3(gridCell.PositionX, gridCell.PositionY, 0);
        }
    }
}