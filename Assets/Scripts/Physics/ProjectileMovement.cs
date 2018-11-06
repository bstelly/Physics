using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Comparers;


namespace Assets.Scripts.Physics
{
    

    [CreateAssetMenu]
    public class ProjectileMovement : ScriptableObject
    {
        public Vector2 InitialPosition;
        [HideInInspector]
        public Vector2 FinalPosition;
        public Vector2 InitialVelocity;
        private Vector2 FinalVelocity;
        public readonly float gravity = 9.81f;  //g
        public float time;              //t


        public float CalcAngleOfLaunch()
        {
            float theta = 0;
            if (InitialVelocity.x > 0 && InitialVelocity.y > 0)
            {
                theta = Mathf.Atan(InitialVelocity.y / InitialVelocity.x);
            }
            //else if (InitialVelocity.x > 0 && InitialVelocity.y <= 0)
            //{
            //    theta = Mathf.Acos(InitialVelocity.x / InitialVelocity)
            //}
            //else if (InitialVelocity.x <= 0 && InitialVelocity.y > 0)
            //{

            //}
            return theta;
        }

        public Vector2 CalcFinalPosition()
        {
            FinalPosition.x = CalcDistance() + InitialPosition.x;
            return FinalPosition;
        }

        public float CalcTimeOfFlight()
        {
            return time = 2 * (InitialVelocity.y / gravity);
        }

        public float CalcDistance()
        {
            float distance = 0;
            if (InitialVelocity.y != 0)
            {
                distance = (2 * InitialVelocity.x * InitialVelocity.y) / gravity;
            }
            else
            {
                distance = InitialVelocity.x * time;
            }
            return distance;
        }

        public void CalcInitialVelocity()
        {
            InitialVelocity.x = FinalVelocity.x - (gravity * time);
            InitialVelocity.y = FinalVelocity.y - (gravity * time);
        }

        public void CalcFinalVelocity()
        {
            FinalVelocity.x = InitialVelocity.x + (gravity * time);
            FinalVelocity.y = InitialVelocity.y + (gravity * time);
        }

        public void CalcHorizontalVelocity()
        {
            float distance = CalcDistance();
            InitialVelocity.x = distance / time;
        }

        public void CalcVerticalVelocity()
        {

        }

        public float CalcMaximumHeight()
        {
            //if (FinalVelocity.x != 0)
            //{
            //    var numerator = ((FinalVelocity * FinalVelocity) - (InitialVelocity * InitialVelocity));
            //    var denominator = 2 * -9.81f;
            //    var result = new Vector2(numerator.x / denominator, numerator.y / denominator);
            //    return result;
            //}
            //else
            //{
                var result = InitialPosition.y + ((InitialVelocity.y * InitialVelocity.y) / (2 * gravity));
                return result;
            //}

        }
    }

    [CustomEditor(typeof(ProjectileMovement))]
    public class ProjectileMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProjectileMovement myProjectileMovement = (ProjectileMovement) target;

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = Color.white;

            GUILayout.Box("Final Position = " + myProjectileMovement.CalcFinalPosition(), boxStyle);
            GUILayout.Box("Angle of Launch = " + myProjectileMovement.CalcAngleOfLaunch() + " rad" + " / " + 
                ((myProjectileMovement.CalcAngleOfLaunch() * 180) / Mathf.PI) + " deg", boxStyle);
            GUILayout.Box("Time of Flight = " + myProjectileMovement.CalcTimeOfFlight() + " seconds", boxStyle);
            GUILayout.Box("Distance = " + myProjectileMovement.CalcDistance() + " meters", boxStyle);
            GUILayout.Box("Maximum Height = " + myProjectileMovement.CalcMaximumHeight() + " meters", boxStyle);


        }
    }
}
