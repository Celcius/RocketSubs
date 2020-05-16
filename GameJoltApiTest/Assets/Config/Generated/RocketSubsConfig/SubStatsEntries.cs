using UnityEngine;
using System;
using System.Collections;


// !!!!! WARNING !!!!!
// Auto Generated Class --- DO NOT CHANGE ---


namespace Config {
public class SubStatsEntries : ScriptableObject {
 

 public SubStatsEntry[] entries;

 private void Populate(Array array) {
  entries= new SubStatsEntry[array.Length];
  for(int i = 0; i < array.Length;i++)
  {
   entries[i] = (SubStatsEntry)array.GetValue(i);
  }
 }

 }
}
