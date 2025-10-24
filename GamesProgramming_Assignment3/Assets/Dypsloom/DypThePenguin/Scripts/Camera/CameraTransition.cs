/// ---------------------------------------------
/// Dyp The Penguin Character | Dypsloom
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.DypThePenguin.Scripts.Camera
{
    using Dypsloom.Shared.Utility;
    using UnityEngine;
    using System.Reflection;

    public class CameraTransition : MonoBehaviour
    {
        [Tooltip("The virtual camera.")]
        [SerializeField] protected Component m_Vcam;
        [Tooltip("The priority the camera will have at the start of the transistion.")]
        [SerializeField] protected int m_Priority = 20;
        [Tooltip("The time to transition back, 0 or lower won't transition back.")]
        [SerializeField] protected float m_TransitionBackTime = 2;

        protected int m_PreviousPriority;

        /// <summary>
        /// Start the camera transition.
        /// </summary>
        public void Transition()
        {
            if (m_Vcam == null)
            {
                return;
            }

            var prop = m_Vcam.GetType().GetProperty("Priority", BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                // No Priority property available on the assigned component.
                return;
            }

            var prevValue = prop.GetValue(m_Vcam);
            m_PreviousPriority = prevValue is int ? (int)prevValue : 0;

            prop.SetValue(m_Vcam, m_Priority);

            if (m_TransitionBackTime <= 0)
            {
                return;
            }

            SchedulerManager.Schedule(
                () =>
                {
                    if (m_Vcam == null) return;
                    var p = m_Vcam.GetType().GetProperty("Priority", BindingFlags.Public | BindingFlags.Instance);
                    if (p != null) p.SetValue(m_Vcam, m_PreviousPriority);
                },
                m_TransitionBackTime);
        }
    }
}