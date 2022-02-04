using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Obstacles
{
    public class wheel : ModeManager
    {
        public Vector3 vectorRot;
        public float speed;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
        }
        protected override void Play()
        {
            base.Play();
            transform.Rotate(vectorRot * speed * 100 * Time.deltaTime, Space.Self);
        }
    }
}
