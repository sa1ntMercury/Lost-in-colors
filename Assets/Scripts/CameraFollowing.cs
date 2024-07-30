using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollowing : MonoBehaviour
{
    [SerializeField] public Transform _player;
    [SerializeField] private int _speed;

    private Vector3 _playerVector;

    void Update()
    {
             _playerVector = _player.position;
            transform.position = Vector3.Lerp(transform.position, new Vector3(_playerVector.x, _playerVector.y + 2f, _playerVector.z - 5), _speed * Time.deltaTime);
    }
}
