using System;
using LabyrinthEscape.InputControls;
using UnityEngine;

namespace LabyrinthEscape.PlayerControls
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private Animation _animation;

        private Quaternion _verticalRotation;

        public void Awake()
        {
            _verticalRotation = Quaternion.Euler(0, 0, 90);
        }

        public void Update()
        {
            //MoveByControls();
            MoveByRigid();
        }

        private void MoveByRigid()
        {
            switch (InputController.Instance.CurrentMovingDirection)
            {
                case InputDirection.Up:
                {
                    _spriteRenderer.flipX = true;
                    transform.localRotation = _verticalRotation;

                    GetComponent<Rigidbody2D>().velocity = Vector2.up * _moveSpeed;
                    PlayWalkAnimation();
                    break;
                }

                case InputDirection.Right:
                {
                    _spriteRenderer.flipX = true;
                    transform.localRotation = Quaternion.identity;

                    GetComponent<Rigidbody2D>().velocity = Vector2.right * _moveSpeed;
                    PlayWalkAnimation();
                    break;
                }
                case InputDirection.Down:
                {
                    _spriteRenderer.flipX = false;
                    transform.localRotation = _verticalRotation;

                    GetComponent<Rigidbody2D>().velocity = Vector2.down * _moveSpeed;
                    PlayWalkAnimation();
                    break;
                }
                case InputDirection.Left:
                {
                    _spriteRenderer.flipX = false;
                    transform.localRotation = Quaternion.identity;

                    PlayWalkAnimation();
                    GetComponent<Rigidbody2D>().velocity = Vector2.left * _moveSpeed;
                    break;
                }

                case InputDirection.None:
                    PlayStayAnimation();
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    break;
            }
        }

        private InputDirection _previousDirection;

        [Obsolete]
        public void MoveByControls()
        {
            // 0.42f, а не половина спрайта, что бы не врезаться в стену прозрачностью
            var raycastStopDistanceX = _spriteRenderer.bounds.size.x * 0.42f;
            var raycastStopDistanceY = _spriteRenderer.bounds.size.y * 0.42f;

            if (_previousDirection != InputController.Instance.CurrentMovingDirection)
            {
                _previousDirection = InputController.Instance.CurrentMovingDirection;
                FixPosition(_previousDirection);
            }

            switch (InputController.Instance.CurrentMovingDirection)
            {
                case InputDirection.Up:
                {
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.transform.localRotation = _verticalRotation;

                    var hit = Physics2D.Raycast(transform.position, Vector2.up, raycastStopDistanceY + 0.01f);
                    if (hit.collider == null)
                    {
                        transform.Translate(0, _moveSpeed * Time.deltaTime, 0);
                        PlayWalkAnimation();
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, hit.point.y - raycastStopDistanceY);
                        PlayStayAnimation();
                    }
                    break;
                }

                case InputDirection.Right:
                {
                    _spriteRenderer.flipX = true;
                    _spriteRenderer.transform.localRotation = Quaternion.identity;

                    var hit = Physics2D.Raycast(transform.position, Vector2.right, raycastStopDistanceX + 0.01f);
                    if (hit.collider == null)
                    {
                        transform.Translate(_moveSpeed * Time.deltaTime, 0, 0);
                        PlayWalkAnimation();
                    }
                    else
                    {
                        transform.position = new Vector3(hit.point.x - raycastStopDistanceX, transform.position.y);
                        PlayStayAnimation();
                    }
                    break;
                }
                case InputDirection.Down:
                {
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.transform.localRotation = _verticalRotation;

                    var hit = Physics2D.Raycast(transform.position, Vector2.down, raycastStopDistanceY + 0.01f);
                    if (hit.collider == null)
                    {
                        transform.Translate(0, -_moveSpeed * Time.deltaTime, 0);
                        PlayWalkAnimation();
                    }
                    else
                    {
                        PlayStayAnimation();
                        transform.position = new Vector3(transform.position.x, hit.point.y + raycastStopDistanceY);
                    }
                    break;
                }
                case InputDirection.Left:
                {
                    _spriteRenderer.flipX = false;
                    _spriteRenderer.transform.localRotation = Quaternion.identity;

                    var hit = Physics2D.Raycast(transform.position, Vector2.left, raycastStopDistanceX + 0.01f);
                    if (hit.collider == null)
                    {
                        PlayWalkAnimation();
                        transform.Translate(-_moveSpeed * Time.deltaTime, 0, 0);
                    }
                    else
                    {
                        PlayStayAnimation();
                        transform.position = new Vector3(hit.point.x + raycastStopDistanceX, transform.position.y);
                    }
                    break;
                }

                case InputDirection.None:
                    PlayStayAnimation();
                    break;
            }
        }

        private void FixPosition(InputDirection direction)
        {
            if (direction == InputDirection.None) return;

            if (direction == InputDirection.Down || direction == InputDirection.Up)
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y,
                    transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y),
                    transform.position.z);
        }

        private bool _isCatStay = true;

        private void PlayStayAnimation()
        {
            if (_isCatStay) return;

            _isCatStay = true;
            _animation.Play("Cat_Stay");
        }

        private void PlayWalkAnimation()
        {
            if (!_isCatStay) return;

            _isCatStay = false;
            _animation.Play("Cat_Walk");
        }
    }
}