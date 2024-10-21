using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
 public class Obtainable : MonoBehaviour
 {
        public string itemName;//�C���X�y�N�^�[�ɃA�C�e���������
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
    internal void Obtain(GameObject item)//Ray���E�N���b�N�ł���
     {
        gameObj = item;
        Inventory.GetInstance().Obtain(this);
            //ItemManager�N���X��Obtain���\�b�h���Ă�
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
