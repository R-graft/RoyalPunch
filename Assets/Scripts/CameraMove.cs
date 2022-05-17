using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove Instance { get; private set; }

    private Vector3 _menuPosition;

    [SerializeField]
    private Rigidbody _playerTransform;

    private void Awake()
    {
        Instance = this;

       _menuPosition = new Vector3(0, 2f, 5);
    }

    private void Update()
    {
        transform.LookAt(_playerTransform.position);
    }
    public IEnumerator MoveAway()
    {
        while (transform.localPosition.y < 12f)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate( new Vector3(2,0.5f,-0.8f) * 0.2f);
        }
        yield break;
    }
    public void MenuPosition()
    {
        transform.localPosition = _menuPosition; 
        transform.localEulerAngles = new Vector3(15, 180, 0);
    }
}
