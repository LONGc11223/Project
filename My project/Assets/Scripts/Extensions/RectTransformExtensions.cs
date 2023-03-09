using UnityEngine;

public class RectTransformExtensions
{
    public static void SetLeft(RectTransform rt, float left)
     {
         rt.offsetMin = new Vector2(left, rt.offsetMin.y);
     }
 
     public static void SetRight(RectTransform rt, float right)
     {
         rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
     }
}
