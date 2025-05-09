using Interfaces;
using TMPro;
using UnityEngine;

namespace UI.InGame
{
   public class InGameUIView : EscapePanel
   {
      [SerializeField] private TMP_Text scoreHUD, healthHUD;
      public static InGameUIView Instance;

      private void Awake()
      {
         Instance = this;
      }
      
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
