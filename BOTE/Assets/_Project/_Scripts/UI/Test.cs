using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            UIManager.Instance.OpenUI<CanvasGamePlay>();
        }

        // Update is called once per frame
        void Update()
        {
           
        }
    }
}
