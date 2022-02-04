using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeresEstudio.Managers
{
    public class ModeManager : MonoBehaviour
    {
        private GameModes mode;
        public RutineManager rutine;
        private void Awake()
        {
            if(rutine == null)
            {
                rutine = GameObject.FindObjectOfType<RutineManager>();
            }else
            {
                mode = rutine.mode;
            }
        }
        private void Update()
        {
            if(rutine != null)
            {
                mode = rutine.mode;
            }
            switch(mode)
            {
                case GameModes.Start:
                    Start();
                    break;
                case GameModes.Cinematic:
                    Cinematic();
                    break;
                case GameModes.Play:
                    Play();
                    break;
                case GameModes.Pause:
                    Pause();
                    break;
                case GameModes.GameOver:
                    GameOver();
                    break;
            }
        }
        private void FixedUpdate()
        {
            switch(mode)
            {
                case GameModes.Start:
                    FixedStart();
                    break;
                case GameModes.Cinematic:
                    FixedCinematic();
                    break;
                case GameModes.Play:
                    FixedPlay();
                    break;
                case GameModes.Pause:
                    FixedPause();
                    break;
                case GameModes.GameOver:
                    FixedGameOver();
                    break;
            }
        }
        //UpdateModes
        protected virtual void Start() { }
        protected virtual void Cinematic() { }
        protected virtual void Play() { }
        protected virtual void Pause() { }
        protected virtual void GameOver() { }
        //FixeUpdateModes
        protected virtual void FixedStart() { }
        protected virtual void FixedCinematic() { }
        protected virtual void FixedPlay() { }
        protected virtual void FixedPause() { }
        protected virtual void FixedGameOver() { }
    }
}
public enum GameModes { Start, Cinematic, Play, Pause, GameOver }
