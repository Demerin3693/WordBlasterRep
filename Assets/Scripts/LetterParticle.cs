using UnityEngine;
using TMPro;

public class LetterParticle : MonoBehaviour
{
    public TMP_Text textMesh;
    public float lifeTime = 1.5f;

    private Vector2 velocity;

    public void Initialize(string letter, Vector2 startPos)
    {
        textMesh.text = letter;
        transform.position = startPos;

        velocity = Random.insideUnitCircle * Random.Range(1.5f, 4f);

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;

        Color c = textMesh.color;
        c.a -= Time.deltaTime / lifeTime;
        textMesh.color = c;
    }
}
