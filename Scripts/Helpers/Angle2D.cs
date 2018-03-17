using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/***********************************************/
/* Helper structure for manipulating 2D angles */
/* By David Hudson, DPH Software, 2014         */
/* You are free to use or modify this code     */
/* as you wish, no guarantee is provided.      */
/* http://www.dphsw.co.uk/blog/?p=180          */
/***********************************************/

public struct Angle2D {

    private float oneminuscos, sin;
    private float cos
    {
        get { return 1f - oneminuscos; }
        set { oneminuscos = 1f - value; }
    }

    public const float PI = 3.1415926535897932384626433832795f;
    public const float HalfPI = 1.5707963267948966192313216916398f;
    public const float TwoPI = 6.283185307179586476925286766559f;
    public const float OneAndAHalfPI = 4.7123889803846898576939650749193f;
    public const float Deg2Rad = 0.01745329251994329576923690768489f;
    public const float Rad2Deg = 57.295779513082320876798154814105f;

    private Angle2D(float c, float s)
    {
        oneminuscos = 1f - c;
        sin = s;
    }
    public Angle2D(Vector2 LookAt)
    {
        Vector2 v = LookAt.normalized;
        if (v.sqrMagnitude > 0.5f)
        {
            oneminuscos = 1f - v.x;
            sin = v.y;
        }
        else
        {
            oneminuscos = sin = 0f;          
        }
    }

    public static Angle2D operator +(Angle2D a1, Angle2D a2)
    {
        return new Angle2D((a1.cos * a2.cos) - (a1.sin * a2.sin), (a1.cos * a2.sin) + (a1.sin * a2.cos));
    }
    public static Angle2D operator -(Angle2D a1, Angle2D a2)
    {
        return new Angle2D((a1.cos * a2.cos) + (a1.sin * a2.sin), (a1.sin * a2.cos) - (a1.cos * a2.sin));
    }
    public static Angle2D operator -(Angle2D a)
    {
        return new Angle2D(a.cos, -a.sin);
    }
    public static bool operator ==(Angle2D a1, Angle2D a2)
    {
        return ((a1.cos * a2.cos) + (a1.sin * a2.sin) > 0.999999f);
    }
    public static bool operator !=(Angle2D a1, Angle2D a2)
    {
        return !((a1.cos * a2.cos) + (a1.sin * a2.sin) > 0.999999f);
    }
    public override bool Equals(object obj)
    {
        if (obj is Angle2D)
        {
            Angle2D a = (Angle2D)obj;
            return this == a;
        }
        else return false;
    }
    public override int GetHashCode()
    {
        return (int)(100000000f * sin - 1000000000f * oneminuscos);
    }

    public float sine { get { return sin; } }
    public float cosine { get { return cos; } }
    public float tangent { get { return sin / cos; } }
    public float radians
    {
        get
        {
            if (sin * sin < cos * cos)
            {
                return (cos > 0f ? (sin < 0f ? TwoPI : 0f) : PI) + Mathf.Atan(sin / cos);
            }
            else
            {
                return (sin > 0f ? HalfPI : OneAndAHalfPI) - Mathf.Atan(cos / sin);
            }
        }
        set
        {
            sin = Mathf.Sin(value);
            cos = Mathf.Cos(value);
        }
    }
    public float degrees
    {
        get
        {
            if (sin * sin < cos * cos)
            {
                return (cos > 0f ? (sin < 0f ? 360f : 0f) : 180f) + Rad2Deg * Mathf.Atan(sin / cos);
            }
            else
            {
                return (sin > 0f ? 90f : 270f) - Rad2Deg * Mathf.Atan(cos / sin);
            }
        }
        set
        {
            sin = Mathf.Sin(Deg2Rad * value);
            cos = Mathf.Cos(Deg2Rad * value);
        }
    }
    public Vector2 lookAt
    {
        get { return new Vector2(1f - oneminuscos, sin); }
        set
        {
            Vector2 v = value.normalized;
            if (v.sqrMagnitude > 0.5f)
            {
                oneminuscos = 1f - v.x;
                sin = v.y;
            }
            else
            {
                oneminuscos = 0f;
                sin = 0f;
            }
        }
    }
    public Quaternion quaternion
    {
        get
        {
            Vector2 v = new Vector2(4f - 2f * oneminuscos + sin, oneminuscos + (2f * sin));
            v.Normalize();
            return new Quaternion(0f,0f,v.y,v.x);
        }
        set
        {
            oneminuscos = 1f + (value.z * value.z) - (value.w * value.w);
            sin = 2f * value.w * value.z;
        }
    }

    public Angle2D halfangle
    {
        get
        {
            Vector2 v = new Vector2(4f - 2f * oneminuscos + sin, oneminuscos + (2f * sin));
            v.Normalize();
            return new Angle2D(v.x,v.y);
        }
    }
    public Angle2D doubleangle
    {
        get { return new Angle2D((cos * cos) - (sin * sin), 2f * cos * sin); }
    }

    public Angle2D plusHalfTurn { get { return new Angle2D(-cos, -sin); } }
    public Angle2D plusQuarterTurnAnticlockwise { get { return new Angle2D(-sin, cos); } }
    public Angle2D plusQuarterTurnClockwise { get { return new Angle2D(sin, -cos); } }

    public static float Dot(Angle2D a, Angle2D b) { return (a.cos * b.cos) + (a.sin * b.sin); }
    public static float PerpDot(Angle2D a, Angle2D b) { return (a.cos * b.sin) - (a.sin * b.cos); }
    public static float PerpDot(Vector2 a, Vector2 b) { return (a.x * b.y) - (a.y * b.x); }

    public static Angle2D Average(List<Angle2D> Angles)
    {
        float c = 0f;
        float s = 0f;
        int n = Angles.Count;
        for (int i = 0; i < n; i++)
        {
            c += Angles[i].cos;
            s += Angles[i].sin;
        }
        return new Angle2D(new Vector2(c, s));
    }
    public static Angle2D Average(Angle2D[] Angles)
    {
        float c = 0f;
        float s = 0f;
        int n = Angles.Length;
        for (int i = 0; i < n; i++)
        {
            c += Angles[i].cos;
            s += Angles[i].sin;
        }
        return new Angle2D(new Vector2(c, s));
    }

    public override string ToString()
    {
        return this.degrees.ToString();
    }
    public Vector2 ToVector2() { return new Vector2(cos, sin); }
    public Vector2 ToVector2(float length) { return new Vector2(length * cos, length * sin); }
    public Vector2 ToVector3() { return new Vector3(cos, sin, 0f); }
    public Vector2 ToVector3(float length) { return new Vector3(length * cos, length * sin, 0f); }
    public Vector2 ToVector4() { return new Vector4(cos, sin, 0f, 0f); }
    public Vector2 ToVector4(float length) { return new Vector4(length * cos, length * sin, 0f, 0f); }
    public Vector2 ToVector4Tangent() { return new Vector4(cos, sin, 0f, 1f); }
    public Vector2 ToVector4Tangent(float length) { return new Vector4(length * cos, length * sin, 0f, 1f); }

    public static implicit operator Angle2D(Vector2 LookAt)
    {
        Vector2 v = LookAt.normalized;
        if (v.sqrMagnitude > 0.5f) return new Angle2D(v.x, v.y);
        else return new Angle2D(1f, 0f);
    }
    public static implicit operator Angle2D(Vector3 LookAt)
    {
        Vector2 v = LookAt;
        v.Normalize();
        if (v.sqrMagnitude > 0.5f) return new Angle2D(v.x, v.y);
        else return new Angle2D(1f, 0f);
    }
    public static implicit operator Angle2D(Vector4 LookAt)
    {
        Vector2 v = LookAt;
        v.Normalize();
        if (v.sqrMagnitude > 0.5f) return new Angle2D(v.x, v.y);
        else return new Angle2D(1f, 0f);
    }
    public static implicit operator Vector2(Angle2D angle)
    {
        return new Vector2(angle.cos, angle.sin);
    }
    public static implicit operator Vector3(Angle2D angle)
    {
        return new Vector3(angle.cos, angle.sin, 0f);
    }

}
