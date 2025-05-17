using System.Collections.Generic;
using Interfaces;
using TMPro;
using UnityEngine;

namespace UI.InGame
{
   public class InGameUIView : EscapePanel
   {
      [SerializeField] private TMP_Text scoreHUD, healthHUD;
      [SerializeField] private SpriteRenderer backgroundRenderer;
      [SerializeField] private List<Sprite> backgrounds;
      [SerializeField] private List<Transform> noteSystem;
      public static InGameUIView Instance;

      private void Awake()
      {
         Instance = this;
      }

      public int stageNum
      {
         set
         {
            var x = value % 2 == 0;
            backgroundRenderer.sprite = x ? backgrounds[0] : backgrounds[1];
            var y = x ? -1.11f : 0.08f;
            foreach (var tr in noteSystem)
            {
               tr.position = new Vector3(tr.position.x, y, 0);
            }
         }
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
