using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Test : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UIManager.Instance.OpenUI<CanvasInventory>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
