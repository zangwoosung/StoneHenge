using UnityEngine;

public class EffectOnAnimal : MonoBehaviour
{

    [SerializeField] GameObject _Disappear;
    public void Disappear(Vector3 pos)
    {

        GameObject psInstance = Instantiate(_Disappear, pos, Quaternion.identity);
        ParticleSystem ps = psInstance.GetComponent<ParticleSystem>();
        ps.Play();

        Destroy(psInstance, ps.main.duration + ps.main.startLifetime.constant);
    }
    
}
