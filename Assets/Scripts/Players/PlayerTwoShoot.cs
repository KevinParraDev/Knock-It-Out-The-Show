using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoShoot : MonoBehaviour
{
    [SerializeField]
     private ShootPointerPool _pool;

     [SerializeField]
     private Transform _shootPoint;

     [SerializeField]
     [Range(0f, 0.25f)]
     private float _proyectileSpeed;

     [SerializeField]
     private float _shootTimeRemaining;

     [SerializeField]
     private bool _canShoot = false;


     private float _timeRemainigInitial;

     private Animator _anim;

     private void Start()
     {
          _timeRemainigInitial = _shootTimeRemaining;

          _anim = GetComponent<Animator>();
     }

     private void Update()
     {
          if (_shootTimeRemaining < 0)
          {
            _canShoot = true;
            
          }
          else
          {
          _shootTimeRemaining -= Time.deltaTime;
          }
     }

    private void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(_canShoot)
                Shoot();
        }
    }

    private void Shoot()
    {
          // Triggerea la animacion si la hay
          if (_anim != null)
          {
              _anim.SetTrigger("Shoot");
          }

        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Apuntador
        GameObject pointer = _pool.RequestPointer();
        pointer.transform.position = new Vector3(mouseCursorPos.x, mouseCursorPos.y, 0);
        Debug.Log("");
        _canShoot = false;
        _shootTimeRemaining = _timeRemainigInitial;

        // Proyectil

        //GameObject bullet = _pool.RequestBullet();
        //bullet.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
        //if (bullet.transform.TryGetComponent(out Bullet bl))
        //{
        //     bl.speed = _bulletSpeed;
        //}
    }
}
