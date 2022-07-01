using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class AsteroidWave : ScriptableObject
{
    [SerializeField]
    private AsteroidSpawnSequence[] _spawnSequences;

    public State Begin() => new State(this);

    [Serializable]
    public struct State
    {
        private AsteroidWave _wave;
        private int _index;
        private AsteroidSpawnSequence.State _sequence;

        public State(AsteroidWave wave)
        {
            _wave = wave;
            _index = 0;
            _sequence = wave._spawnSequences[0].Begin();
        }

        public float Progress(float deltaTime, GameBehaviorCollection asteroid)
        {
           
                deltaTime = _sequence.Progress(deltaTime, asteroid);
                while (deltaTime >= 0f)
                {
                    if (++_index >= _wave._spawnSequences.Length)
                    {
                        return deltaTime;
                    }
                    _sequence = _wave._spawnSequences[_index].Begin();
                    deltaTime = _sequence.Progress(deltaTime, asteroid);
                }
                
            
            return -1f;
        }
    }
}
