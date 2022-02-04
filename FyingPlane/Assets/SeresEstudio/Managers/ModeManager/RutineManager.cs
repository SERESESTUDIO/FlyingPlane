using System;
using UnityEngine;
namespace SeresEstudio.Managers
{
    public class RutineManager : ModeManager
    {
        [Header("Monitor")]
        public GameModes mode;
        public int indexModule;
        public float timer;
        public bool manual;
        public bool resume;
        public bool unfreze;
        [Header("Settings")]
        public bool loop;
        public ModeModule[] module;
        private GameModes saveMode;
        private int saveIndex;
        private bool saveFreze;
        private void Awake()
        {
            indexModule = 0;
            timer = 0;
            manual = false;
            resume = false;
            unfreze = false;
        }
        private void Update()
        {
            if (module != null && module.Length != 0)
            {
                if (mode != GameModes.Pause && !manual)
                {
                    resume = false;
                    mode = module[indexModule].selectedMode;
                    if (!module[indexModule].frezeTime)
                    {
                        timer += 1 * Time.deltaTime;
                    }
                    if (timer >= module[indexModule].time)
                    {
                        timer = 0;
                        module[indexModule].frezeTime = saveFreze;
                        indexModule++;
                        unfreze = false;
                    }
                    if (indexModule >= module.Length && !loop)
                    {
                        indexModule = module.Length - 1;
                    }
                    else if (indexModule < 0 && !loop)
                    {
                        indexModule = 0;
                    }
                    else if (indexModule >= module.Length && loop)
                    {
                        indexModule = 0;
                    }
                    saveMode = mode;
                    if (unfreze)
                    {
                        module[indexModule].frezeTime = false;
                    } else
                    {
                        saveFreze = module[indexModule].frezeTime;
                    }
                }
                else
                {
                    if (resume)
                    {
                        mode = saveMode;
                        resume = false;
                    }
                }
            }
        }
    }
    [Serializable]
    public class ModeModule
    {
        public string moduleName;
        public GameModes selectedMode;
        public float time;
        public bool frezeTime;
    }
}
