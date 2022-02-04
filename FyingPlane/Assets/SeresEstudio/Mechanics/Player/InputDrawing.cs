using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Managers;
namespace SeresEstudio.Mechanics.Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class InputDrawing : ModeManager
    {
        public Camera _camera;
        public float timerOut;
        public float maxDistance;
        public GameObject point;
        [HideInInspector]
        public List<GameObject> points;
        [HideInInspector]
        public List<Vector3> pointsVectors;

        private Vector3 inputAxis;
        private LineRenderer line;
        private int saveIndex;
        private GameObject savePoint;
        private bool enterOnes;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            line = GetComponent<LineRenderer>();
        }
        protected override void Play()
        {
            base.Play();
            inputAxis = _camera.ScreenToWorldPoint(Input.mousePosition);
            inputAxis.z = 0;
            if(Input.GetMouseButton(0))
            {
                if(!enterOnes)
                {
                    Reset();
                    enterOnes = true;
                }
                point.transform.position = inputAxis;
                point.GetComponent<Point>().inputDraw = this;
                point.GetComponent<Point>().timerOut = timerOut;
                savePoint = Instantiate(point);
                savePoint.transform.SetParent(this.transform);
            } else
            {
                enterOnes = false;
            }
            if(saveIndex != points.Count)
            {
                pointsVectors.Clear();
                foreach(GameObject obj in points)
                {
                    pointsVectors.Add(obj.transform.position);
                }
                saveIndex = points.Count;
            }
            line.positionCount = pointsVectors.Count;
            line.SetPositions(pointsVectors.ToArray());
        }
        protected override void GameOver()
        {
            base.GameOver();
            Reset();
        }
        public void Reset()
        {
            foreach(GameObject obj in points)
            {
                Destroy(obj);
            }
            points.Clear();
            pointsVectors.Clear();
            line.positionCount = pointsVectors.Count;
            line.SetPositions(pointsVectors.ToArray());
        }
    }
}
