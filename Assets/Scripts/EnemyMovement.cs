using UnityEngine;
using TMPro;
public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    private Rigidbody2D rb;

    [Header("Attributes")]
    public float moveSpeed = 2f;
    public Transform target;            // можна призначити в інспекторі
    public string targetTag = "Player"; // якщо не призначено — знайдемо за тегом
    public float stopDistance = 0.3f;   // зупинка коли близько до гравця
    public bool faceTarget = true;      // чи дивитись в сторону гравця (фліп)

    public WordEntry word;
    public TMP_Text wordText;
    public TMP_Text translationText; 

    public GameSettings settings;
    public TMP_Text deadLetter;

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Vector2 pos = transform.position;

        // Вибух літер
        WordInputManager mgr = FindAnyObjectByType<WordInputManager>();
            if (mgr != null)
                mgr.ExplodeWord(wordText.text, pos);
            other.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag(targetTag);
            if (go != null) target = go.transform;
        }

        word = WordManager.Instance.GetRandomWord();
        if (settings.EnglishToUkrainian)
        {
        wordText.text = word.translation;
        translationText.text = word.word;
        }
        else
        {
        wordText.text = word.word;
        translationText.text = word.translation;
        }

    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 currentPos = rb.position;
        Vector2 targetPos = target.position;
        Vector2 direction = targetPos - currentPos;
        float distance = direction.magnitude;
        /*
        if (distance > stopDistance)
        {
            Vector2 move = direction.normalized * settings.moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(currentPos + move);
        }
        else
        {
            // опціонально зупинити
            rb.linearVelocity = Vector2.zero;
        }
        */
         // рухаємо ворога строго вліво
        Vector2 move = Vector2.left * settings.moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        if (faceTarget)
        {
            // простий фліп по X (працює якщо спрайт дивиться вправо за замовчуванням)
            Vector3 local = transform.localScale;
            if (direction.x > 0.01f) local.x = Mathf.Abs(local.x);
            else if (direction.x < -0.01f) local.x = -Mathf.Abs(local.x);
            transform.localScale = local;
        }

        if (settings.enableTranslation)
        {
            translationText.alpha = 1f;
        }
        else
        {
            translationText.alpha = 0f;
        }
    }
}
