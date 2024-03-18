using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class Sort : MonoBehaviour
    {
        public static void BubbleSort(List<int> arr)
        {
            int n = arr.Count;
            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
 
                
                if (swapped == false)
                    break;
            }
        }

        private void Start()
        {
            List<int> list = new List<int>(5);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);
            BubbleSort(list);
        }
    }
    
}