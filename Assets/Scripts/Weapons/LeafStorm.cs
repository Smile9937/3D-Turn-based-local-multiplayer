using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LeafStorm : MonoBehaviour
{
    [SerializeField] private float _knockbackIntensity = 10f;
    [SerializeField] private float _damageFrequecy = 2f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _parent;
    [SerializeField] private float _activeTime;
    private float _lastDamage;

    private void OnEnable()
    {
        StartCoroutine(ActiveTime());
    }

    private IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(_activeTime);
        _parent.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        IDamageable _damageTarget = other.GetComponent<IDamageable>();

        if(_damageTarget != null && _lastDamage >= _damageFrequecy)
        {
            _damageTarget.TakeDamage(_damage);

            float knockback = Random.Range(-_knockbackIntensity, _knockbackIntensity);

            _damageTarget.TakeKnockback(new Vector3(knockback, _knockbackIntensity, knockback));
        }
    }
    private void FixedUpdate()
    {
        if(_lastDamage >= _damageFrequecy)
        {
            _lastDamage = 0;
        }
        _lastDamage += Time.deltaTime;
    }
}
