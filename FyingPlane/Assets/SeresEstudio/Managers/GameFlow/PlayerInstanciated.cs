using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Mechanics.Player;
using SeresEstudio.Managers;
using UnityEngine.UI;
namespace SeresEstudio.Managers.GameFlow
{
    public class PlayerInstanciated : ModeManager
    {
        public float upSmooth;
        public float downSmooth;
        public float distance;
        public float timerOut;
        public GameObject player;
        public GameObject checkPoint;
        public Text CheckPointText;
        public int indexModule;
        public ActionCont actionCont;
        private GameObject actionPlayer;
        private bool inisialization;
        private GameObject playerOnAction;
        private float timer;
        private bool myInst;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
        }
        protected override void Pause()
        {
            base.Pause();
            inisialization = true;
        }
        protected override void Start()
        {
            base.Start();
            if (indexModule == actionCont.actualPass && inisialization)
            {
                Reset();
            }
        }
        protected override void Play()
        {
            base.Play();
            if(playerOnAction == null && GameObject.FindObjectOfType<PlayerLocomotion>())
            {
                playerOnAction = GameObject.FindObjectOfType<PlayerLocomotion>().gameObject;
            } else if(playerOnAction != null)
            {
                float activeDistance = (transform.position - playerOnAction.transform.position).magnitude;
                if(activeDistance <= distance)
                {
                    actionCont.actualPass = indexModule;
                    checkPoint.SetActive(true);
                    CheckPointText.GetComponent<Text>().text = "Check Point: " + indexModule.ToString();
                    myInst = true;
                }
            }
            if(myInst && timer >= timerOut)
            {
                checkPoint.SetActive(false);
                myInst = false;
                timer = 0;
            } else if(myInst && timer < timerOut)
            {
                timer += Time.deltaTime;
            }
        }
        void Reset()
        {
            Camera.main.GetComponent<CameraController>().upRestriction = upSmooth;
            Camera.main.GetComponent<CameraController>().downRestriction = downSmooth;
            if (actionPlayer == null)
            {
                if (GameObject.FindObjectOfType<PlayerLocomotion>() && actionPlayer == null)
                {
                    actionPlayer = GameObject.FindObjectOfType<PlayerLocomotion>().gameObject;
                }
                else if (!GameObject.FindObjectOfType<PlayerLocomotion>())
                {
                    player.transform.position = transform.position;
                    actionPlayer = Instantiate(player);
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
