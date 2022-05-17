using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private GameObject _smokeEffect;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private GameObject _dangerZone;

    [SerializeField]
    private GameObject _fieldPunch;

    [SerializeField]
    private ParticleSystem _particleSystemHand;

    [SerializeField]
    private ParticleSystem _particleSystemFoot;

    [SerializeField]
    private ParticleSystem _particleSystemFieldHand;

    [SerializeField]
    private ParticleSystem _particleFloor;

    private Rigidbody _rb;

    private bool look = true;

    private bool damage = false;

    private bool super = true; 

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
        if (look)
        {
            _rb.transform.LookAt(_playerTransform);  
        }
        if (damage)
        {
            GameEvens.playerDamage.Invoke();
        }
    }
    public void CoroutineStart(bool value)
    {
        if (value)
        {
            StartCoroutine("GetSuperPunch");
        }
        else
        {
            StopCoroutine("GetSuperPunch");
        }
    }
    IEnumerator GetSuperPunch()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);

            int punch = Random.Range(1,4);
            switch (punch)
            {
                case 1: StartCoroutine("Jab");
                    break;
                case 2:
                    StartCoroutine("Upper");
                    break;
                case 3:
                    StartCoroutine("Tap");
                    break;
            }
        }
    }
   
    IEnumerator Jab()
    {
        look = false;
        super = false;
        _animator.SetTrigger("Jab");

        yield return new WaitForSecondsRealtime(0.5f);

        while (_fieldPunch.transform.localScale.x < 100f )
        {
            yield return new WaitForFixedUpdate();
            _fieldPunch.transform.localScale += new Vector3(1.5f, 1.5f, 0);
        }

        _particleSystemFieldHand.Play();
        yield return new WaitForSecondsRealtime(0.2f);
        _fieldPunch.transform.localScale = new Vector3(1, 1, 0);

        GameEvens.jabEvent.Invoke();

        super = true;
        look = true;
        yield break;
    }
   IEnumerator Upper()
    {
        look = false;
        super = false;
        _animator.SetTrigger("Upper");
        _particleSystemHand.Play();
        _particleFloor.Play();

        float time = 0;
        while (time < 2.6f)
        {
            yield return new WaitForFixedUpdate();
            _playerTransform.Translate(Vector3.forward * 0.06f);
            time+= 0.02f;
        }

        GameEvens.upperEvent.Invoke();

        super = true;
        look = true;
        yield break;
    }
    IEnumerator Tap()
    {
        look = false;
        super = false;
        _smokeEffect.SetActive(false);
        _animator.SetTrigger("Tup");
        _particleSystemFoot.Play();

        while (_dangerZone.transform.localScale.x < 100)
        {
            yield return new WaitForFixedUpdate();
            _dangerZone.transform.localScale += new Vector3(0.85f, 0.85f, 0);
        }

        _smokeEffect.SetActive(true);
        _dangerZone.transform.localScale = new Vector3(1, 1, 0);

        GameEvens.tapEvent.Invoke();

        super = true;
        look = true;
        yield break;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (super)
        {
        _animator.SetBool("Box", true);
        StopAllCoroutines();
        }
        damage = true;
       
    }

    private void OnCollisionExit(Collision collision)
    {
        _animator.SetBool("Box", false);
        CoroutineStart(true);
        damage = false;
    }
}
