using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SeresEstudio.Managers;
using FMOD.Studio;
using FMODUnity;
namespace SeresEstudio.UI.Menu
{
    public class ActionButtons : MonoBehaviour
    {
        public GameObject mark;
        public GameObject line;
        public Color normal;
        public Color hover;
        public Color selected;
        public Animator anim;
        public string animActionName;
        public GameObject myCanvas;
        public GameObject tarjetCanvas;
        public float timeToChange;
        public Image alternativeImage;
        private float markPosition;
        private RutineManager rutine;
        private float timer;
        public bool change;
        public string eventName;
        public string paramName;
        private EventInstance menuEvent;
        [HideInInspector]
        public bool Blocked;
        private void Awake()
        {
            rutine = GameObject.FindObjectOfType<RutineManager>();
            markPosition = GetComponent<RectTransform>().anchoredPosition.y;
            if(line != null)
                line.SetActive(false);
            if (alternativeImage == null)
            {
                GetComponent<Image>().color = normal;
            } else
            {
                alternativeImage.color = normal;
            }
        }
        private void Update()
        {
            if(change)
            {
                if(timer < timeToChange)
                {
                    timer += Time.deltaTime;
                } else
                {
                    timer = 0;
                    change = false;
                    if (anim != null)
                    {
                        myCanvas.SetActive(false);
                        tarjetCanvas.SetActive(true);
                        anim.SetBool(animActionName, false);
                    }
                }
            }
        }
        public void EnterButton()
        {
            ActionButtons[] buttons = GameObject.FindObjectsOfType<ActionButtons>();
            Blocked = false;
            foreach (ActionButtons but in buttons)
            {
                if(but.change)
                {
                    Blocked = true;
                }
            }
            if (!Blocked)
            {
                menuEvent = RuntimeManager.CreateInstance("event:/" + eventName);
                menuEvent.setParameterByName(paramName, 0);
                menuEvent.start();
                menuEvent.release();
                if (alternativeImage == null)
                {
                    GetComponent<Image>().color = hover;
                }
                else
                {
                    alternativeImage.color = hover;
                }
                if (line != null)
                    line.SetActive(true);
                RectTransform markTransform = mark.GetComponent<RectTransform>();
                markTransform.anchoredPosition = new Vector2(markTransform.anchoredPosition.x, markPosition);
            }
        }
        public void ExitButton()
        {
            if (alternativeImage == null)
            {
                GetComponent<Image>().color = normal;
            }
            else
            {
                alternativeImage.color = normal;
            }
            if (line != null)
                line.SetActive(false);
        }
        public void ClickButton()
        {
            menuEvent = RuntimeManager.CreateInstance("event:/" + eventName);
            menuEvent.setParameterByName(paramName, 1);
            menuEvent.start();
            menuEvent.release();
            if (alternativeImage == null)
            {
                GetComponent<Image>().color = selected;
            }
            else
            {
                alternativeImage.color = selected;
            }
        }
        public void UpButton()
        {
            if (alternativeImage == null)
            {
                GetComponent<Image>().color = hover;
            }
            else
            {
                alternativeImage.color = hover;
            }
        }
        public void OnPlay()
        {
            if(anim != null)
            {
                anim.SetBool(animActionName, true);
                rutine.resume = true;
            }
        }
        public void ChangeCanvas()
        {
            change = true;
            if (anim != null)
            {
                anim.SetBool(animActionName, true);
            }
        }
        bool IsPlaying(FMOD.Studio.EventInstance instance)
        {
            FMOD.Studio.PLAYBACK_STATE state;
            instance.getPlaybackState(out state);
            return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
        }
    }
}
