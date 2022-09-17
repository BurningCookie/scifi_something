using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform TargetPositionTransform;

    private NavMeshAgent navMeshAgent;

    public GameObject AgentObject;
    public GameObject PlayerObject;
    public GameObject target;
    public GameObject RaycastTarget;
    
    public Camera cam;

    public bool enemyhit;

    public float maxRange;


    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = TargetPositionTransform.position;

        RaycastHit hit;

        var targetRender = target.GetComponent<Renderer>();

        if (IsVisible(cam, target) == false)
        {
            navMeshAgent.isStopped = false;
        }
        
        if(IsVisible(cam, target))
        {
            if (Physics.Raycast(AgentObject.transform.position, new Vector3(PlayerObject.transform.position.x - RaycastTarget.transform.position.x, PlayerObject.transform.position.y - RaycastTarget.transform.position.y, PlayerObject.transform.position.z - RaycastTarget.transform.position.z), out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    navMeshAgent.isStopped = true;

                    navMeshAgent.velocity = new Vector3(0, 0, 0);
                }
                else
                {
                    navMeshAgent.isStopped = false;
                }
            }
        }
    }
}