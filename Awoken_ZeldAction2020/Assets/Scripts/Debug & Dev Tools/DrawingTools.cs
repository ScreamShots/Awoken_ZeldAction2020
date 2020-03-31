using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by Rémi Sécher
/// Merging two script allowing to create circle from any class in the game.
/// To use it dont forget to add the namespace DevTools.
/// The two script i merge code from:
/// - https://www.codinblack.com/how-to-draw-lines-circles-or-anything-else-using-linerenderer/
/// - https://www.loekvandenouweland.com/content/use-linerenderer-in-unity-to-draw-a-circle.html
/// </summary>


namespace DevTools
{

    public static class DrawingTools
    {
        public static void DrawCircle(this GameObject container, float radius, float lineWidth, int vertexNumber, Color lineColor, bool useWorldSpc)  //static function that you can call from anywhere to creat circles
        {
            var line = container.AddComponent<LineRenderer>();          //adding a line renderer component to the targeted gameobject holder
            line.useWorldSpace = useWorldSpc;                                 //allow the circle to move depending gameobject world position
            line.startColor = lineColor;                                //set the line color
            line.endColor = lineColor;
            line.material = new Material(Shader.Find("Sprites/Default"));   //set line material (to enable color to show)

            line.startWidth = lineWidth;                                    //setting the line width
            line.endWidth = lineWidth;
            line.loop = true;                                               //Unable that properties to make the last point of the line the same as the first

            //Creating the circle (maths function & logic)
            float angle = 2 * Mathf.PI / vertexNumber;
            line.positionCount = vertexNumber;

            for (int i = 0; i < vertexNumber; i++)
            {
                Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                         new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                           new Vector4(0, 0, 1, 0),
                                           new Vector4(0, 0, 0, 1));
                Vector3 initialRelativePosition = new Vector3(0, radius, 0);
                line.SetPosition(i, container.transform.position + rotationMatrix.MultiplyPoint(initialRelativePosition));

            }
        }
    }

}

