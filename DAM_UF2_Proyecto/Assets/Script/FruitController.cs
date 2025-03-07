using System;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    //public static bool inBasket = false;
    public AudioClip collisionSoundClip;     

    private Dictionary<string, string> fruitEvolution = new Dictionary<string, string>()
    {
        {"Cherry", "Strawberry"},
        {"Strawberry", "Grape"},
        {"Grape", "Dekopon"},
        {"Dekopon", "Orange"},
        {"Orange", "Apple"},
        {"Apple", "Peach"},
        {"Peach", "Pineapple"},
        {"Pineapple", "Melon"},
        {"Melon", "Watermelon"},
        {"Watermelon", "Cherry"}
    };
    
    private Dictionary<string, int> fruitScore = new Dictionary<string, int>(){
        {"Cherry", 5},
        {"Strawberry", 10},
        {"Grape", 15},
        {"Dekopon", 20},
        {"Orange", 25},
        {"Apple", 30},
        {"Peach", 35},
        {"Pineapple", 40},
        {"Melon", 45},
        {"Watermelon", 50}
    };

    [SerializeField] private GameObject[] fruitPrefabs;
    private Dictionary<string, GameObject> fruitPrefabsDict;
    private float limitYTop;

    private void Start()
    {
        fruitPrefabsDict = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in fruitPrefabs)
        {
            fruitPrefabsDict[prefab.tag] = prefab;
        }

        GameObject topLimit = GameObject.Find("TopLimit");
        BoxCollider2D leftCollider = topLimit.GetComponent<BoxCollider2D>();
        limitYTop = leftCollider.bounds.max.y; 
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string myTag = gameObject.tag;
        string otherTag = other.gameObject.tag;

        if (myTag == otherTag && fruitEvolution.ContainsKey(myTag))
        {
            if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            {
                MergeFruits(other.gameObject);
                if (collisionSoundClip != null){
                    AudioSource.PlayClipAtPoint(collisionSoundClip, transform.position);
                }
            }
        }
    }


    private void MergeFruits(GameObject otherFruit)
    {
        string newFruitTag = fruitEvolution[gameObject.tag]; 

        if (fruitPrefabsDict.ContainsKey(newFruitTag)){
        Vector3 spawnPosition = (transform.position + otherFruit.transform.position) / 2; 

        CreateFruit(spawnPosition, newFruitTag); 

        int newFruitScore = fruitScore[newFruitTag];
        GameManager.instance.IncreaseScore(newFruitScore); 

        }

        Destroy(otherFruit);
        Destroy(gameObject);
    }

    private void CreateFruit(Vector3 spawnPosition, String newFruitTag){

        GameObject newFruit = Instantiate(fruitPrefabsDict[newFruitTag], spawnPosition, Quaternion.identity);
        newFruit.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "TopLimit") 
        {
            float velocidadY = GetComponent<Rigidbody2D>().linearVelocity.y;

            if (velocidadY > 0) 
            {
                GameManager.instance.GameOver();
            }
        }
    }

}