using UnityEngine;
using System.Collections;

public class PassArrayToShader : MonoBehaviour {

    public static void Color(Material material, string name, Color[] array)
    {
        for (int i = 0; i < array.Length; i++)
            material.SetColor(name + i.ToString(), array[i]);
    }
}
