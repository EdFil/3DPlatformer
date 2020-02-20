using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    Transform _player;
    SphereCollider _visionArea;
    NavMeshAgent _navMeshAgent;
    Vector3 _lastPlayerPosition;
    bool _seenPlayer = false;
    bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _visionArea = GetComponent<SphereCollider>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
            Vector3 aiVision = new Vector3(transform.position.x, transform.position.y + _navMeshAgent.height * 0.8f, transform.position.z);
            Vector3 playerVision = new Vector3(_player.position.x, _player.position.y + _navMeshAgent.height * 0.8f, _player.position.z);
            Vector3 aiLineOfSightToPlayer = playerVision - aiVision;

            if (Vector3.Dot(transform.forward, aiLineOfSightToPlayer) > 1) {
                if (Physics.Raycast(aiVision, aiLineOfSightToPlayer, out hit, _visionArea.radius))
                {
                    _seenPlayer = hit.collider.gameObject.tag == "Player";
                    if (_seenPlayer) {
                        Debug.DrawRay(aiVision, aiLineOfSightToPlayer);
                    }
                }
            }
        }

        if (_seenPlayer) {
            _navMeshAgent.destination = _player.position;
            _seenPlayer = false;
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
