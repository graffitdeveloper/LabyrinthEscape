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

        public void Update()
        {
            MoveByControls();
        }

        public void MoveByControls()
        {
            // 0.42f, а не половина спрайта, что бы не врезаться в стену прозрачностью
            var raycastStopDistanceX = _spriteRenderer.bounds.size.x * 0.42f;
            var raycastStopDistanceY = _spriteRenderer.bounds.size.y * 0.42f;


            switch (InputController.Instance.CurrentMovingDirection)
            {
                case InputDirection.Up:
                {
                    _spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
                    _spriteRenderer.flipX = true;

                    var hit = Physics2D.Raycast(transform.position, Vector2.up, raycastStopDistanceY);
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
                    _spriteRenderer.transform.rotation = Quaternion.identity;
                    _spriteRenderer.flipX = true;

                    var hit = Physics2D.Raycast(transform.position, Vector2.right, raycastStopDistanceX);
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
                    _spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
                    _spriteRenderer.flipX = false;

                    var hit = Physics2D.Raycast(transform.position, Vector2.down, raycastStopDistanceY);
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
                    _spriteRenderer.transform.rotation = Quaternion.identity;
                    _spriteRenderer.flipX = false;

                    var hit = Physics2D.Raycast(transform.position, Vector2.left, raycastStopDistanceX);
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

        private void PlayStayAnimation()
        {
            _animation.Play("Cat_Stay");
        }

        private void PlayWalkAnimation()
        {
            _animation.Play("Cat_Walk");
        }
    }
}