using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Obstacles
{
    public class MobilityObject : ModeManager
    {
        [HideInInspector]
        public MobilityManager mobilityManager;
        [HideInInspector]
        public GameObject[] points;
        [HideInInspector]
        public float speed;
        [HideInInspector]
        public float smoothRotation;
        [HideInInspector]
        public float detectionDistance;
        private int index;
        private float distance;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            index = 0;
            mobilityManager.Objets.Add(gameObject);
        }
        protected override void FixedPlay()
        {
            base.FixedPlay();
            transform.rotation = Quaternion.Lerp(transform.rotation, points[index].transform.rotation, smoothRotation * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            distance = (transform.position - points[index].transform.position).magnitude;
            if(distance <= detectionDistance)
            {
                index++;
                if (index >= points.Length)
                {
                    mobilityManager.Objets.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
