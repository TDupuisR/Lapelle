using UnityEngine;

public class AnimVague : MonoBehaviour
{
    public AnimationCurve curveEau; 
    public float amplitude = 1f;     
    public float speed = 1f;         
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float t = (Time.time * speed);
        float y = curveEau.Evaluate(t) * amplitude;

        transform.position = new Vector3(startPos.x, startPos.y + y, startPos.z);
    }
}
