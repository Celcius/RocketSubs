using UnityEngine;
using System;
using System.Collections;


// !!!!! WARNING !!!!!
// Auto Generated Class --- DO NOT CHANGE ---


namespace Config {
public class SubGeneralStatsEntries : ScriptableObject {
 

 public SubGeneralStatsEntry[] entries;

 private void Populate(Array array) {
  entries= new SubGeneralStatsEntry[array.Length];
  for(int i = 0; i < array.Length;i++)
  {
   entries[i] = (SubGeneralStatsEntry)array.GetValue(i);
  }
 }

 }
}
