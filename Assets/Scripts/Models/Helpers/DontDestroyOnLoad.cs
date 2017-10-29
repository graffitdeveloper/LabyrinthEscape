using UnityEngine;

namespace LabyrinthEscape.SomeHelpers
{
    /// <summary>
    /// Вспомогательный скрипт, для быстрого создания неудаляемых со сцены объектов
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        protected void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}