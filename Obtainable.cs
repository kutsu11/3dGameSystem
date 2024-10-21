using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class Obtainable : MonoBehaviour
 {
        public string itemName;//インスペクターにアイテム名を入力
        public int itemId;
    GameObject gameObj;

        public Raycast_Mouseover raycast_Mouseover;

    void Update()
    {
     if(raycast_Mouseover.outlineinfo)
        {
            this.gameObject.layer = 0;
        }
    }
    internal void Obtain(GameObject item)//Ray中右クリックでこれ
     {
        gameObj = item;
        Inventory.GetInstance().Obtain(this);
            //ItemManagerクラスのObtainメソッドを呼ぶ
     }
       internal string GetItemName()
      {
            return itemName;
       }

      public int GetitemId()
      {
          return itemId;
      }
      internal GameObject GetGameObject()
      {
          return gameObject;
      }
 }
