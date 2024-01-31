using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTwoShoot : MonoBehaviour
{
    [SerializeField]
    private ShootPointerPool _pointerPool;

    [SerializeField]
    private ProyectilePool _proyectilePool;

     [SerializeField]
     private Transform _shootPoint;

     [SerializeField]
     [Range(0f, 0.25f)]
     private float _proyectileSpeed;

     [Header("Auto Aim")]
     [SerializeField]
     private float _autoAimArea;

     [SerializeField]
     private float _autoAimDistance;

     [SerializeField]
     private float _autoShootTimeRemaining;

     [SerializeField]
     private bool _canShoot = false;

     [Header("Player Two")]
     [SerializeField]
     private float _shootTimeRemaining;

     [SerializeField]
     private SpriteRenderer spritePoint;

     [SerializeField]
     private Sprite[] pointers = new Sprite[2];


    private float _timeRemainigInitial;
    private float _timeRemainingInitialAuto;


     private Animator _anim;

     private void Start()
     {
          _timeRemainigInitial = _shootTimeRemaining;
          _timeRemainingInitialAuto = _autoShootTimeRemaining;

          //_anim = GetComponent<Animator>();
     }

     private void Update()
     {

          if (GameManager.Instance.twoPlayers)
          {
               if (_shootTimeRemaining <= 0)
               {
                    _canShoot = true;
                    spritePoint.sprite = pointers[0];
               }
               else
               {
                    _shootTimeRemaining -= Time.deltaTime;
               }

               if (Input.GetButtonDown("Fire1"))
               {
                    if (_canShoot)
                         Shoot();
               }
          }
          else
          {
               if (_autoShootTimeRemaining <= 0)
               {
                    _canShoot = true;
                    spritePoint.sprite = pointers[0];
               }
               else
               {
                    _autoShootTimeRemaining -= Time.deltaTime;
               }

               if (_canShoot)
                    AutoShoot();
          }
          
    }

    private void Shoot()
    {
        // Triggerea la animacion si la hay
        //if (_anim != null)
        //{
        //    _anim.SetTrigger("Shoot");
        //}

        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Apuntador
        GameObject pointer = _pointerPool.RequestPointer();
        pointer.transform.position = new Vector3(mouseCursorPos.x, mouseCursorPos.y, 0);

        // Proyectil

        PublicProyectile proyectil = _proyectilePool.RequestProyectile();
        proyectil.ShootProyectile(_shootPoint.position, mouseCursorPos);

        AudioManager.Instance.PlaySound2D("CakeShoot");

        _canShoot = false;
        spritePoint.sprite = pointers[1];
        _shootTimeRemaining = _timeRemainigInitial;

        //GameObject bullet = _pool.RequestBullet();
        //bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
        //if (bullet.transform.TryGetComponent(out Bullet bl))
        //{
        //     bl.speed = _bulletSpeed;
        //}
    }

     private void AutoShoot()
     {


          // Crear un punto aleatorio dentro del círculo
          Vector3 puntoAleatorio = CalculateRandomPoint();

          // Se dispara el proyectil en el punto
          // Apuntador
          GameObject pointer = _pointerPool.RequestPointer();
          pointer.transform.position = new Vector3(puntoAleatorio.x, puntoAleatorio.y, 0);

          // Proyectil

          PublicProyectile proyectil = _proyectilePool.RequestProyectile();
          proyectil.ShootProyectile(_shootPoint.position, puntoAleatorio);

          AudioManager.Instance.PlaySound2D("CakeShoot");

          _canShoot = false;
          spritePoint.sprite = pointers[1];
          _autoShootTimeRemaining = _timeRemainingInitialAuto;

     }

     private Vector2 CalculateRandomPoint()
     {
          int playerDirection = PlayerOne.Instance.direction;

          Vector2 targetPoint = (Vector2)PlayerOne.Instance.transform.position + new Vector2(playerDirection * _autoAimDistance, 0);

          float randomAngle = Random.Range(0f, 360f); // Ángulo aleatorio en grados
          float rad = randomAngle * Mathf.Deg2Rad; // Convertir a radianes

          float randomRadius = Random.Range(0, _autoAimArea);
          // Calcular las coordenadas cartesianas
          float impactPointX = targetPoint.x + randomRadius * Mathf.Cos(rad);
          float impactPointY = targetPoint.y + randomRadius * Mathf.Sin(rad);

          return new Vector2(impactPointX, impactPointY);
     }
}
