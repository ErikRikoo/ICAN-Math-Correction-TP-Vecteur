using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BallisticDrawing : MonoBehaviour
{
    [SerializeField] [HideInInspector] private LineRenderer m_LineRenderer;

    [Min(0f)]
    [SerializeField] private float m_PathLength;
    [Min(0.1f)]
    [SerializeField] private float m_VertexDensity;

    [Range(0, 360)]
    [SerializeField] private float m_Angle;

    [SerializeField] private float m_SpeedLength;
    [Range(0, 180)]
    [SerializeField] private float m_SpeedAngle;
    
    
    private void OnValidate()
    {
        if (m_LineRenderer == null)
        {
            m_LineRenderer = GetComponent<LineRenderer>();
        }

        UpdateRenderer();
    }

    private void UpdateRenderer()
    {
        if (m_LineRenderer.positionCount != VertexCount)
        {
            m_LineRenderer.positionCount = VertexCount;
        }

        Vector3 speed = Speed;
        Vector3 accumulatedPosition = transform.position;
        Vector3 gravity = new Vector3(0, -9.8f, 0);
        int count = VertexCount;
        for (int i = 0; i < count; ++i)
        {
            m_LineRenderer.SetPosition(i, accumulatedPosition);
            speed += gravity * 1/m_VertexDensity;
            accumulatedPosition += speed * 1/m_VertexDensity;
        }

    }

    public int VertexCount => (int) (m_PathLength * m_VertexDensity);

    public Vector3 Speed
    {
        get
        {
            Vector3 ret = Vector3.forward * m_SpeedLength;
            ret = Quaternion.Euler(-m_SpeedAngle, m_Angle, 0) * ret;
            return ret;
        }
    }
}
