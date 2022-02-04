using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Obstacles
{
    public class MobilityManager : ModeManager
    {
        public float startTimer;
        public float delayTimer;
        public float speed;
        public float smoothRotation;
        public float detectionDistance;
        public GameObject[] mobiles;
        public GameObject[] points;
        public float timer;
        private GameObject tarjet;
        [HideInInspector]
        public List<GameObject> Objets;
        [HideInInspector]
        public int index;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
        }
        protected override void Start()
        {
            base.Start();
            timer = startTimer;
            if(Objets.Count != 0)
            {
                foreach(GameObject obj in Objets)
                {
                    Destroy(obj);
                }
                Objets.Clear();
            }
        }
        protected override void Play()
        {
            base.Play();
            if(timer < delayTimer)
            {
                timer += Time.deltaTime;
            } else
            {
                index = Random.Range(0, mobiles.Length);
                mobiles[index].GetComponent<MobilityObject>().mobilityManager = this;
                tarjet = Instantiate(mobiles[index]);
                tarjet.transform.SetParent(this.transform);
                tarjet.transform.localPosition = Vector3.zero;
                tarjet.transform.localEulerAngles = Vector3.zero;
                tarjet.GetComponent<MobilityObject>().points = points;
                tarjet.GetComponent<MobilityObject>().speed = speed;
                tarjet.GetComponent<MobilityObject>().smoothRotation = smoothRotation;
                tarjet.GetComponent<MobilityObject>().detectionDistance = detectionDistance;
                timer = 0;
            }
        }
    }
}
