﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public static Inventory instance;

   public delegate void OnItemChanged();
   public OnItemChanged onItemChangedCallback;
   
   void Awake()
   {
      instance = this;
   }

   public List<Item> items = new List<Item>();

   public void Add (Item item)
   {
        items.Add(item);
        if(onItemChangedCallback != null)
           onItemChangedCallback.Invoke();
   }
   
   public void Remove (Item item)
   {
      items.Remove(item);
      if(onItemChangedCallback != null)
         onItemChangedCallback.Invoke();
   }
}
