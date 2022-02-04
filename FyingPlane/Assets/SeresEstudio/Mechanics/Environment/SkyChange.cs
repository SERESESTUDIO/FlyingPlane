using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace SeresEstudio.Mechanics.Environment
{
    public class SkyChange : MonoBehaviour
    {
        public int index;
        public float smoothTransition;
        public ColorsBlock[] blocks;
        private Material material;
        private Color TopColor;
        private Color MiddleColor;
        private Color DownColor;
        private void Awake()
        {
            material = GetComponent<Renderer>().material;
            TopColor = blocks[0].TopColor;
            MiddleColor = blocks[0].MiddleColor;
            DownColor = blocks[0].DownColor;
        }
        private void Update()
        {
            TopColor = Color.Lerp(TopColor, blocks[index].TopColor, smoothTransition * Time.deltaTime);
            MiddleColor = Color.Lerp(MiddleColor, blocks[index].MiddleColor, smoothTransition * Time.deltaTime);
            DownColor = Color.Lerp(DownColor, blocks[index].DownColor, smoothTransition * Time.deltaTime);
            material.SetColor("Color_C132E98E", TopColor);
            material.SetColor("Color_7F5C012F", MiddleColor);
            material.SetColor("Color_6D4F8623", DownColor);
        }
    }
    [Serializable]
    public class ColorsBlock
    {
        public string _name;
        public Color TopColor;
        public Color MiddleColor;
        public Color DownColor;
    }
}
