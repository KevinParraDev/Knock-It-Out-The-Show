using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilePool : MonoBehaviour
{
    [SerializeField]
    private PublicProyectile _proyectilePrefab;

    [SerializeField]
    private int _poolSize;

    [SerializeField]
    private List<PublicProyectile> _proyectileList;

    private void Start()
    {
        AddProyectileToPool(_poolSize);
    }

    private void AddProyectileToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            PublicProyectile proyectile = Instantiate(_proyectilePrefab);
            proyectile.gameObject.SetActive(false);
            _proyectileList.Add(proyectile);
            proyectile.transform.parent = transform;
        }
    }

    public PublicProyectile RequestProyectile()
    {
        foreach (PublicProyectile proyectile in _proyectileList)
        {
            // Si la bala no está activa se puede disparar
            if (!proyectile.gameObject.activeSelf)
            {
                proyectile.gameObject.SetActive(true);
                return proyectile;
            }
        }
        AddProyectileToPool(1);
        _proyectileList[_proyectileList.Count - 1].gameObject.SetActive(true);
        return _proyectileList[_proyectileList.Count - 1];
    }
}
