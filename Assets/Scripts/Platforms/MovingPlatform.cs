
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform: MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private Transform[] _wayPoints;
    [SerializeField]
    [Range(0f, 0.1f)]
    private float _speed;

    [SerializeField]
    protected bool _active;

    [SerializeField]
    private float _stopSeconds = 1.5f;

    [Space(10)]
    [SerializeField]
    protected GameObject _platform;

    // flag de direccionamiento del movimiento
    private int _indexWayPoint;

    private Animator _platformAnimator;


    private void Start()
    {
        if(_platform.TryGetComponent(out Animator anim))
        {
            _platformAnimator = anim;
        }
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            Move();
        }
    }

    // Virtual permite usar un override en una clase derivada de esta (como la de Trap_Saw) para modificar este metodo
    public virtual void Switch()
    {
        if (_active)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }

    public virtual void Activate()
    {
        _active = true;

    }

    public virtual void Deactivate()
    {
        _active = false;

    }


    private void Move()
    {
        // Debe hacerse asi ya que al ser activable y desactivable usando lerp y Math.pingpong generaba comportamientos no deseados
        // adicionalmente esto permite tener mas de dos puntos de desplazamiento

        // Comprobamos si la plataforma ya llego al punto
        if (Vector3.Distance(_platform.transform.position, _wayPoints[_indexWayPoint].position) <= 0.1f)
        {
            StartCoroutine(ChangeWaypoint());
        }

        // Esto es lo que me desplaza la plataforma
        _platform.transform.position = Vector3.MoveTowards(_platform.transform.position, _wayPoints[_indexWayPoint].position, _speed);
    }

    private IEnumerator ChangeWaypoint()
    {
        _platformAnimator.SetBool("IsMoving", false);
        _active = false;
        yield return new WaitForSeconds(_stopSeconds);

        _indexWayPoint++;

        if (_indexWayPoint >= _wayPoints.Length)
        {
            _indexWayPoint = 0;
        }

        _platformAnimator.SetBool("IsMoving", true);
        _active = true;
    }

    // TODO: En caso de que queramos que la plataforma vuelva al medio creamos el metodo Stop
}
