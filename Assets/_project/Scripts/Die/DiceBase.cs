using System;
using System.Collections.Generic;
using _project.Scripts.Effects;
using _project.Scripts.ScriptableObjects.Dice;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;


namespace _project.Scripts.Die
{
    public abstract class DiceBase : MonoBehaviour
    {
        [FormerlySerializedAs("_diceData"),SerializeField, InfoBox("0, 1, 2, 3, 4, 5 => down, back, right, up, left, front ")] protected DiceDataScriptableObject diceData;
        [FormerlySerializedAs("_facesValues"),SerializeField] protected List<int> facesValues = new(6) { 1, 2, 3, 4, 5, 6 };
        [FormerlySerializedAs("_faceIdMap"),SerializeField] protected SerializedDictionary<GameObject, int> faceIdMap = new();
        [SerializeField] protected ScoreEffectScript scoreEffectScript;
        [CanBeNull] private AudioManager _audioManager;

        [SerializeField] private float _soundCooldown = 0.4f;
        private float _lastSoundTime;

        private void Awake()
        {
            _audioManager = FindFirstObjectByType<AudioManager>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!(Time.time - _lastSoundTime > _soundCooldown)) return;
            if (other.gameObject.tag != "Floor") return;

            _lastSoundTime = Time.time;
            _audioManager?.DiceSoundPlayer.PlayRandomSound();
        }

        private void Reset()
        {
            foreach (Transform t in transform)
            {
                string direction = t.name.Remove(0, "AnchorPoint ".Length);
                switch (direction)
                {
                    case "Forward":
                        faceIdMap[t.gameObject] = 5;
                        break;
                    case "Backward":
                        faceIdMap[t.gameObject] = 1;
                        break;
                    case "Left":
                        faceIdMap[t.gameObject] = 4;
                        break;
                    case "Right":
                        faceIdMap[t.gameObject] = 2;
                        break;
                    case "Up":
                        faceIdMap[t.gameObject] = 3;
                        break;
                    case "Down":
                        faceIdMap[t.gameObject] = 0;
                        break;
                }
            }
        }

        public int GetUpFaceValue()
        {
            float highestY = float.MinValue;
            int faceIndex = -1;
            foreach ((GameObject anchor, int i) in faceIdMap)
            {
                if (highestY < anchor.transform.position.y)
                {
                    highestY = anchor.transform.position.y;
                    faceIndex = i;
                }
            }

            if (faceIndex == -1)
            {
                throw new  Exception("No face found");
            }
        
            return facesValues[faceIndex];
        }

        protected void SpawnScoreEffect(int score)
        {
            ScoreEffectScript effectScript = Instantiate(scoreEffectScript, transform.position + Vector3.up, Quaternion.identity);
            effectScript.Setup(score);
        }

        public abstract Awaitable ApplyEffect(GameState gamestate);

        private void OnDrawGizmosSelected()
        {
            // TODO: Draw labels for directions
        }
    }
}