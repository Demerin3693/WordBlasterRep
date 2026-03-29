using UnityEngine;
using TMPro;
using System.Collections;

public class WordInputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameSettings settings;
    
    public Transform firePoint;     // точка з якої стріляєш
    public LineRenderer laser;      // сам лазер
    public float laserDuration = 0.1f;

    IEnumerator ShootLaser(Vector2 targetPos)
    {
        SoundManager.instance.PlayLaser();
        laser.gameObject.SetActive(true);
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, targetPos);

        yield return new WaitForSeconds(laserDuration);
        laser.gameObject.SetActive(false);
    }

    void Update()
    {
        // Коли натиснуто Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {

            CheckWord();
        }
    }

    public GameObject letterPrefab;

      public void ExplodeWord(string word, Vector2 pos) 
    {
    foreach (char c in word)
    {
        GameObject letterObj = Instantiate(letterPrefab, pos, Quaternion.identity);
        LetterParticle p = letterObj.GetComponent<LetterParticle>();
        p.Initialize(c.ToString(), pos);
    }
    }




        public void CheckWord()
    {
        string userWord = inputField.text.Trim().ToLower();
        Debug.Log(userWord);
        if (string.IsNullOrEmpty(userWord))
            return;

        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();

        foreach (EnemyMovement enemy in enemies)
        {
            string ua = enemy.word.word.ToLower();
            string en = enemy.word.translation.ToLower();
            Debug.Log(ua);
            Debug.Log(en);
            Debug.Log("finished");
            Vector2 pos = enemy.transform.position;

            if (userWord == enemy.translationText.text)
                {
                    StartCoroutine(ShootLaser(pos));
                    ExplodeWord(enemy.wordText.text, pos);
                    SoundManager.instance.PlayExplosion();
                    Destroy(enemy.gameObject);
                    inputField.text = "";
                    return;
                }
        }

        // Якщо не знайшло ворогів
        inputField.text = "";
        Debug.Log("Not found");
    }

}
