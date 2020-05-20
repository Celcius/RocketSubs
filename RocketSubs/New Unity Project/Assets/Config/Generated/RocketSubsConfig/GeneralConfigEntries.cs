using UnityEngine;
using System;
using System.Collections;


// !!!!! WARNING !!!!!
// Auto Generated Class --- DO NOT CHANGE ---


namespace Config {
public class GeneralConfigEntries : ScriptableObject {
 

 public GeneralConfigEntry[] entries;

 private void Populate(Array array) {
  entries= new GeneralConfigEntry[array.Length];
  for(int i = 0; i < array.Length;i++)
  {
   entries[i] = (GeneralConfigEntry)array.GetValue(i);
  }
 }

 }
}
