using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using UnityEngine;
using Object = UnityEngine.Object;


namespace _project.Scripts.Die
{
    public abstract class Dice : MonoBehaviour
    {
        [SerializeField, InfoBox("0, 1, 2, 3, 4, 5 => down, back, right, up, left, front ")] private string _diceName;
        [SerializeField] protected List<int> _facesValues = new(6) { 1, 2, 3, 4, 5, 6 };
        [SerializeField] protected SerializedDictionary<GameObject, int> _faceIdMap = new();

        private void Reset()
        {
            foreach (Transform t in transform)
            {
                string direction = t.name.Remove(0, "AnchorPoint ".Length);
                switch (direction)
                {
                    case "Forward":
                        _faceIdMap[t.gameObject] = 5;
                        break;
                    case "Backward":
                        _faceIdMap[t.gameObject] = 1;
                        break;
                    case "Left":
                        _faceIdMap[t.gameObject] = 4;
                        break;
                    case "Right":
                        _faceIdMap[t.gameObject] = 2;
                        break;
                    case "Up":
                        _faceIdMap[t.gameObject] = 3;
                        break;
                    case "Down":
                        _faceIdMap[t.gameObject] = 0;
                        break;
                }
            }
        }

        public int GetUpFace()
        {
            float highestY = float.MinValue;
            int faceIndex = -1;
            foreach ((GameObject anchor, int i) in _faceIdMap)
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
        
            return _facesValues[faceIndex];
        }

        public abstract void ApplyEffect(int faceScore, Object gamestate);

        private void OnDrawGizmosSelected()
        {
            // TODO: Draw labels for directions
        }
    }
}