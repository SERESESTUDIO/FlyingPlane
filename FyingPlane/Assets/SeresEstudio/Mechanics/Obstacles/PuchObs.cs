using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
using SeresEstudio.Mechanics.Player;
using FMOD.Studio;
using FMODUnity;
namespace SeresEstudio.Mechanics.Obstacles
{
    public class PuchObs : ModeManager
    {
        public StudioEventEmitter eventEmitter;
        public ParticleSystem particle;
        public float startTime;
        public float delayTime;
        public float stopTime;
        public float adviceTime;
        private GameObject player;
        public float speed;
        public Vector3 direction;
        public float timer;
        private bool action;
        public string parameter;
        private bool enterOnes;
        private float advice;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
        }
        protected override void Start()
        {
            base.Start();
            timer = startTime;
        }
        protected override void FixedPlay()
        {
            base.FixedPlay();
            timer += Time.deltaTime;
            if(timer >= delayTime && timer < stopTime)
            {
                action = true;
            } else if(timer >= stopTime)
            {
                timer = 0;
                action = false;
            } else if (timer < delayTime)
            {
                action = false;
            }
            if (action)
            {
                advice += Time.deltaTime;
                if (!enterOnes)
                {
                    eventEmitter.Play();
                    eventEmitter.SetParameter(parameter, 0);
                    enterOnes = true;
                }
                if (particle != null)
                {
                    particle.Play();
                }
                if (player != null && advice >= adviceTime)
                {
                    player.transform.Translate(direction * speed * Time.deltaTime, Space.World);
                }
            } else
            {
                advice = 0;
                eventEmitter.SetParameter(parameter, 1);
                enterOnes = false;
                if (particle != null)
                {
                    particle.Stop();
                }
            }
        }
        private void OnTriggerEnter(Collider collider)
        {
            if(collider.GetComponent<PlayerLocomotion>())
            {
                player = collider.gameObject;
            }
        }
        private void OnTriggerExit(Collider collider)
        {
            if (collider.GetComponent<PlayerLocomotion>())
            {
                player = null;
            }
        }
    }
}
