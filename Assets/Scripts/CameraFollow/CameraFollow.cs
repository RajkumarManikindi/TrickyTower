using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private float _startPositionY;

    private void Awake()
    {
        _startPositionY = transform.localPosition.y;
    }

    public void KeepToStartPosition(float duration = 1)
    {
        OnLerpPosition(_startPositionY, duration);
    }
        
    public void MoveToPosition(float val,float duration = 1)
    {
        OnLerpPosition(val, duration);
    }
        


    private void OnLerpPosition(float positionToMoveTo, float duration = 1)
    {
        StartCoroutine(LerpPosition(positionToMoveTo, duration));
    }

    private IEnumerator LerpPosition(float targetPositionY, float duration)
    {
        var time = 0f;
        var position = transform.localPosition;
        var startPosition = position.y;
           
        while (time < duration)
        {
            var val = Mathf.Lerp(startPosition, targetPositionY, (time / duration));
            position = new Vector3(position.x, val, position.z);
            transform.localPosition = position;
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = new Vector3(position.x, targetPositionY,position.z);
    }
}
