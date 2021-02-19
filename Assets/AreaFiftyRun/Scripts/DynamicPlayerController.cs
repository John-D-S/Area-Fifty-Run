using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class DynamicPlayerController : MonoBehaviour
{
    /*[SerializeField]
    private List<Vector2> walkNodes = new List<Vector2> { new Vector2(0.75f, -3), new Vector2(-0.75f, -3), new Vector2(-0.75f, -2.5f), new Vector2(f, -2.5f) };
    */
    [SerializeField]
    private List<Vector2> runNodes = new List<Vector2> { new Vector2(1, -3), new Vector2(-1, -3), new Vector2(-1.75f, -1.5f), new Vector2(1.5f, -2.5f), new Vector2(1, -3) };
    [SerializeField]
    private float runcycleTime = 0.5f;

    private Vector2 targetFootPositionF;
    private Vector2 targetFootPositionB;

    [SerializeField]
    public SpringJoint2D fFoot;
    [SerializeField]
    public SpringJoint2D bFoot;

    private FootPath Run;
    private void OnValidate()
    {
        Run = new FootPath(runNodes);
    }

    private void Update()
    {
        Run.DrawDebugPath(gameObject.transform.position);
    }

    private float runTimer;

    void FixedUpdate()
    {
        runTimer += Time.fixedDeltaTime;
        if (runTimer >= runcycleTime)
        {
            runTimer -= runcycleTime;
        }
        float percentThrough = runTimer / runcycleTime;

        fFoot.connectedAnchor = Run.TargetPosition(percentThrough);
    }

}
