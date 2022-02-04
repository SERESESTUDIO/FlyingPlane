using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Player
{
    public class ChangePoint : ModeManager
    {
        public float distance;
        private GameObject player;
        [Header("Camera Settings")]
        public Vector3 cameraOffset;
        public float upRestriction;
        public float downRestriction;
        [Header("PlayerSettings")]
        public float upPlayerRestriction;
        public float downPlayerRestriction;
        public float speed;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
        }
        protected override void Play()
        {
            base.Play();
            if(player == null && GameObject.FindObjectOfType<PlayerLocomotion>())
            {
                player = GameObject.FindObjectOfType<PlayerLocomotion>().gameObject;
            } else if(player != null)
            {
                float _distance = (transform.position - player.transform.position).magnitude;
                if(_distance <= distance)
                {
                    Camera.main.GetComponent<CameraController>().saveX = cameraOffset.x;
                    Camera.main.GetComponent<CameraController>().saveY = cameraOffset.y;
                    Camera.main.GetComponent<CameraController>().upRestriction = upRestriction;
                    Camera.main.GetComponent<CameraController>().downRestriction = downRestriction;
                    player.GetComponent<PlayerLocomotion>().upRestriction = upPlayerRestriction;
                    player.GetComponent<PlayerLocomotion>().downRestriction = downPlayerRestriction;
                    player.GetComponent<PlayerLocomotion>().speed = speed;
                }
            }
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distance);
        }
    }
}
