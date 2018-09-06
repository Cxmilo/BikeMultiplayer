/*
Race Positioning System by Solution Studios

Script Name: RPS_PSPlacer.cs

Description:
This script is for the RPS_Editor only.
It contols the placing of PositionSensors in the scene.
It is a modified version of the WaypointCircuit.cs script from the Vehicle Standard Assets


We haven't commented this script much as it is just lots of complicated looking arrays and spline equations.
The second half (line 580 onwards) provides the inspector fields.
One of the parts changed from the WaypointCircuit.cs script is the OnDrawGizmos function where we have added drawing the red cubes as you would see in the editor.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RPS_PSPlacer : MonoBehaviour
{
    public WaypointList waypointList = new WaypointList();
    [SerializeField]
    bool smoothRoute = true;

	[HideInInspector]
    public bool isLoop = false;
    int numPoints;
    Vector3[] points;
    float[] distances;

	bool isDone = false;

    public float editorVisualisationSubsteps = 1000;
    public float Length { get; private set; }
    public Transform[] Waypoints { get { return waypointList.items; } }

	[HideInInspector]
    public bool pointsEnabled = true;
	[HideInInspector]
    public float pointSubsteps = 100;
	[HideInInspector]
    public float trackwidth = 4.0f;

    //this being here will save GC allocs
    int p0n;
    int p1n;
    int p2n;
    int p3n;

    private float i;
    Vector3 P0;
    Vector3 P1;
    Vector3 P2;
    Vector3 P3;

    // Use this for initialization
    void Awake()
    {
        if (Waypoints.Length > 1)
        {
            CachePositionsAndDistances();
        }
        numPoints = Waypoints.Length;
        if (isLoop == false)
        {
            numPoints = Waypoints.Length - 1;
        }
    }

    public void CreateCubes ()
    {
		bool is2D = true;
		GameObject storobj = GameObject.Find("RPS_Storage");
		if (storobj != null) {
			RPS_Storage storscriptt = storobj.gameObject.GetComponent<RPS_Storage>();
			if (storscriptt != null) {
				is2D = storscriptt.is2D;
			}
		}
		isDone = true;
        RPS_Storage storscript = gameObject.GetComponent<RPS_Storage>();
		storscript.hasAddedPSScript = false;
		storscript.hasRemovedPSScript = true;
        if (storscript != null)
        {
            Length = distances[distances.Length - 1];
            float newleng = Length;
            if (isLoop == false)
            {
                newleng = Length - 1;
            }
            numPoints = Waypoints.Length;
            if (isLoop == false)
            {
                numPoints = Waypoints.Length - 1;
            }
            Vector3 pprev = Waypoints[0].position;
			int numcreated = 0;
			Material cubeMat = Resources.Load("cubeMat") as Material;
			storscript.cuboidarr = new List<GameObject>();
            for (float dist = 0; dist < newleng; dist += newleng / pointSubsteps)
            {
                Vector3 next = GetRoutePosition(dist + 1);

                if (((pprev == points[points.Length - 1]) && (next == points[0])) || ((pprev == points[0]) && (next == points[points.Length - 1])))
                {
                }
                else
                {
                    Vector3 nextt = GetRoutePosition(dist + 0.01f);
					if (is2D == true) {
	                    Quaternion rotation = Quaternion.LookRotation(nextt - pprev);
						GameObject newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						numcreated = numcreated + 1;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
					} else {
						Quaternion rotation = Quaternion.LookRotation(nextt - pprev);
						GameObject newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						RPS_PS3D setScript = newcube.gameObject.AddComponent<RPS_PS3D>();
						setScript.setNumber = numcreated;
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
						rotation *= Quaternion.Euler(Vector3.forward * 30);
						newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						setScript = newcube.gameObject.AddComponent<RPS_PS3D>();
						setScript.setNumber = numcreated;
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
						rotation *= Quaternion.Euler(Vector3.forward * 30);
						newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						setScript = newcube.gameObject.AddComponent<RPS_PS3D>();
						setScript.setNumber = numcreated;
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
						rotation *= Quaternion.Euler(Vector3.forward * 30);
						newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						setScript = newcube.gameObject.AddComponent<RPS_PS3D>();
						setScript.setNumber = numcreated;
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
						rotation *= Quaternion.Euler(Vector3.forward * 30);
						newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						setScript = newcube.gameObject.AddComponent<RPS_PS3D>();
						setScript.setNumber = numcreated;
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
						rotation *= Quaternion.Euler(Vector3.forward * 30);
						newcube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						newcube.name = "Cube " + numcreated;
						newcube.transform.position = nextt;
						newcube.transform.rotation = rotation;
						newcube.transform.localScale = new Vector3(trackwidth,0.5f,0.5f);
						setScript = newcube.gameObject.AddComponent<RPS_PS3D>();
						setScript.setNumber = numcreated;
						newcube.gameObject.GetComponent<Renderer>().material = cubeMat;
						newcube.gameObject.transform.parent = storscript.gameObject.transform;
						storscript.cuboidarr.Add(newcube.gameObject);
						numcreated = numcreated + 1;
					}
					//then later, create PS gameobjects as children of the cubes and remove cube renderers etc.
					//the child objects should get their scripts attached with their sensor numbers set through a loop
					//the scale of the sensors should be 1/scaleOfCube as then -0.5,0,0 and 0.5,0,0 local positions will give ends of the cube
					//you can just add your things to the x value to find the PS positions
					//e.g. For 7 position sensors (using ninths as don't want them right at ends)
					//     -0.5 + (1/8) ,0,0
					//     -0.5 + (2/8) ,0,0
					//     -0.5 + (3/8) ,0,0
					//     -0.5 + (4/8) ,0,0
					//     -0.5 + (5/8) ,0,0
					//     -0.5 + (6/8) ,0,0
					//     -0.5 + (7/8) ,0,0

                }

                pprev = next;
            }
        
        foreach (GameObject gam in storscript.pointChildren)
            {
                DestroyImmediate(gam.gameObject);
            }
        }
		DestroyImmediate(gameObject.GetComponent<RPS_PSPlacer>());
    }

    public RoutePoint GetRoutePoint(float dist)
    {
        // position and direction
        Vector3 p1 = GetRoutePosition(dist);
        Vector3 p2 = GetRoutePosition(dist + 0.1f);
        Vector3 delta = p2 - p1;
        return new RoutePoint(p1, delta.normalized);
    }

    public Vector3 GetRoutePosition(float dist)
    {
        numPoints = Waypoints.Length;
        if (isLoop == false)
        {
            //numPoints = Waypoints.Length - 1;
        }

        int point = 0;

        if (Length == 0)
        {
            Length = distances[distances.Length - 1];
        }

        dist = Mathf.Repeat(dist, Length);

        while (distances[point] < dist) { ++point; }


        // get nearest two points, ensuring points wrap-around start & end of circuit
        p1n = ((point - 1) + numPoints) % numPoints;
        p2n = point;

        // found point numbers, now find interpolation value between the two middle points

        i = Mathf.InverseLerp(distances[p1n], distances[p2n], dist);

        if (smoothRoute)
        {
            // smooth catmull-rom calculation between the two relevant points



            // get indices for the surrounding 2 points, because
            // four points are required by the catmull-rom function
            p0n = ((point - 2) + numPoints) % numPoints;
            p3n = (point + 1) % numPoints;



            //THINK HERE the found point is between p1 and p2
            //it tends to p3n so p3n must not be 0
            if (isLoop == false)
            {
                if (p3n == 0)
                {
                    p3n = p2n;
                }
                if (p1n == 0)
                {
                    p0n = 0;
                }
            }

            // 2nd point may have been the 'last' point - a dupe of the first,
            // (to give a value of max track distance instead of zero)
            // but now it must be wrapped back to zero if that was the case.
            p2n = p2n % numPoints;

            P0 = points[p0n];
            P1 = points[p1n];
            P2 = points[p2n];
            P3 = points[p3n];

            return CatmullRom(P0, P1, P2, P3, i);

        }
        else
        {

            // simple linear lerp between the two points:

            p1n = ((point - 1) + numPoints) % numPoints;
            p2n = point;

            return Vector3.Lerp(points[p1n], points[p2n], i);

        }

    }



    Vector3 CatmullRom(Vector3 _P0, Vector3 _P1, Vector3 _P2, Vector3 _P3, float _i)
    {
        // comments are no use here... it's the catmull-rom equation.
        // Un-magic this, lord vector!
        return 0.5f * ((2 * _P1) + (-_P0 + _P2) * _i + (2 * _P0 - 5 * _P1 + 4 * _P2 - _P3) * _i * _i + (-_P0 + 3 * _P1 - 3 * _P2 + _P3) * _i * _i * _i);
    }


    void CachePositionsAndDistances()
    {
        // transfer the position of each point and distances between points to arrays for
        // speed of lookup at runtime
        points = new Vector3[Waypoints.Length + 1];
        distances = new float[Waypoints.Length + 1];

        float accumulateDistance = 0;
        for (int i = 0; i < points.Length; ++i)
        {
            var t1 = Waypoints[(i) % Waypoints.Length];
            var t2 = Waypoints[(i + 1) % Waypoints.Length];
            if (t1 != null && t2 != null)
            {
                Vector3 p1 = t1.position;
                Vector3 p2 = t2.position;
                points[i] = Waypoints[i % Waypoints.Length].position;
                distances[i] = accumulateDistance;
                accumulateDistance += (p1 - p2).magnitude;
            }
        }
        if (isLoop == false)
        {
            distances[distances.Length - 1] = distances[distances.Length - 2];
            float[] newdist = new float[distances.Length - 1];
            int arrrrrnum = 0;
            foreach (float dist in distances)
            {
                if (arrrrrnum < newdist.Length)
                {
                    newdist[arrrrrnum] = dist;
                }
                arrrrrnum = arrrrrnum + 1;
            }
            distances = newdist;
            points = new Vector3[Waypoints.Length];
            int arraynumber = 0;
            foreach (Transform poi in Waypoints)
            {
                points[arraynumber].x = poi.position.x;
                points[arraynumber].y = poi.position.y;
                points[arraynumber].z = poi.position.z;
                arraynumber = arraynumber + 1;
            }

        }
    }


    void OnDrawGizmos()
    {
        DrawGizmos(true);
    }

    void OnDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    void DrawGizmos(bool selected)
    {
		bool is2D = true;
		GameObject storObj = GameObject.Find("RPS_Storage");
		if (storObj != null) {
			RPS_Storage storscript = storObj.gameObject.GetComponent<RPS_Storage>();
			if (storscript != null) {
				is2D = storscript.is2D;
			}
		}

		if (isDone == false) {
        selected = true;
        if (pointsEnabled == true)
        {
            if (pointSubsteps < 1)
            {
                pointSubsteps = 1;
            }

        }

        waypointList.circuit = this;
        if (((isLoop == true) && (Waypoints.Length > 1)) || ((isLoop == false) && (Waypoints.Length > 2)))
        {
            numPoints = Waypoints.Length;
            if (isLoop == false)
            {
                numPoints = Waypoints.Length - 1;
            }

            CachePositionsAndDistances();
            Length = distances[distances.Length - 1];
            Gizmos.color = selected ? Color.yellow : new Color(1, 1, 0, 0.5f);
            Vector3 prev = Waypoints[0].position;float newleng = Length;
                if (isLoop == false)
                {
                    newleng = Length - 1;
                }
            if (smoothRoute)
            {
                
                for (float dist = 0; dist < newleng; dist += newleng / editorVisualisationSubsteps)
                {
                    Vector3 next = GetRoutePosition(dist + 1);
                    if (isLoop == true)
                    {
                        Gizmos.DrawLine(prev, next);
                    }
                    else
                    {
                        if (((prev == points[points.Length - 1]) && (next == points[0])) || ((prev == points[0]) && (next == points[points.Length - 1])))
                        {

                        }
                        else
                        {
                            Gizmos.DrawLine(prev, next);
                        }
                    }
                    prev = next;
                }
                if (pointsEnabled == true)
                {
                    Vector3 pprev = Waypoints[0].position;
                    for (float dist = 0; dist < newleng; dist += newleng / pointSubsteps)
                    {
                        Vector3 next = GetRoutePosition(dist + 1);

                        if (((pprev == points[points.Length - 1]) && (next == points[0])) || ((pprev == points[0]) && (next == points[points.Length - 1])))
                        {
                        }
                        else
                        {
                            Vector3 nextt = GetRoutePosition(dist + 0.01f);
                            Gizmos.color = new Color(1, 0, 0, 0.5f);
                            Quaternion rotation = Quaternion.LookRotation(nextt - pprev);
							if (is2D) {
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);	
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.5f, 0.5f));
							} else {
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);	
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.3f, 0.3f));
								rotation *= Quaternion.Euler(Vector3.forward * 30);
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.3f, 0.3f));
								rotation *= Quaternion.Euler(Vector3.forward * 30);
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.3f, 0.3f));
								rotation *= Quaternion.Euler(Vector3.forward * 30);
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.3f, 0.3f));
								rotation *= Quaternion.Euler(Vector3.forward * 30);
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.3f, 0.3f));
								rotation *= Quaternion.Euler(Vector3.forward * 30);
								Gizmos.matrix = Matrix4x4.TRS(pprev, rotation, transform.localScale);
								Gizmos.DrawCube(new Vector3(0, 0, 0), new Vector3(trackwidth, 0.3f, 0.3f));
							}
                            Gizmos.color = selected ? Color.yellow : new Color(1, 1, 0, 0.5f);
                            Gizmos.matrix = Matrix4x4.zero;
                        }

                        pprev = next;
                    }
                }
                if (isLoop == true)
                {
                    Gizmos.DrawLine(prev, Waypoints[0].position);
                }
            }
            else
            {
                prev = points[0];
                int arrnum = 0;
                foreach (Vector3 poi in points)
                {
                    Vector3 next = poi;
                    Gizmos.DrawLine(prev, next);
                    prev = next;
                    arrnum = arrnum + 1;
                }
                if (isLoop == true)
                {
                    Gizmos.DrawLine(points[points.Length - 1], points[points.Length - 2]);
                }
            }
            string getstring = "Waypoints: Length: " + Waypoints.Length + " pos: ";
            foreach (Transform way in Waypoints)
            {
                getstring = getstring + "(" + way.position.x + "," + way.position.y + "," + way.position.z + ") ";
            }
            getstring = getstring + "Points: Length: " + distances.Length + " pos: ";
            foreach (float way in distances)
            {
                getstring = getstring + "(" + way + "," + way + "," + way + ") ";
            }
        }
    }
	}

    [System.Serializable]
    public class WaypointList
    {
        public RPS_PSPlacer circuit;
        public Transform[] items = new Transform[0];
    }

    public struct RoutePoint
    {
        public Vector3 position;
        public Vector3 direction;

        public RoutePoint(Vector3 position, Vector3 direction)
        {
            this.position = position;
            this.direction = direction;
        }

    }

    public void addPoint(Transform newpoint)
    {
        if (waypointList.items.Length > 0)
        {
            Transform[] newarray = new Transform[waypointList.items.Length + 1];
            int num = 0;
            foreach (Transform trans in waypointList.items)
            {
				if (trans != null) {
                	newarray[num] = waypointList.items[num];
				}
                num = num + 1;
            }
            newarray[newarray.Length - 1] = newpoint;
            waypointList.items = newarray;
        } else
        {
            waypointList.items = new Transform[1];
            waypointList.items[0] = newpoint;
        }
        int nn = 0; foreach (Transform child in waypointList.items) child.name = "Point " + (nn++).ToString("000");

    }

    public class TransformNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((Transform)x).name.CompareTo(((Transform)y).name);
        }
    }

}



#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RPS_PSPlacer.WaypointList))]
public class PSListDrawer : PropertyDrawer
{
    float lineHeight = 18;
    float spacing = 4;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float x = position.x;
        float y = position.y;
        float inspectorWidth = position.width;

        // Draw label


        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var items = property.FindPropertyRelative("items");
        string[] titles = new string[] { "Transform", "", "", "" };
        string[] props = new string[] { "transform", "^", "v", "-" };
        float[] widths = new float[] { .7f, .1f, .1f, .1f };
        float lineHeight = 18;
        bool changedLength = false;
        if (items.arraySize > 0)
        {

            for (int i = -1; i < items.arraySize; ++i)
            {

                var item = items.GetArrayElementAtIndex(i);

                float rowX = x;
                for (int n = 0; n < props.Length; ++n)
                {
                    float w = widths[n] * inspectorWidth;

                    // Calculate rects
                    Rect rect = new Rect(rowX, y, w, lineHeight);
                    rowX += w;

                    if (i == -1)
                    {
                        EditorGUI.LabelField(rect, titles[n]);
                    }
                    else
                    {
                        if (n == 0)
                        {
                            EditorGUI.ObjectField(rect, item.objectReferenceValue, typeof(Transform), true);
                        }
                        else
                        {
                            if (GUI.Button(rect, props[n]))
                            {
                                switch (props[n])
                                {
                                    case "-":
                                        items.DeleteArrayElementAtIndex(i);
                                        items.DeleteArrayElementAtIndex(i);
                                        changedLength = true;
                                        break;
                                    case "v":
                                        if (i > 0) items.MoveArrayElement(i, i + 1);
                                        break;
                                    case "^":
                                        if (i < items.arraySize - 1) items.MoveArrayElement(i, i - 1);
                                        break;
                                }

                            }
                        }
                    }
                }

                y += lineHeight + spacing;
                if (changedLength)
                {
                    break;
                }
            }

        }
        else
        {

            // add button
            var addButtonRect = new Rect((x + position.width) - widths[widths.Length - 1] * inspectorWidth, y, widths[widths.Length - 1] * inspectorWidth, lineHeight);
            if (GUI.Button(addButtonRect, "+"))
            {
                items.InsertArrayElementAtIndex(items.arraySize);
            }

            y += lineHeight + spacing;
        }

        // add all button
        var addAllButtonRect = new Rect(x, y, inspectorWidth, lineHeight);
        if (GUI.Button(addAllButtonRect, "Assign using all child objects"))
        {
            var circuit = property.FindPropertyRelative("circuit").objectReferenceValue as RPS_PSPlacer;
            var children = new Transform[circuit.transform.childCount];
            int n = 0; foreach (Transform child in circuit.transform) children[n++] = child;
            System.Array.Sort(children, new TransformNameComparer());
            circuit.waypointList.items = new Transform[children.Length];
            for (n = 0; n < children.Length; ++n)
            {
                circuit.waypointList.items[n] = children[n];
            }


        }
        y += lineHeight + spacing;

        // rename all button
        var renameButtonRect = new Rect(x, y, inspectorWidth, lineHeight);
        if (GUI.Button(renameButtonRect, "Auto Rename numerically from this order"))
        {
            var circuit = property.FindPropertyRelative("circuit").objectReferenceValue as RPS_PSPlacer;
            int n = 0; foreach (Transform child in circuit.waypointList.items) child.name = "Point " + (n++).ToString("000");

        }
        y += lineHeight + spacing;

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty items = property.FindPropertyRelative("items");
        float lineAndSpace = lineHeight + spacing;
        return 40 + (items.arraySize * lineAndSpace) + lineAndSpace;
    }

    // comparer for check distances in ray cast hits
    public class TransformNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((Transform)x).name.CompareTo(((Transform)y).name);
        }
    }

}
#endif
