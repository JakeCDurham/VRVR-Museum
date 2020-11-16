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
        {new Vector3(start+scale, start, start+scale), new Vector3(start, start, start+scale)}};
        for(int i = 0; i < positions.Length; i++) {
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
        lr.SetPosition(0, A);
        lr.SetPosition(1, B);

        Gradient gradient = new Gradient();
        float alpha = 0.7f;
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(0.1f, 0.1f, 0.1f), 0.5f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
