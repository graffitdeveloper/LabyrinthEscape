using UnityEngine;

namespace LabGen.LabyrinthElements
{
    public abstract class LabyrinthElementView : MonoBehaviour
    {
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                transform.position = _position;
                name = string.Format("{0}_{1}:{2}", ElementName, _position.x, _position.y);
            }
        }

        protected abstract string ElementName { get; }
    }
}