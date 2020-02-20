using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    static int _totalRays = 10;
    Transform _player;
    Collider _collider;
    NavMeshAgent _navMeshAgent;
    Vector3 _lastPlayerPosition;
    bool _seenPlayer = false;
    bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < _totalRays; i++) {
            float angle = Mathf.LerpAngle(-45, 45, (float)i/(float)_totalRays);

            Debug.DrawRay(_head.position, Quaternion.Euler(0, angle, 0) * _head.forward * 5.0f);
            Debug.DrawRay(_head.position, Quaternion.Euler(angle, 0, 0) * _head.up * 5.0f);
        }*/

        if (_isActive) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _player.position - transform.position, out hit, Mathf.Infinity))
            {
                _seenPlayer = hit.collider.gameObject.tag == "Player"; 
            }
        }

        if (_seenPlayer) {
            _navMeshAgent.destination = _player.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isActive = other.gameObject.tag == "Player";
    }

    private void OnTriggerExit(Collider other)
    {
        _isActive = false;
    }
}
