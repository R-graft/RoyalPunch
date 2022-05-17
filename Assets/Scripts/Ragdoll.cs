using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Rigidbody[] bodyParts;

    public Rigidbody _rbCharacter;

    public Animator animator;

    public Vector3[] fallingTransforms;

    public Quaternion[] falingAngles;

    public Vector3[] upTransforms;

    public Quaternion[] upAngles;

    public float upTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        bodyParts = GetComponentsInChildren<Rigidbody>();
        GameEvens.tapEvent.AddListener(tapDamage);
        GameEvens.jabEvent.AddListener(jabDamage);
        GameEvens.upperEvent.AddListener(upperDamage);

        fallingTransforms = new Vector3[bodyParts.Length];

        falingAngles = new Quaternion[bodyParts.Length];

        upTransforms = new Vector3[bodyParts.Length];

        upAngles = new Quaternion[bodyParts.Length];
    }

    private void tapDamage()
    {
      
            StartCoroutine(RegdolldOn());
     
        
    }
    private void jabDamage()
    {
      
            StartCoroutine(RegdolldOn());
   

    }
    private void upperDamage()
    {
       
            StartCoroutine(RegdolldOn());
      

    }
    IEnumerator RegdolldOn()
    {
        animator.enabled = false;
        float timer = 0;
        while (timer<2)
        {
        yield return new WaitForFixedUpdate();
        _rbCharacter.AddForce(_rbCharacter.position * (10 / _rbCharacter.position.magnitude) * 200, ForceMode.Acceleration);
            timer += Time.fixedDeltaTime / 2;
        }

        while (upTime < 3)
        {

            yield return new WaitForFixedUpdate();


            for (int i = 0; i < fallingTransforms.Length; i++)
            {
                //bodyParts[i].transform.localPosition = Vector3.Lerp(fallingTransforms[i], upTransforms[i], upTime);

                bodyParts[i].transform.localRotation = Quaternion.Lerp(falingAngles[i], upAngles[i], upTime);

                upTime += Time.fixedDeltaTime / 10;
            }

        }
        animator.enabled = true;
        yield break;
    }

    void Update()
    {

    }
}
