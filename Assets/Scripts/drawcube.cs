using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawcube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float scale = 1.0f;
        float start = -0.5f;
        // first the front face
        Vector3[,] positions = {{new Vector3(start, start, start), new Vector3(start+scale, start, start)},
        {new Vector3(start+scale, start, start), new Vector3(start+scale, start+scale, start)},
        {new Vector3(start+scale, start+scale, start), new Vector3(start, start+scale, start)},
        {new Vector3(start, start+scale, start), new Vector3(start, start, start)},
        // left face 
        {new Vector3(start, start, start), new Vector3(start, start, start+scale)},
        {new Vector3(start, start, start+scale), new Vector3(start, start+scale, start+scale)},
        {new Vector3(start, start+scale, start+scale), new Vector3(start, start+scale, start)},
        // top face
        {new Vector3(start+scale, start+scale, start), new Vector3(start+scale, start+scale, start+scale)},
        {new Vector3(start+scale, start+scale, start+scale), new Vector3(start, start+scale, start+scale)},
        // bottom face
        {new Vector3(start+scale, start, start), new Vector3(start+scale, start, start+scale)},
        {new Vector3(start+scale, start, start+scale), new Vector3(start, start, start+scale)},
        // right face
        {new Vector3(start+scale, start+scale, start+scale), new Vector3(start+scale, start, start+scale)}};
        for(int i = 0; i < 12; i++) {
            makeLine(positions[i, 0], positions[i, 1]);
        }
    }

    void makeLine(Vector3 A, Vector3 B) {
        GameObject child = new GameObject();
        child.transform.position = gameObject.transform.position;
        child.transform.parent = gameObject.transform;
        child.AddComponent<LineRenderer>();
        LineRenderer lr = child.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.SetColors(Color.white, Color.white);
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.SetWidth(0.01f, 0.01f);
        int numPoints = 40;
        lr.positionCount = numPoints;
        Vector3 step = (B - A) / ((float) (numPoints-1));
        Vector3 current = A;
        var points = new Vector3[numPoints];
        Debug.Log(step);
        for(int i = 0; i < numPoints; i++) {
            points[i] = current;
            current = current + step;
            Debug.Log(current);
        }
        lr.SetPositions(points);

        Gradient gradient = new Gradient();
        float alpha = 0.5f;
        Color mygrey = new Color(0.7f, 0.7f, 0.7f);
        Color darker = new Color(0.4f, 0.4f, 0.4f);
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(mygrey, 0.05f), new GradientColorKey(darker, 0.5f), new GradientColorKey(mygrey, 0.95f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.forward = new Vector3(0, 1, 0);
    }
}
