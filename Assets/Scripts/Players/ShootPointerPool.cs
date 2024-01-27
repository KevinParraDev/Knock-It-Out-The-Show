using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPointerPool : MonoBehaviour
{

    [SerializeField]
    private GameObject _pointerPrefab;

    [SerializeField]
    private int _poolSize;

    [SerializeField]
    private List<GameObject> _pointerList;

    private void Start()
    {
        AddPointersToPool(_poolSize);
    }

    private void AddPointersToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject pointer = Instantiate(_pointerPrefab);
            pointer.SetActive(false);
            _pointerList.Add(pointer);
            pointer.transform.parent = transform;
        }
    }

    public GameObject RequestPointer()
    {
        foreach (GameObject pointer in _pointerList)
        {
            // Si la bala no está activa se puede disparar
            if (!pointer.activeSelf)
            {
                pointer.SetActive(true);
                return pointer;
            }
        }
        AddPointersToPool(1);
        _pointerList[_pointerList.Count - 1].SetActive(true);
        return _pointerList[_pointerList.Count - 1];
    }
}
