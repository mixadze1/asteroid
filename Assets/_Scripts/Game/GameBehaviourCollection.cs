using System.Collections.Generic;
using UnityEngine;

public class GameBehaviorCollection
{
    private List<GameBehavior> _behaviors = new List<GameBehavior>();
    public bool IsEmpty => _behaviors.Count == 0;
    public int IsLength => _behaviors.Count;

    public void Add(GameBehavior asteroid)
    {
        _behaviors.Add(asteroid);
    }

    public void GameUpdate()
    {
        for (int i = 0; i < _behaviors.Count; i++)
        {
            if (!_behaviors[i].GameUpdate())
            {
                int lastIndex = _behaviors.Count - 1;
                _behaviors[i] = _behaviors[lastIndex];
                _behaviors.RemoveAt(lastIndex);
                i -= 1;
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < _behaviors.Count; i++)
        {
            if(_behaviors[i] != null )
                _behaviors[i].Recycle();
        }
        if (_behaviors != null)
            _behaviors.Clear();
    }
}
