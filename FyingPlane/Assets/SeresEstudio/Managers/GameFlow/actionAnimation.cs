using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Managers.GameFlow
{
    public class actionAnimation : ModeManager
    {
        public string animationName;
        private Animator anim;
        private bool enterOnes;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            anim = GetComponent<Animator>();
        }
        protected override void Cinematic()
        {
            base.Cinematic();
            if(!enterOnes)
            {
                anim.SetTrigger(animationName);
                enterOnes = true;
            }
        }
        protected override void Start()
        {
            base.Start();
            enterOnes = false;
        }
    }
}
