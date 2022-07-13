using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    public float speed;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;
    public GameObject telaFinal;
    public GameObject telaMorte;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7;
    private bool dead = false;

    public bool invencible = true;

    public LevelManager levelManager;
    [Header("text")]
    public TextMeshPro textoPowerUp;
    
    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    public float scaleDuration = 1f;
    public float scaleBounce = 1.2f;
    public Ease ease = Ease.OutBack;

    [Header("vfx")]
    public ParticleSystem vfxDead;

    [Header("Limits")]
    public float limit = 5f;

    [SerializeField] private BouceHelper _bouceHelper;
    [SerializeField] private ScaleStartPlayer _scaleHelper;

    private void Start()
    {
        ResetSpeed();
        _startPosition = transform.position;
        invencible = false;

    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        if (_pos.x < -limit) _pos.x = -limit;
        else if (_pos.x > limit) _pos.x = limit;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward* _currentSpeed * Time.deltaTime,Space.World);
    }

    public void ScalePlayer()
    {
        

        if (_scaleHelper != null)
        {
            _scaleHelper.ScalePlayerInit(transform);
        }

    }

    public void Bounce()
    {
        if (_bouceHelper != null)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _bouceHelper.Bounce();           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy && transform.tag == "Player")
        {
            if (!invencible)
            {
                MoveBack(collision.transform);
                dead = true;
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
        
    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible)EndGame(AnimatorManager.AnimationType.IDLE);
            //levelManager.NextLevel();
        }
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        //Debug.Log(dead);
        if (levelManager._index < levelManager.levels.Count && !dead)
        {
            endScreen.SetActive(true);
        }
        else if (!dead)
        {
            telaFinal.SetActive(true);
        }
        else
        {
            if (vfxDead != null) vfxDead.Play();
            telaMorte.SetActive(true);
            dead = false;
        }

        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed /_baseSpeedToAnimation);
    }

    #region POWERUPS
    
    public void SetPowerUpText(string s)
    {
        textoPowerUp.text = s;
    }
     public void PowerUpSpeedUp(float f)
    {
        _currentSpeed += f;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }
     public void ResetSpeed()
    {
        Debug.Log("Speed resetada de "+_currentSpeed+" pra "+speed);
        _currentSpeed = speed;
    }


    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        //var p = transform.position;
        //p.y = _startPosition.y + amount;
        //transform.position = p;

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
