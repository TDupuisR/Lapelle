using UnityEngine;

public class AnubisDoggoMovement : MonoBehaviour
{
    public float speed = 2f;                                            // Vitesse d'ouverture de la porte
    [SerializeField, Range(0.1f, 50f)] private float destination = 5f;  // distance entre la porte et sa destination une fois ouverte (limité entre 0.1 et 50)
    [SerializeField, Range(0f, 360f)] private float RotationPath;       // Permet de faire pivoter le trajet de la porte
    private Vector2 directionAngle;                                     // Variable pour tranformer l'angle RotationPath (en degré) vers une direction (Vector2)
    private Vector3 destinationPosition;                                // Sert a transformer la distance avec la destination en coordonnées X/Y/Z
    public bool go;

    void Start()
    {
        directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
        destinationPosition = transform.position + (Vector3)directionAngle * destination;
    }

    void Update()
    {
        if (go)
        {
            transform.position = Vector2.MoveTowards(transform.position, destinationPosition, speed * Time.deltaTime);
        }
    }

    public void ouverture()
    {
        go = true;
    }

    // Fonction pour dessiner le chemin que prendra la porte en s'ouvrant dans l'éditeur
    void OnDrawGizmos()
    {
        if (!Application.IsPlaying(gameObject))
        {
            directionAngle = (Vector2)(Quaternion.Euler(0, 0, RotationPath) * Vector2.right);
            destinationPosition = transform.position + (Vector3)directionAngle * destination;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destinationPosition, 0.2f);
        Gizmos.DrawLine(destinationPosition, transform.position);
    }
}
