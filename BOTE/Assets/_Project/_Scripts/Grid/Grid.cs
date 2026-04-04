using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

namespace CUHP
{
    public class IsometricGrid<TGridObject>
    {
        public EventHandler<OnGridObjectChangedEventArg> OnGridObjectChanged;
        public class OnGridObjectChangedEventArg : EventArgs {
            public int x, y;
        }

        private int width;
        private int height;
        private float cellSize;
        private Transform parent;
        private TGridObject[,] gridArray;
        private Vector3 originPosition;
        private TextMesh[,] debugTextArray;

        public IsometricGrid(int width, int height, float cellSize, Vector3 originPosition, Func<IsometricGrid<TGridObject>, int,int, TGridObject> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            gridArray = new TGridObject[width, height];
            debugTextArray = new TextMesh[width, height];
            this.originPosition = originPosition;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    gridArray[i, j] = createGridObject(this, i, j);
                }
            }
            bool showDebug = true;
            if (showDebug)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        debugTextArray[i, j] = UtilsClass.CreateWorldText(0.05f, gridArray[i, j]?.ToString(), null, GetWorldPositionOffset(i,j), 40, Color.white, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
                    }
                }
                Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            }
        }
        public Vector3 GetWorldPosition(int x,int y)
        {
            Vector3 gridPos = new Vector3(x, y);
            Vector3 isoPos = XYToIso(gridPos);
            return isoPos + originPosition;
        }
        public Vector3 GetWorldPositionOffset(int x, int y)
        {
            Vector3 gridPos = new Vector3(x, y);
            Vector3 isoPos = XYToIso(gridPos);
            return isoPos + originPosition + XYToIso(Vector3.one * 0.5f);
        }
        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            worldPosition = worldPosition - originPosition;
            Vector3 gridPos = IsoToXY(worldPosition);
            x = Mathf.FloorToInt(gridPos.x);
            y = Mathf.FloorToInt(gridPos.y);
        }
        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public float GetCellSize()
        {
            return cellSize;
        }
        public Vector3 GetCenter()
        {
            return GetWorldPosition(width/2,height/2);
        }
        public void SetGridObject(int x,int y,TGridObject value)
        {
            if(x>=0&&y>=0&&x<width && y < height)
            {
                gridArray[x, y] = value;
                debugTextArray[x,y].text = value.ToString();
                if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArg { x = x, y = y });
            }
        }
        public void TriggerObjectChanged(int x,int y)
        {
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArg {x = x, y = y});
        }
        public TGridObject GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default(TGridObject);
            }
        }
        public void SetGridObject(Vector2 worldPosition, TGridObject value)
        {
            int x, y;
            GetXY(worldPosition,out x,out y);
            SetGridObject(x,y,value);
        }
        public TGridObject GetGridObject(Vector2 worldPosition)
        {
            int x, y;
            
            GetXY(worldPosition, out x, out y);
            Debug.Log("GetGridObject: " + x + " " + y);
            return GetGridObject(x, y);
        }
        public Vector3 IsoToXY(Vector3 v)
        {
            float x = (v.x - 2f * v.y) / (2f * cellSize);
            float y = (v.x + 2f * v.y) / (2f * cellSize);
            return new Vector3(x, y, v.z);
        }
        public Vector3 XYToIso(Vector3 v)
        {
            float x = (v.x + v.y) * cellSize;
            float y = (v.y - v.x) * cellSize * 0.5f;
            return new Vector3(x, y, v.z);
        }
    }
}
