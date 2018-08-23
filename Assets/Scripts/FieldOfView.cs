using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public float viewRadius = 7;
    [Range(0, 360)]
    public float viewAngle = 147f;
    public float viewDelay = 0.2f;

    public float meshResolution;
    public MeshFilter viewMeshFilter;

    public LayerMask targetMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private Mesh viewMesh;

    public void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindTargetsCorut", viewDelay);
    }

    public void LateUpdate()
    {
        DrawFieldOfView();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        angleInDegrees += 90;

        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.z;

        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    private IEnumerator FindTargetsCorut(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                RaycastHit2D ray = Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, targetMask);
                if (object.ReferenceEquals(ray.transform, target))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector2> viewPoints = new List<Vector2>();
        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo castInfo = viewCast(angle);
            viewPoints.Add(castInfo.point);
        }

        int vertextCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertextCount];
        int[] triangles = new int[(vertextCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertextCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertextCount - 2) { 
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }


    private struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.distance = distance;
            this.angle = angle;
        }
    }

    private ViewCastInfo viewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, targetMask);

        if (hit.collider)
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
    }
}
