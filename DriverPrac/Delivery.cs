using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{

  bool hasPackage = false;
  [SerializeField] float destroyDelay=0.5f;
  [SerializeField] Color32 hasPackageColor = new Color32(255, 255, 255, 255);
  [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);

  SpriteRenderer spriteRenderer;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

   private void OnCollisionEnter2D(Collision2D other)
   {
     Debug.Log("Hi");
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if(other.tag == "Package" && !hasPackage)
      {
        Debug.Log("Pick Up!");
        hasPackage=true;
        spriteRenderer.color=hasPackageColor;
        Destroy(other.gameObject ,destroyDelay );
      }
      else if(other.tag=="Customer"&& hasPackage)
      {
        Debug.Log("Here!");
        spriteRenderer.color=noPackageColor;
        hasPackage=false;
      }
   }
}
