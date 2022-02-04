using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SeresEstudio.UI.Menu
{
    public class WordAsignation : MonoBehaviour
    {
        public InputField inputField;
        public Image[] wordsCanvas;
        public Sprite[] numbInput;
        [HideInInspector]
        public List<int> numbers;
        private int saveCount;
        private void Awake()
        {
            foreach(Image im in wordsCanvas)
            {
                im.enabled = false;
            }
        }
        private void Update()
        {
            if(saveCount != inputField.text.Length)
            {
                numbers.Clear();
                for(int i = 0; i < inputField.text.Length; i++)
                {
                    int value;
                    int.TryParse(inputField.text[i].ToString(), out value);
                    numbers.Add(value);
                }
                for(int i = 0; i < wordsCanvas.Length; i++)
                {
                    if(i < inputField.text.Length)
                    {
                        wordsCanvas[i].enabled = true;
                        wordsCanvas[i].sprite = numbInput[numbers[i]];
                    } else
                    {
                        wordsCanvas[i].enabled = false;
                    }
                }
                saveCount = inputField.text.Length;
            }
        }
    }
}