using TMPro;
using UnityEngine;

namespace UI.InGame
{
   public class InGameUIView : MonoBehaviour
   {
      [SerializeField] private TMP_Text scoreHUD, healthHUD;
   
      public int score
      {
         set => scoreHUD.text = value.ToString();
      }
      
      public int health
      {
         set => healthHUD.text = value.ToString();
      }
   }
}
