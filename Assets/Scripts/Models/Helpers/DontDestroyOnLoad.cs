using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    protected void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}