using UnityEngine;
using System.Collections;

public class NaviAgent : MonoBehaviour
{
    public Transform goal;
    Vector3 start;

    // Use this for initialization
    void Start()
    {
        // 最初の位置を覚えておく
        start = transform.position;
        // NavMeshAgentを取得して
        var agent = GetComponent<NavMeshAgent>();

        // ゴールを設定。
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        // クリックで最初の位置にもどる。
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = start;
        }


    }
}