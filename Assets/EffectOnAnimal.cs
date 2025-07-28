using UnityEngine;

public class EffectOnAnimal : MonoBehaviour
{

    [SerializeField] GameObject _Disappear;
    [SerializeField] GameObject[] _effefPrefabs;
    public void Disappear(Vector3 pos)
    {
        _Disappear = _effefPrefabs[Random.Range(0, _effefPrefabs.Length)];
        
        GameObject psInstance = Instantiate(_Disappear, pos, Quaternion.identity);
        ParticleSystem ps = psInstance.GetComponent<ParticleSystem>();
        ps.Play();

        Destroy(psInstance, ps.main.duration + ps.main.startLifetime.constant);
    }
    
}
