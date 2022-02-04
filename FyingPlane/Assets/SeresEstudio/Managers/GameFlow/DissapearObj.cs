using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Managers.GameFlow
{
    public class DissapearObj : ModeManager
    {
        public float timeDessappear;
        private float timer;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
        }
        protected override void Start()
        {
            base.Start();
            timer = 0;
            GetComponent<Renderer>().enabled = true;
        }
        protected override void Cinematic()
        {
            base.Cinematic();
            if(timer < timeDessappear)
            {
                timer += Time.deltaTime;
            } else
            {
                GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
