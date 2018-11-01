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

        public float x;     //final position
        public float Xo;    //Initial x position
        public float y;     //Final y position
        public float Yo;    //Initial y position
        public float VOx;   //Initial x velocity
        public float VOy;   //Initial y velocity
        public float g;     //gravity
        public float t;     //time
        [HideInInspector]
        public float result;


    }

    [CustomEditor(typeof(ProjectileMovement))]
    public class ProjectileMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ProjectileMovement projectile = CreateInstance<ProjectileMovement>();

            ProjectileMovement myProjectileMovement = (ProjectileMovement) target;
            if (GUILayout.Button("Calculate"))
            {
                projectile.x = projectile.Xo + projectile.VOx * projectile.t;
                projectile.y = projectile.Yo + (projectile.VOy * projectile.t) +
                               ((1 / 2) * projectile.g * projectile.t * projectile.t);
            }

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = Color.white;

            GUILayout.Box("Result = " + projectile.result.ToString(), boxStyle);

        }
    }
}

// Vx = Vo cos(theta) 19.82
// Vy = Vo sin(theta) - g * t 
// x = Vo cos(theta) * t
// y = Vo sin(theta) t - (1/2) g * t2