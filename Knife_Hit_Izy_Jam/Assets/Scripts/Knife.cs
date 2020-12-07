using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("Atributes", order = 1)]
    public float speed;

    [Header("Audios", order = 1)]
    public AudioClip AudioThowKnife;
    public AudioClip AudioHitKnife;
    public AudioClip AudioHitWood;

    [Header("Skins", order = 1)]
    public Sprite Skin1;
    public Sprite Skin2;
    public Sprite Skin3;
    public Sprite Skin4;

    private bool isActive = true;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D collider2d;
    private AudioSource audioData;
    //private SpriteRenderer spriteRenderer;

    //Get Components
    private void Start() {
        audioData = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<BoxCollider2D>();
        //ChangeSkin();
    }

    //Thow knife and delete icon
    private void Update() {
        if (Input.GetMouseButtonDown(0) && isActive) {
            rigidbody2d.AddForce(new Vector2(0,speed), ForceMode2D.Impulse);
            Manager.Instance.DeleteKnifeIcon();
            audioData.PlayOneShot(AudioThowKnife);
        }
    }

    //Check if collision with wood or knife
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isActive)
            return;

        isActive = false;
        if (collision.collider.tag == "Wood") {
            //Set Parent to Wood
            transform.SetParent(collision.transform);
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.isKinematic = true;

            //Change Collider
            collider2d.offset = new Vector2(collider2d.offset.x, 0f);
            
            //Play audio
            audioData.PlayOneShot(AudioHitWood);
            
            //Call Manager
            Manager.Instance.KnifeHit();
        
        } else if (collision.collider.tag == "Knife") {
            //Play audio
            audioData.PlayOneShot(AudioHitKnife);

            //Call Manager
            Manager.Instance.StartSceneManager(false);
        }
    }
    /*
    private void ChangeSkin() {
        switch (Manager.Instance.knifeSkin) {
            case 4:
            spriteRenderer.sprite = Skin4;
            break;
            case 3:
            spriteRenderer.sprite = Skin3;
            break;
            case 2:
            spriteRenderer.sprite = Skin2;
            break;
            case 1:
            spriteRenderer.sprite = Skin1;
            break;
            default:
            spriteRenderer.sprite = Skin1;
            break;
        }
    }
    */
}