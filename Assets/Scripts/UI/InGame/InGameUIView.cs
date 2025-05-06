using TMPro;
using UnityEngine;

namespace UI.InGame
{
   public class InGameUIView : MonoBehaviour
   {
      [SerializeField] private TMP_Text scoreHUD, healthHUD, stageTimeHUD;
   
      public int score
      {
         set => scoreHUD.text = value.ToString();
      }
      
      public int health
      {
         set => healthHUD.text = value.ToString();
      }
   
      public float stageTime
      {
         set => stageTimeHUD.text = value.ToString();
      }

   }
}
