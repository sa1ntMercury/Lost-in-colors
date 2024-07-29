using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private Setup _prefab;

    private List<Setup> _setups;

    private void Awake()
    {
        AddSetup(new Vector3());
    }

    public void AddSetup(Vector3 position)
    {
        if(_setups == null)
        {
            _setups = new List<Setup>(8);
        }

        if(_setups.Count >= 2)
        {
            for (int i = 0; i < _setups.Count/2; i++)
            {
                Destroy(_setups[i].gameObject);
            }

            _setups.RemoveRange(0, _setups.Count / 2);

        }

        _setups.Add(Instantiate(_prefab, position, new Quaternion()));
        _setups[0].PlugInLine();

    }
}
