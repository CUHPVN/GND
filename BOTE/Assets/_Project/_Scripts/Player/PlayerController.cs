using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Vector2Int position;
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector3 cachePos;
    [SerializeField] private bool availableToMove=true;
    [SerializeField] private float remainTime=0;
    [SerializeField] private GameObject FObject;
    [SerializeField] private FarmGridManager farmGridManager;
    [SerializeField] private InteractGridManager interactGridManager;
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
        if(AchievementManager.Instance != null) AchievementManager.Instance.TriggerAchievement(AchievementManager.Instance.GetAchievement("first_move"));
        return true;
    }
    private IEnumerator MoveStepByStep(Vector2[] vector,Vector2Int[] pos)
    {
        int index = 1,length = vector.Length;
        while(!availableToMove) yield return null;
        while (index < vector.Length)
        {
            float t=0;
            float tmpSpeed = speed;
            Direction direction = DirectionCalulate(transform.position, cachePos);
            cachePos =vector[index];
            position=pos[index];
            if(direction == Direction.Up || direction == Direction.Down || direction == Direction.Right || direction ==Direction.Left)
            {
                tmpSpeed/=Mathf.Sqrt(2);
            }
            float duration = 1/tmpSpeed;
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
        float tmpSpeed = speed;
        Direction direction = DirectionCalulate(transform.position, cachePos);
        if(direction == Direction.Up || direction == Direction.Down || direction == Direction.Right || direction ==Direction.Left)
        {
            tmpSpeed/=Mathf.Sqrt(2);
        }
        float duration = 1/tmpSpeed;
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
    void Update()
    {
        CheckInteract();
    }
    void CheckInteract()
    {
        Vector3 playerPosition = transform.position;
        FarmLand farmLand = farmGridManager.grid.GetGridObject(playerPosition);
        InteractObject interactObject = interactGridManager.grid.GetGridObject(playerPosition);
        if (farmLand != null)
        {
            if(AchievementManager.Instance != null) AchievementManager.Instance.TriggerAchievement("stand_on_farmland");
            ShowInteract();
            if (GamePlayManager.Instance != null && !GamePlayManager.Instance.BlockInput && Input.GetKeyDown(KeyCode.F))
            {
                if (CircularManager.Instance != null && farmLand.Interact(CircularManager.Instance.currentItem,out bool isItemChanged))
                {
                    CircularManager.Instance.UseItem();
                    if (isItemChanged)
                    {
                        CircularManager.Instance.ChangeItem();
                    }
                }
            }
            
        }
        else
        if (interactObject != null)
        {
            ShowInteract();
            if (GamePlayManager.Instance != null && !GamePlayManager.Instance.BlockInput && Input.GetKeyDown(KeyCode.F))
            {
                if (CircularManager.Instance != null && interactObject.interactable.Interact(CircularManager.Instance.currentItem,out bool isItemChanged))
                {
                    CircularManager.Instance.UseItem();
                    if (isItemChanged)
                    {
                        CircularManager.Instance.ChangeItem();
                    }
                }
            }
        }
        else
        {
            HideInteract();
        }
    }
    internal void ShowInteract()
    {
        FObject.SetActive(true);
    }

    internal void HideInteract()
    {
        FObject.SetActive(false);
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
