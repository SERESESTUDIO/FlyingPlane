using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Player
{
    public class CameraController : ModeManager
    {
        public GameObject player;
        public float initialSmooth;
        public float smoothCamera;
        public float smoothDirection;
        public Vector3 offset;
        public float upRestriction;
        public float downRestriction;
        [HideInInspector]
        public float saveX;
        [HideInInspector]
        public float saveY;
        private Vector3 initialPos;
        private float saveinitalSmooth;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            saveX = offset.x;
            saveY = offset.y;
            initialPos = transform.position;
            saveinitalSmooth = initialSmooth;
        }
        protected override void FixedPlay()
        {
            base.FixedPlay();
            initialSmooth = saveinitalSmooth;
            ActionPlay(smoothCamera);
        }
        protected override void FixedStart()
        {
            base.FixedStart();
            //transform.position = initialPos;
            ActionPlay(initialSmooth);
        }
        public void ActionPlay(float smooth)
        {
            if (player == null && GameObject.FindObjectOfType<PlayerLocomotion>())
            {
                player = GameObject.FindObjectOfType<PlayerLocomotion>().gameObject;
            }
            else if (player != null)
            {
                if (player.GetComponent<PlayerLocomotion>().front)
                {
                    offset.x = Mathf.Lerp(offset.x, saveX, smoothDirection * Time.deltaTime);
                }
                else
                {
                    offset.x = Mathf.Lerp(offset.x, -saveX, smoothDirection * Time.deltaTime);
                }
                if(player.GetComponent<PlayerLocomotion>().rot == 1)
                {
                    offset.y = Mathf.Lerp(offset.y, saveY, smoothDirection * Time.deltaTime);
                } else if(player.GetComponent<PlayerLocomotion>().rot == -1)
                {
                    offset.y = Mathf.Lerp(offset.y, -saveY, smoothDirection * Time.deltaTime);
                }
                Vector3 tarjet = player.transform.position + offset;
                if(tarjet.x <= initialPos.x)
                {
                    tarjet.x = initialPos.x;
                }
                if (tarjet.y >= upRestriction)
                {
                    tarjet.y = upRestriction;
                }
                if (tarjet.y <= downRestriction)
                {
                    tarjet.y = downRestriction;
                }
                tarjet.z = transform.position.z;
                transform.position = Vector3.Lerp(transform.position, tarjet, smooth * Time.fixedDeltaTime);
            }
        }
        public void SetSmooth(float value)
        {
            initialSmooth = value;
        }
    }
}
