using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Player
{
    public class Point : ModeManager
    {
        public GameObject player;
        [HideInInspector]
        public InputDrawing inputDraw;
        [HideInInspector]
        public float timerOut;
        private float timer;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            inputDraw.points.Add(this.gameObject);
        }
        protected override void Play()
        {
            base.Play();
            if(player == null && GameObject.FindObjectOfType<PlayerLocomotion>())
            {
                player = GameObject.FindObjectOfType<PlayerLocomotion>().gameObject;
            } else if(player != null)
            {
                float distance = (transform.position - player.transform.position).magnitude;
                if(distance <= inputDraw.maxDistance)
                {
                    DestroyObj();
                }
            }
            if (timer < timerOut)
            {
                timer += Time.deltaTime;
            } else
            {
                DestroyObj();
            }
        }
        public void DestroyObj()
        {
            inputDraw.points.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
