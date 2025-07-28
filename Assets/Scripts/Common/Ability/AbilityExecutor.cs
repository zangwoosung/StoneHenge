
using UnityEngine;


class AbilityExecutor : MonoBehaviour {
    [SerializeField] AbilityData ability;
    [SerializeField] GameObject target;    

      void SpawnVFX() {
        if (ability.vfxPrefab == null) return;

        var vfx = Instantiate(ability.vfxPrefab, target.transform.position, transform.rotation);

        if (ability.vfxPrefab == null) return;       
        ParticleSystem ps = vfx.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps, ps.main.duration + ps.main.startLifetime.constant);
               
        foreach (var effect in ability.effects)
        {
            Debug.Log("effect name  " + effect.ToString());
            effect.Execute(gameObject, target);
        }      

        Destroy(vfx, 5f);
    }

    public void Execute(GameObject target) {
       
        SpawnVFX();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            Execute(target);
        }
    }
}