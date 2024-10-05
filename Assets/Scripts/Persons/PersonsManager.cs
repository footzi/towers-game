using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonsManager : MonoBehaviour
{
    private List<Person> _persons = new List<Person>();

    public static event Action<Person> OnEnemyCollideWithPerson;

    void OnEnable()
    {
        EnemiesManager.OnEnemyCollision += OnEnemyCollisionHandler;
    }

    void OnDisable()
    {
        EnemiesManager.OnEnemyCollision -= OnEnemyCollisionHandler;
    }

    void Awake()
    {
        _persons = FindObjectsOfType<Person>().ToList();
    }

    private void OnEnemyCollisionHandler(Collider2D collision, Enemy enemy)
    {
        Person person = _persons.Find((obj) => obj.gameObject == collision.gameObject);

        if (person)
        {
            Destroy(person.gameObject);
            _persons.Remove(person);

            OnEnemyCollideWithPerson?.Invoke(person);
        };
    }

    public List<Person> GetPersons()
    {
        return _persons;
    }
}
