using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Physics
{
    

    [CreateAssetMenu]
    public class ProjectileMovement : ScriptableObject
    {
        public Vector2 InitialPosition;
        public Vector2 FinalPosition;
        public Vector2 InitialVelocity;
        public Vector2 FinalVelocity;
        private float gravity;           //g
        public float time;              //t

        public void CalcAngleOfLaunch()
        {
            //horizontal velocity
            float theta = Mathf.Atan(InitialVelocity.y / InitialVelocity.x);
        }

        public void CalcTimeOfFlight()
        {
            time = 2 * (InitialVelocity.y / gravity);
        }

        public void CalcDistance()
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
                var result = (InitialVelocity.y * InitialVelocity.y) / (2 * 9.81f);
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

            float maxHeight = 0;


            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = Color.white;

            if(GUILayout.Button("find angle"))
            {
                myProjectileMovement.CalcAngleOfLaunch();
            }

            GUILayout.Box("Maximum Height = " + myProjectileMovement.CalcMaximumHeight() + " meters" + "\n", boxStyle);

        }
    }
}



// Vx = Vo cos(theta) 19.82
// Vy = Vo sin(theta) - g * t 
// x = Vo cos(theta) * t
// y = Vo sin(theta) t - (1/2) g * t2