using System.Collections.Generic;
using UnityEngine;
public class FishManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _fishesPrefab;
    [Header("X - min falue, Y - max value")]
    [SerializeField] private Vector2Int _quantityForSpawn;

    private List<Fish1> _fishes = new List<Fish1>();

    private void FixedUpdate()
    {
        foreach(var fish in _fishes)
        {
            fish.DoUpdate();
        }
    }

    public void RemoveFish(Fish1 fish)
    {
        _fishes.Remove(fish);
    }

    public void Spawn()
    {
        if (_fishesPrefab == null)
        {
            return;
        }

        DestroyFish(_fishes);
        DestroyChilds();
        for (var i = 0; i < _fishesPrefab.Count; ++i)
        {
            var quantityFish = Random.Range(_quantityForSpawn.x, _quantityForSpawn.y);
            for(var j = 0; j < quantityFish; ++j)
            {
                var fishObj = Instantiate(_fishesPrefab[i], transform, false);
                var fish = fishObj.GetComponent<Fish1>();
                _fishes.Add(fish);
                fishObj.transform.position = new Vector3(0f, Random.Range(-fish.DepthOfHabitat.x, -fish.DepthOfHabitat.y), 0f);
            }
        }
    }

    public void DestroyFish(List<Fish1> fishes)
    {
        foreach (var fish in fishes)
        {
            Destroy(fish.gameObject);
        }

        fishes.Clear();
    }

    private void DestroyChilds()
    {
        for (var i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
