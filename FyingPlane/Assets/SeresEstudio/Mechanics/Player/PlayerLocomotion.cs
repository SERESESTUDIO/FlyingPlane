using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
using FMODUnity;
using FMOD.Studio;
using SeresEstudio.Managers.GameFlow;
namespace SeresEstudio.Mechanics.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerLocomotion : ModeManager
    {
        public bool front;
        public int rot;
        public float GravityScale;
        public float speed;
        public float smoothRotation;
        public float smoothRotationStatic;
        public float downRotation;
        public string eventName;
        public string CrashParameterName;
        public string VolunmeName;

        public GameObject plane;
        public Animator anim;

        public float upRestriction;
        public float downRestriction;
        public float timerDead;

        private InputDrawing inputDrawing;
        private Rigidbody rBody;
        private Vector3 direction;
        private float saveX;
        private float saveY;
        private GameObject tarjectLook;
        private bool saveDir;
        private bool crash;
        private bool enterOnes;
        private float timer;
        private EventInstance playerEvent;
        private bool startEvent;
        private float volume;
        private float initialTimer;
        private float initialTimerStart;
        private bool allowCrash;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            rBody = GetComponent<Rigidbody>();
            rBody.useGravity = false;
            if(inputDrawing == null)
            {
                inputDrawing = GameObject.FindObjectOfType<InputDrawing>();
            }
            tarjectLook = new GameObject();
            //tarjectLook.transform.SetParent(this.transform);
            tarjectLook.transform.localScale = Vector3.one;
            tarjectLook.transform.localEulerAngles = new Vector3(0,90,0);
            direction = new Vector3(1, 0, 0);
            plane.GetComponent<Renderer>().enabled = false;
        }
        protected override void FixedPlay()
        {
            base.FixedPlay();
            if (initialTimerStart < 2)
            {
                initialTimerStart += Time.deltaTime;
            } else
            {
                allowCrash = true;
                initialTimerStart = 2;
            }
            if(!startEvent)
            {
                volume = 1;
                playerEvent = RuntimeManager.CreateInstance("event:/" + eventName);
                playerEvent.setParameterByName(CrashParameterName, 0);
                playerEvent.setParameterByName(VolunmeName, 1f);
                playerEvent.start();
                playerEvent.release();
                startEvent = true;
            }
            plane.GetComponent<Renderer>().enabled = true;
            anim.enabled = true;
            tarjectLook.transform.position = transform.position;
            if (inputDrawing != null && !crash)
            {
                float smoothRot;
                if (inputDrawing.pointsVectors.Count != 0)
                {
                    smoothRot = smoothRotation;
                    volume = Mathf.Lerp(volume, 1f, 5 * Time.deltaTime);
                    direction = (inputDrawing.pointsVectors[0] - transform.position).normalized;
                    tarjectLook.transform.LookAt(inputDrawing.pointsVectors[0]);
                } else
                {
                    if (initialTimer < 0.5f)
                    {
                        initialTimer += Time.deltaTime;
                    }
                    else
                    {
                        volume = Mathf.Lerp(volume, 0.7f, 5 * Time.deltaTime);
                    }
                    smoothRot = smoothRotationStatic;
                    if (direction.y > -GravityScale)
                    {
                        direction.y -= GravityScale * Time.deltaTime;
                    }
                    float rotDir;
                    if(front)
                    {
                        rotDir = 90;
                    } else
                    {
                        rotDir = -90;
                    }
                    if(rot == 1)
                    {
                        tarjectLook.transform.localEulerAngles = new Vector3(-downRotation, rotDir, 0);
                    } else if(rot == 0)
                    {
                        tarjectLook.transform.localEulerAngles = new Vector3(0, rotDir, 0);
                    } else if(rot == -1)
                    {
                        tarjectLook.transform.localEulerAngles = new Vector3(downRotation, rotDir, 0);
                    }
                }
                transform.Translate(direction * speed * Time.fixedDeltaTime, Space.World);
                transform.rotation = Quaternion.Lerp(transform.rotation, tarjectLook.transform.rotation, smoothRot * Time.fixedDeltaTime);
                playerEvent.setParameterByName(VolunmeName, volume);
                if (transform.position.x > saveX)
                {
                    front = true;
                }
                else if (transform.position.x == saveX)
                {
                    front = true;
                }
                else
                {
                    front = false;
                }
                if (saveDir != front)
                {
                    anim.SetTrigger("Turn");
                    saveDir = front;
                }
                if (transform.position.y > saveY)
                {
                    rot = 1;
                    anim.SetBool("Up", true);
                }
                else if (transform.position.y == saveY)
                {
                    rot = 0;
                }
                else
                {
                    rot = -1;
                    anim.SetBool("Up", false);
                }
                saveY = transform.position.y;
                saveX = transform.position.x;
            } else if(crash)
            {
                playerEvent.setParameterByName(CrashParameterName, 1);
                playerEvent.setParameterByName(VolunmeName, 1f);
                rBody.useGravity = true;
                GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, GetComponent<CapsuleCollider>().center.y, 1);
                GetComponent<CapsuleCollider>().height = 1;
                if (!enterOnes)
                {
                    rBody.velocity = Vector3.zero;
                    anim.SetTrigger("Crash");
                    enterOnes = true;
                }
                rBody.useGravity = true;
                if (timer < timerDead)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    Destroy(tarjectLook);
                    Destroy(gameObject);
                    rutine.unfreze = true;
                }
            }
            if(transform.position.y >= upRestriction && allowCrash || transform.position.y <= downRestriction && allowCrash)
            {
                crash = true;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "puch" && allowCrash)
            {
                crash = true;
            }
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag != "puch")
            {
                crash = true;
            }
        }
    }
}
