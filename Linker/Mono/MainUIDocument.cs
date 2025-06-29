using UnityEngine;
using UnityEngine.UIElements;

public class MainUIDocument : MonoBehaviour
{
    public static UIDocument Instance;
    
    private void Awake()
    {
        Instance = GetComponent<UIDocument>();
    }
}