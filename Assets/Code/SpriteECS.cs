using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteECS : MonoBehaviour
{
    public float[] Frames;
    public int index;
    public Material material;



    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            material.SetTextureOffset("_MainTex", new Vector2(Frames[index], 0));
            index++;
            if (index >= Frames.Length)
            {
                index = 0;
            }
        }
    }
}
