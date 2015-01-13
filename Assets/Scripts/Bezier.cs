using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bezier : System.Object
{
	public Vector3 p0;
	public Vector3 p1;
	public Vector3 p2;
	public Vector3 p3;
	
	public float ti = 0f;

	private float Ax;
	private float Ay;
	private float Az;
	
	private float Bx;
	private float By;
	private float Bz;
	
	private float Cx;
	private float Cy;
	private float Cz;
	
	// Init function v0 = 1st point, v1 = handle of the 1st point , v2 = handle of the 2nd point, v3 = 2nd point
	// handle1 = v0 + v1
	// handle2 = v3 + v2
	public Bezier( Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3 )
	{
		this.p0 = v0;
		this.p1 = v1;
		this.p2 = v2;
		this.p3 = v3;
	}
	// 0.0 >= t <= 1.0
	public Vector3 GetPointAtTime(float t) {
		Vector3 m0 = (p1 - p0) * t + p0, m1 = (p2 - p1) * t + p1, m2 = (p3 - p2) * t + p2, b0 = (m1 - m0) * t + m0, b1 = (m2 - m1) * t + m1;

		return (b1 - b0) * t + b0;
	}
}