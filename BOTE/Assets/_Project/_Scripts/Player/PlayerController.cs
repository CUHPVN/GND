using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2Int position;
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 cachePos;
    [SerializeField] private bool availableToMove=true;
    [SerializeField] private float remainTime=0;
    private Coroutine moveCor;
    public bool MoveTo(Vector2[] vector,Vector2Int[] pos)
    {
        if(vector.Length==1) return false;
        if(availableToMove==false) return false;
        if (moveCor != null)
        {
            StopCoroutine(moveCor);
            availableToMove=false;
            StartCoroutine(MoveToCache(remainTime));
        }
        moveCor = StartCoroutine(MoveStepByStep(vector,pos));
        
        return true;
    }
    private IEnumerator MoveStepByStep(Vector2[] vector,Vector2Int[] pos)
    {
        int index = 1,length = vector.Length;
        while(!availableToMove) yield return null;
        while (index < vector.Length)
        {
            float t=0;
            float duration = 1/speed;
            cachePos =vector[index];
            position=pos[index];
            DirectionCalulate(transform.position,cachePos);
            while (t < duration)
            {
                t+=Time.deltaTime;
                remainTime = t;
                transform.position = Vector3.Lerp(transform.position,vector[index],t/duration);
                yield return null;
            }
            transform.position = vector[index];
            index++;
        }
        moveCor=null;
    }
    private IEnumerator MoveToCache(float t)
    {
        float duration = 1/speed;
        DirectionCalulate(transform.position,cachePos);
        while (t < duration)
        {
            t+=Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position,cachePos,t/duration);
            yield return null;
        }
        transform.position = cachePos;
        availableToMove=true;
    }
    public (int x,int y) GetPosition()
    {
        return (position.x,position.y);
    }
    public Direction DirectionCalulate(Vector2 first,Vector2 second)
    {
        Vector2 res=second-first;
        if (Mathf.Approximately(res.x,0) && res.y <0)
        {
            return Direction.Down;
        }else
        if (Mathf.Approximately(res.x,0) && res.y >0)
        {
            return Direction.Up;
        }else
        if (res.x < 0 && Mathf.Approximately(res.y,0))
        {
            return Direction.Left;
        }else
        if (res.x > 0 && Mathf.Approximately(res.y,0))
        {
            return Direction.Right;
        }else
        if (res.x > 0 && res.y < 0)
        {
            return Direction.RightDown;
        }else
        if (res.x > 0 && res.y > 0)
        {
            return Direction.RightUp;
        }else
        if (res.x < 0 && res.y < 0)
        {
            return Direction.LeftDown;
        }else 
        if (res.x < 0 && res.y > 0)
        {
            return Direction.LeftUp;
        } 
        return Direction.Down;
    }
}
public enum Direction
{
    Up=0,
    RightUp=1,
    Right=2,
    RightDown=3,
    Down=4,
    LeftDown=5,
    Left=6,
    LeftUp=7,
}
