using System;
using System.Collections;
using UnityEngine;

namespace Runtime.Lerping
{
    public abstract class ALerping : MonoBehaviour
    {

        [SerializeField] private bool m_ShouldAnimateOnEnable;

        [SerializeField] private float m_Duration;
        [SerializeField] private AnimationCurve m_Curve;
        private float m_InverseDuration;

        private void Awake()
        {
            m_InverseDuration = 1 / m_Duration;
        }

        private void OnEnable()
        {
            if (m_ShouldAnimateOnEnable)
            {
                StartCoroutine(c_Animate());
            }
        }

        private IEnumerator c_Animate()
        {
            for (float time = 0f; time < m_Duration; time += Time.deltaTime)
            {
                UpdateState(m_Curve.Evaluate(time * m_InverseDuration));
                yield return null;
            }
            UpdateState(m_Curve.Evaluate(1));
        }

        protected abstract void UpdateState(float _time);
    }
}