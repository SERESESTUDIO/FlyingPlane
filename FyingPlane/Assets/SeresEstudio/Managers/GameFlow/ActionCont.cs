using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SeresEstudio.Managers.GameFlow
{
    public class ActionCont : MonoBehaviour
    {
        public GameObject button;
        public InputField inputField;
        public int actualPass;
        private void Update()
        {
            if (inputField.text.Length == 5)
            {
                int pass = 0;
                int.TryParse(inputField.text, out pass);
                PlayerInstanciated[] players = GameObject.FindObjectsOfType<PlayerInstanciated>();
                bool result = false;
                foreach(PlayerInstanciated pl in players)
                {
                    if(pl.indexModule == pass)
                    {
                        result = true;
                        break;
                    }
                }
                if (result)
                {
                    actualPass = pass;
                    button.SetActive(true);
                } else
                {
                    button.SetActive(false);
                    inputField.text = "";
                }
            } else
            {
                button.SetActive(false);
            }
        }
        public void Reset()
        {
            actualPass = 0;
            button.SetActive(false);
        }
    }
}
