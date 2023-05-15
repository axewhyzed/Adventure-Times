using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    bool dead = false;
    [SerializeField] AudioSource deathSound;

    private void Update()
    {
        if(transform.position.y < -1f && !dead)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy Body"))
        {
            GetComponent<MeshRenderer>().enabled = false; //unticks mesh renderer and makes player invisible
            GetComponent<Rigidbody>().isKinematic = true; // stops the player from being pushed around by other objects
            GetComponent<PlayerMovement>().enabled = false; //stops player movement script
            Die();
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), 1.3f);
        dead = true;
        deathSound.Play();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
